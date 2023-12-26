using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;




namespace Terrain {

    public class MapGeneration : MonoBehaviour
    {

        [SerializeField] private Vector2 map_size = new Vector2();
        [SerializeField] private int elevation_strategy;
        [SerializeField] private int land_strategy;
        [SerializeField] private int region_strategy;
        [SerializeField] private int features_strategy;
        [SerializeField] public GameObject perlin_map_object;

        

        void Start()
        {
            List<List<float>> ocean_map = GenerateWaterMap();
            List<List<float>> regions_map = GenerateRegionMap(ocean_map);
            List<List<float>> features_map = GenerateFeaturesMap(regions_map, ocean_map);
            List<List<float>> elevation_map = GenerateElevationMap();

            TerrainHandler.SpawnTerrain(map_size, elevation_map, regions_map, ocean_map, features_map);

            DebugHandler.SetHexAsChildren(this);
            DebugHandler.SpawnPerlinViewers(map_size, elevation_map, "elevation");
            DebugHandler.SpawnPerlinViewers(map_size, regions_map, "regions_map");

            DebugHandler.PrintMapDebug("ocean_map", ocean_map);
            DebugHandler.PrintMapDebug("regions_map", regions_map);
            DebugHandler.PrintMapDebug("features_map", features_map);
            DebugHandler.PrintMapDebug("elevation_map", elevation_map);
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

        private List<List<float>> GenerateRegionMap(List<List<float>> ocean_map){

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

            List<List<float>> region_map = strategy.GenerateRegionsMap(map_size, ocean_map);
 
            return region_map;
        }
        
        private List<List<float>> GenerateWaterMap(){

        WaterStrategy strategy = null;
            switch (land_strategy)
            {
                case 0:
                    strategy = new PerlinStrategy();
                    break;
                case 1:
                    strategy = new PerlinStrategy();
                    break;
                default:
                    strategy = new PerlinStrategy();
                    break;
            }

            List<List<float>> ocean_map = strategy.GenerateWaterMap(map_size, "ocean");
            List<List<float>> river_map = strategy.GenerateWaterMap(map_size, "river");

            ocean_map = TerrainUtils.CombineMapValue(ocean_map, river_map, map_size, (int) EnumHandler.LandType.Water);
            strategy.OceanBorder(ocean_map);

            DebugHandler.SpawnPerlinViewers(map_size, river_map, "river_map");
            DebugHandler.SpawnPerlinViewers(map_size, ocean_map, "ocean_map");

            return ocean_map;
        }



        private List<List<float>> GenerateElevationMap(){
            List<List<float>> elevation_map = TerrainUtils.GenerateMap(map_size);

            ElevationStrategy strategy = null;
            switch (elevation_strategy)
            {
                case 0:
                    strategy = new ElevationRandom();
                    break;
                case 1:
                    strategy = new ElevationGrouping();
                    break;
                default:
                    strategy = new ElevationRandom();
                    break;
            }

            strategy.GenerateElevationMap(elevation_map, map_size);
            return elevation_map;
        }



        public Vector2 GetMapSize(){
            return map_size;
        }

    }
}



  