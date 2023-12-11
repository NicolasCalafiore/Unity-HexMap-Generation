using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        [SerializeField] private GameObject generic_hex;
        [SerializeField] public GameObject perlin_map_object;

        

        void Start()
        {
            List<List<float>> elevation_map = GenerateElevationMap();
            List<List<float>> regions_map = GenerateRegionMap();
            List<List<float>> ocean_map = GenerateWaterMap();

            TerrainHandler.SpawnTerrain(map_size, generic_hex, elevation_map, regions_map, ocean_map, perlin_map_object);

            SetHexAsChildren();
            DebugHandler.SpawnPerlinViewers(map_size, elevation_map, "elevation", perlin_map_object);
            DebugHandler.SpawnPerlinViewers(map_size, regions_map, "regions_map", perlin_map_object);
        }

        private void SetHexAsChildren(){
            foreach(Hex i in TerrainHandler.GetHexList()){
                GameObject hex_go = TerrainHandler.hex_to_hex_go[i];
                hex_go.transform.SetParent(this.transform);
                hex_go.name = "Hex - " + i.GetColRow().x + "_" + i.GetColRow().y + " - " + i.GetRegionType() + " - " + i.GetElevationType();
            }
        }

        private List<List<float>> GenerateRegionMap(){

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

            List<List<float>> region_map = strategy.GenerateRegionsMap(map_size, perlin_map_object);
 
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

            List<List<float>> ocean_map = strategy.GenerateWaterMap(new Vector2( .7f, .475f), map_size, "ocean");
            List<List<float>> river_map = strategy.GenerateWaterMap(new Vector2( .75f, .65f), map_size, "river");

            ocean_map = TerrainUtils.CombineMapValue(ocean_map, river_map, map_size, (int) TerrainUtils.LandType.Water);
            strategy.OceanBorder(ocean_map);

            DebugHandler.SpawnPerlinViewers(map_size, river_map, "river_map", perlin_map_object);
            DebugHandler.SpawnPerlinViewers(map_size, ocean_map, "ocean_map", perlin_map_object);

            return ocean_map;
        }



        private List<List<float>> GenerateElevationMap(){
            List<List<float>> elevation_map = TerrainUtils.GenerateMap(map_size);

            ElevationStrategy strategy = null;
            switch (elevation_strategy)
            {
                case 0:
                    strategy = new RandomStrategy();
                    break;
                case 1:
                    strategy = new GroupingStrategy();
                    break;
                default:
                    strategy = new RandomStrategy();
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



  