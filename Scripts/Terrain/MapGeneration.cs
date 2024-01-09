using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;




namespace Terrain {

    public class MapGeneration
    {
        /*  
            Main entry point for Terrain Generation
            Generates all List<List<float>> maps
            Generates all HexTile objects
            Sets all HexTile properties
            Calls TerrainHandler to spawn terrain
        */
        private Vector2 map_size;                               // Map size
        private int elevation_strategy;                         
        private int land_strategy;
        private int region_strategy;
        private int features_strategy;
        public List<List<float>> ocean_map = new List<List<float>>();
        public List<List<float>> river_map = new List<List<float>>();
        public List<List<float>> water_map = new List<List<float>>();

        public List<List<float>> regions_map = new List<List<float>>();

        public List<List<float>> elevation_map = new List<List<float>>();
        public List<List<float>> features_map = new List<List<float>>();
        public List<List<float>> resource_map = new List<List<float>>();

        public MapGeneration(Vector2 map_size, int elevation_strategy, int land_strategy, int region_strategy, int features_strategy){
            this.map_size = map_size;
            this.elevation_strategy = elevation_strategy;
            this.land_strategy = land_strategy;
            this.region_strategy = region_strategy;
            this.features_strategy = features_strategy;
        }

        public void GenerateTerrainMap()
        {
            (List<List<float>>, List<List<float>>) water_tuple = GenerateWaterMap();    // Returns Perlin (ocean_map, river_map)
            ocean_map = water_tuple.Item1;
            river_map = water_tuple.Item2;
            water_map = TerrainUtils.CombineMapValue(ocean_map, river_map, map_size, (int) EnumHandler.LandType.Water);   // Combine ocean_map and river_map into water_map

            regions_map = GenerateRegionMap(ocean_map, river_map);

            elevation_map = GenerateElevationMap(regions_map);
            features_map = GenerateFeaturesMap(regions_map, water_map);
            resource_map = GenerateResourceMap(ocean_map, river_map, regions_map, features_map);

        }

        public void InitializeBorderEffects(List<HexTile> hex_list){
            List<Tuple<int, int>> land_border = TerrainUtils.CompareValueBorder(water_map, 1, 0);
            SetBorderElevation(land_border, (float) EnumHandler.HexElevation.Flatland, (float) EnumHandler.HexElevation.Flatland, hex_list);  // Sets all coasts to 0 if < 0
            //             coor of border tiles               conditional value                  set value
            
            List<System.Tuple<int, int>> land_ocean_border = TerrainUtils.CompareValueBorder(ocean_map, 0, 1);
            SetBorderRegion(land_ocean_border, (float) EnumHandler.HexRegion.Ocean, (float) EnumHandler.HexRegion.Shore, hex_list);   //Makes Shores

            List<System.Tuple<int, int>> shore_river_borders = TerrainUtils.CompareValueBorder(regions_map, (int) EnumHandler.HexRegion.Shore, (int) EnumHandler.HexRegion.River);
            SetBorderRegion(shore_river_borders, (float) EnumHandler.HexRegion.Shore, (float) EnumHandler.HexRegion.Ocean, hex_list); // Destroys shores that are touching rivers

        }

        private void SetBorderElevation( List<Tuple<int, int>> land_border, float conditional_value, float set_value, List<HexTile> hex_list){
            foreach(Tuple<int, int> tuple in land_border){
                foreach(HexTile hex in hex_list){
                    if(hex.GetColRow() == new Vector2(tuple.Item1, tuple.Item2)){
                            if( (float) hex.GetElevationType() <= conditional_value)
                                elevation_map[tuple.Item1][tuple.Item2] = set_value;
                                hex.is_coast = true;
                    }
                }
            }

        }

        private void SetBorderRegion(List<Tuple<int, int>> tuples, float conditional_value, float set_value, List<HexTile> hex_list){
            foreach(Tuple<int, int> tuple in tuples){
                foreach(HexTile hex in hex_list){
                    if(hex.GetColRow() == new Vector2(tuple.Item1, tuple.Item2)){
                        
                            if( (float) hex.GetElevationType() == conditional_value)
                                regions_map[tuple.Item1][tuple.Item2] = set_value;


                            else{
                                regions_map[tuple.Item1][tuple.Item2] = set_value;
                            }
                    }
                }
            }

        }



        private List<List<float>> GenerateFeaturesMap(List<List<float>> regions_map, List<List<float>> ocean_map)
        {
            FeaturesStrategy strategy = null;
            switch (features_strategy)
            {
                case 0:
                    strategy = new RegionSpecificRandom();
                    break;
                case 1:
                    strategy = new RegionSpecificRandom();
                    break;
                default:
                    strategy = new RegionSpecificRandom();
                    break;
            }

            List<List<float>> features_map = strategy.GenerateFeaturesMap(map_size, regions_map, ocean_map);
            return features_map;
        }

        private List<List<float>> GenerateResourceMap(List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map, List<List<float>> features_map)
        {
            ResourceStrategy strategy = null;
            switch (features_strategy)
            {
                case 0:
                    strategy = new ResourceRandom();
                    break;
                case 1:
                    strategy = new ResourceRandom();
                    break;
                default:
                    strategy = new ResourceRandom();
                    break;
            }

            List<List<float>> resource_map = strategy.GenerateResourceMap(map_size, ocean_map, river_map, regions_map, features_map);
            return resource_map;
        }

        private List<List<float>> GenerateRegionMap(List<List<float>> ocean_map, List<List<float>> river_map){

            RegionStrategy strategy = null;

            switch(region_strategy){
                case 0:
                    strategy = new MapFactor();
                    break;
                case 1:
                    strategy = new MapFactor();
                    break; 
                default:
                    strategy = new MapFactor();
                    break;
            }

            List<List<float>> region_map = strategy.GenerateRegionsMap(map_size, ocean_map, river_map);
 
            return region_map;
        }
        
        private (List<List<float>>, List<List<float>>) GenerateWaterMap(){

        WaterStrategy strategy = null;
            switch (land_strategy)
            {
                case 0:
                    strategy = new PerlinWaterStrategy();
                    break;
                case 1:
                    strategy = new PerlinWaterStrategy();
                    break;
                default:
                    strategy = new PerlinWaterStrategy();
                    break;
            }

            List<List<float>> ocean_map = strategy.GenerateWaterMap(map_size, EnumHandler.HexRegion.Ocean);
            List<List<float>> river_map = strategy.GenerateWaterMap(map_size, EnumHandler.HexRegion.River);
            strategy.OceanBorder(ocean_map);

            return (ocean_map, river_map);
        }



        private List<List<float>> GenerateElevationMap(List<List<float>> regions_map){
            List<List<float>> elevation_map = TerrainUtils.GenerateMap(map_size);

            ElevationStrategy strategy = null;
            switch (elevation_strategy)
            {
                case 0:
                    strategy = new ElevationGrouping();
                    break;
                case 1:
                    strategy = new ElevationGrouping();
                    break;
                default:
                    strategy = new ElevationGrouping();
                    break;
            }

            strategy.GenerateElevationMap(elevation_map, map_size, regions_map);
            return elevation_map;
        }

        public List<HexTile> GetHexList(List<HexTile> hex_list){
            return hex_list;
        }

        private void InitializeDebugComponents(List<List<float>> elevation_map, List<List<float>> regions_map, List<List<float>> features_map, List<List<float>> ocean_map){
            DebugHandler.PrintMapDebug("ocean_map", ocean_map);
            DebugHandler.PrintMapDebug("regions_map", regions_map);
            DebugHandler.PrintMapDebug("features_map", features_map);
            DebugHandler.PrintMapDebug("elevation_map", elevation_map);
        }
    }
}



  