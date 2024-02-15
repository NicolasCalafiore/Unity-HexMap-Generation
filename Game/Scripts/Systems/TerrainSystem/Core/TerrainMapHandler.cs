using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using static Terrain.MapManager;




namespace Terrain {

    public class TerrainMapHandler
    {
        private Vector2 map_size;
        private List<List<float>> ocean_map = new List<List<float>>();
        private List<List<float>> river_map = new List<List<float>>();
        private List<List<float>> water_map = new List<List<float>>();
        private List<List<float>> regions_map = new List<List<float>>();
        private List<List<float>> elevation_map = new List<List<float>>();
        private List<List<float>> features_map = new List<List<float>>();
        private List<List<float>> resource_map = new List<List<float>>();

        public TerrainMapHandler(){}
              
        public void GenerateTerrainMap(ElevationStrat elevation_strategy, LandStrat land_strategy , RegionStrat region_strategy, FeaturesStrat features_strategy, ResourceStrat resource_strat, Vector2 map_size)
        {
            this.map_size = map_size;

            water_map = GenerateWaterMap(land_strategy);    // Returns Perlin (ocean_map, river_map)
            regions_map = GenerateRegionMap(region_strategy, ocean_map, river_map);
            elevation_map = GenerateElevationMap(elevation_strategy, regions_map);
            features_map = GenerateFeaturesMap(features_strategy, regions_map, water_map);
            resource_map = GenerateResourceMap(resource_strat, ocean_map, river_map, regions_map, features_map);
        }


        private List<List<float>> GenerateFeaturesMap(FeaturesStrat features_strategy, List<List<float>> regions_map, List<List<float>> ocean_map)
        {
            FeaturesStrategy strategy = null;
            switch (features_strategy)
            {
                case FeaturesStrat.RegionSpecific:
                    strategy = new RegionSpecificRandom();
                    break;

                default:
                    strategy = new RegionSpecificRandom();
                    break;
            }

            List<List<float>> features_map = strategy.GenerateFeaturesMap(map_size, regions_map, ocean_map);
            return features_map;
        }

        private List<List<float>> GenerateResourceMap(ResourceStrat features_strategy, List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map, List<List<float>> features_map)
        {
            ResourceStrategy strategy = null;
            switch (features_strategy)
            {
                case ResourceStrat.RegionRandom:
                    strategy = new ResourceRandom();
                    break;

                default:
                    strategy = new ResourceRandom();
                    break;
            }

            List<List<float>> resource_map = strategy.GenerateResourceMap(map_size, ocean_map, river_map, regions_map, features_map);
            return resource_map;
        }

        private List<List<float>> GenerateRegionMap(RegionStrat region_strat, List<List<float>> ocean_map, List<List<float>> river_map){

            RegionStrategy strategy = null;

            switch(region_strat){
                case RegionStrat.MapFactor:
                    strategy = new MapFactor();
                    break;
                default:
                    strategy = new MapFactor();
                    break;
            }

            List<List<float>> region_map = strategy.GenerateRegionsMap(map_size, ocean_map, river_map);
 
            return region_map;
        }
        
        private List<List<float>> GenerateWaterMap(LandStrat land_strategy){

        WaterStrategy strategy = null;
            switch (land_strategy)
            {
                case LandStrat.Perlin:
                    strategy = new PerlinWaterStrategy();
                    break;

                default:
                    strategy = new PerlinWaterStrategy();
                    break;
            }

            this.ocean_map = strategy.GenerateWaterMap(map_size, RegionsEnums.HexRegion.Ocean);
            this.river_map = strategy.GenerateWaterMap(map_size, RegionsEnums.HexRegion.River);

            strategy.SetOceanBorder(ocean_map);
            water_map = TerrainUtils.CombineMapValue(ocean_map, river_map, map_size, (int) LandEnums.LandType.Water);   // Combine ocean_map and river_map into water_map
        
            return water_map;
        }



        private List<List<float>> GenerateElevationMap(ElevationStrat elevation_strategy, List<List<float>> regions_map){
            List<List<float>> elevation_map = TerrainUtils.GenerateMap();

            ElevationStrategy strategy = null;
            switch (elevation_strategy)
            {
                case ElevationStrat.Groupings:
                    strategy = new ElevationGrouping();
                    break;
                default:
                    strategy = new ElevationGrouping();
                    break;
            }

            strategy.GenerateElevationMap(elevation_map, map_size, regions_map);
            return elevation_map;
        }


        public void GenerateShores(){

            List<HexTile> hex_list = HexTile.GetHexList();

            List<Tuple<int, int>> land_border = TerrainUtils.CompareValueBorder(water_map, 1, 0);
            FilterShoreElevation(land_border, (float) ElevationEnums.HexElevation.Flatland, (float) ElevationEnums.HexElevation.Flatland, hex_list);  // Sets all coasts to 0 if < 0
            //             coor of border tiles               conditional value                  set value
            
             List<System.Tuple<int, int>> land_ocean_border = TerrainUtils.CompareValueBorder(ocean_map, 0, 1);
            SetBorderRegion(land_ocean_border, (float) RegionsEnums.HexRegion.Ocean, (float) RegionsEnums.HexRegion.Shore, hex_list);   //Makes Shores

            List<System.Tuple<int, int>> shore_river_borders = TerrainUtils.CompareValueBorder(regions_map, (int) RegionsEnums.HexRegion.Shore, (int) RegionsEnums.HexRegion.River);
            SetBorderRegion(shore_river_borders, (float) RegionsEnums.HexRegion.Shore, (float) RegionsEnums.HexRegion.Ocean, hex_list); // Destroys shores that are touching rivers

        }

        private void FilterShoreElevation( List<Tuple<int, int>> land_border, float conditional_value, float set_value, List<HexTile> hex_list){

            foreach(Tuple<int, int> tuple in land_border){
                foreach(HexTile hex in hex_list){
                    if(hex.GetColRow() == new Vector2(tuple.Item1, tuple.Item2)){
                            if( (float) hex.GetElevationType() <= conditional_value){
                                elevation_map[tuple.Item1][tuple.Item2] = set_value;
                            }
                            hex.SetCoast();
                    }
                }
            }
        }


        private void SetBorderRegion(List<Tuple<int, int>> tuples, float conditional_value, float set_value, List<HexTile> hex_list){
            foreach(Tuple<int, int> tuple in tuples){
                foreach(HexTile hex in hex_list){
                    if(hex.GetColRow() == new Vector2(tuple.Item1, tuple.Item2)){
                        
                            if( (float) hex.GetElevationType() == conditional_value){
                                regions_map[tuple.Item1][tuple.Item2] = set_value;
                                hex.SetRegionType(RegionsEnums.GetRegionType(set_value));
                            }
                            else{
                                regions_map[tuple.Item1][tuple.Item2] = set_value;
                                hex.SetRegionType(RegionsEnums.GetRegionType(set_value));
                            }
                    }
                }
            }

        }

        public List<List<float>> GetElevationMap(){
            return elevation_map;
        }

        public List<List<float>> GetRegionsMap(){
            return regions_map;
        }

        public List<List<float>> GetWaterMap(){
            return water_map;
        }

        public List<List<float>> GetOceanMap(){
            return ocean_map;
        }

        public List<List<float>> GetRiverMap(){
            return river_map;
        }

        public List<List<float>> GetFeaturesMap(){
            return features_map;
        }

        public List<List<float>> GetResourceMap(){
            return resource_map;
        }

        public Vector2 GetMapSize(){
            return map_size;
        }

        




    }
}