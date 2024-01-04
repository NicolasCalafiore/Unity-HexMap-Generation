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
        private List<HexTile> hex_list = new List<HexTile>();   
        private Vector2 map_size;
        private int elevation_strategy;
        private int land_strategy;
        private int region_strategy;
        private int features_strategy;
        public TerrainHandler terrain_handler;

        public MapGeneration(Vector2 map_size, int elevation_strategy, int land_strategy, int region_strategy, int features_strategy, TerrainHandler terrain_handler){
            this.map_size = map_size;
            this.elevation_strategy = elevation_strategy;
            this.land_strategy = land_strategy;
            this.region_strategy = region_strategy;
            this.features_strategy = features_strategy;
            this.terrain_handler = terrain_handler;
        }

        public void GenerateTerrain(List<Player> player_list)
        {
            hex_list = HexTileUtils.CreateHexObjects(map_size);

            (List<List<float>>, List<List<float>>) water_tuple = GenerateWaterMap();
            List<List<float>> ocean_map = water_tuple.Item1;
            List<List<float>> river_map = water_tuple.Item2;
            List<List<float>> water_map = TerrainUtils.CombineMapValue(ocean_map, river_map, map_size, (int) EnumHandler.LandType.Water);

            List<List<float>> regions_map = GenerateRegionMap(ocean_map, river_map);

            List<List<float>> elevation_map = GenerateElevationMap(regions_map);
            List<List<float>> features_map = GenerateFeaturesMap(regions_map, water_map);
            List<List<float>> resource_map = GenerateResourceMap(ocean_map, river_map, regions_map, features_map);
            List<List<float>> city_map = GenerateCityMap(water_map, player_list, features_map);
            DebugHandler.PrintMapDebug("city_map", city_map);

            HexTileUtils.SetHexElevation(elevation_map, hex_list);
            HexTileUtils.SetHexRegion(regions_map, hex_list);
            HexTileUtils.SetHexLand(water_map, hex_list);
            HexTileUtils.SetHexFeatures(features_map, hex_list);
            HexTileUtils.SetHexResource(resource_map, hex_list);
            HexTileUtils.SetStructureType(city_map, hex_list);

            DecoratorHandler.SetHexDecorators(hex_list);
        
            terrain_handler.SpawnTerrain(map_size, hex_list, city_map);
            //InitializeDebugComponents(elevation_map, regions_map, features_map, water_map);




        }

        private List<List<float>> GenerateCityMap(List<List<float>> water_map, List<Player> player_list, List<List<float>> features_map)
        {
            List<List<float>> city_map = TerrainUtils.GenerateMap(map_size);
            foreach(Player player in player_list){
                Vector3 random_coor = TerrainUtils.RandomVector3(map_size);
                while(water_map[(int) random_coor.x][(int) random_coor.z] == (int) EnumHandler.LandType.Water || features_map[(int) random_coor.x][(int) random_coor.z] != (int) EnumHandler.HexNaturalFeature.None){
                    random_coor = TerrainUtils.RandomVector3(map_size);
                }
                city_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.StructureType.Capital;
            }

            return city_map;
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
                    strategy = new RegionPerlin();
                    break;
                case 1:
                    strategy = new MapFactor();
                    break; 
                default:
                    strategy = new RegionPerlin();
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

            List<List<float>> ocean_map = strategy.GenerateWaterMap(map_size, EnumHandler.HexRegion.Ocean, hex_list);
            List<List<float>> river_map = strategy.GenerateWaterMap(map_size, EnumHandler.HexRegion.River, hex_list);
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

        public List<HexTile> GetHexList(){
            return hex_list;
        }

        private void InitializeDebugComponents(List<List<float>> elevation_map, List<List<float>> regions_map, List<List<float>> features_map, List<List<float>> ocean_map){
            DebugHandler.SetHexAsChildren(this);
            DebugHandler.SpawnPerlinViewers(map_size, elevation_map, "elevation");
            DebugHandler.SpawnPerlinViewers(map_size, regions_map, "regions_map");

            DebugHandler.PrintMapDebug("ocean_map", ocean_map);
            DebugHandler.PrintMapDebug("regions_map", regions_map);
            DebugHandler.PrintMapDebug("features_map", features_map);
            DebugHandler.PrintMapDebug("elevation_map", elevation_map);
        }

    }
}



  