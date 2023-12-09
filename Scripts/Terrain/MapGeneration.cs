using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Strategy.Assets.Game.Scripts.Terrain;
using Unity.VisualScripting;
using UnityEngine;


namespace Terrain {

    public class MapGeneration : MonoBehaviour
    {

        [SerializeField] private Vector2 map_size = new Vector2();
        [SerializeField] private int elevation_strategy;
        [SerializeField] private GameObject generic_hex;
        [SerializeField] private GameObject perlin_map_object;
        [SerializeField] private float region_scale;
        [SerializeField] private float river_scale;
        [SerializeField] private float ocean_scale;
        

        void Start()
        {
            map_size += new Vector2(10, 10);
            List<List<float>> elevation_map = GenerateElevationMap();
            List<List<float>> regions_map = GenerateRegionMap();


            
            List<List<float>> ocean_map = GenerateOceanMap();
            DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, ocean_map, "ocean_map");
            DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, elevation_map, "elevation");
            DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, regions_map, "regions_map");



           
            GenerateMapFactors();

            TerrainHandler.SpawnTerrain(map_size, generic_hex, elevation_map, regions_map, ocean_map, perlin_map_object);


            foreach(Hex i in TerrainHandler.GetHexList()){
                GameObject hex_go = TerrainHandler.hex_to_hex_go[i];
                hex_go.transform.SetParent(this.transform);
            }





        }

        private List<List<float>> GenerateOceanMap(){

        List<List<float>> ocean_map = GenerateWaterMap(ocean_scale, new Vector2( .7f, .475f));
            for(int i = 0; i < ocean_map.Count; i++){
                for(int j = 0; j < ocean_map[i].Count; j++){
                    if(i < 5 || j > ocean_map[i].Count - 5 || j < 5 || i > ocean_map.Count - 5){
                        ocean_map[i][j] = 0;
                    }
                }
            }

            List<List<float>> river_map = GenerateWaterMap(river_scale, new Vector2( .75f, .65f));
            DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, river_map, "large_river_map");

           
            for(int i = 0; i < river_map.Count; i++){
                for(int j = 0; j < river_map[i].Count; j++){
                    if(river_map[i][j] == 0 || ocean_map[i][j] == 0){
                        ocean_map[i][j] = 0;
                    }
                }
            }

            return ocean_map;
        }

        private List<List<float>> GenerateRegionMap(){
            List<List<float>> regions_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(regions_map, map_size, region_scale);
            FilterRegionMap(regions_map, map_size);
            
            return regions_map;
        }

        private void GenerateMapFactors(){
            List<List<float>> rain_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(rain_map, map_size, 2.2f);

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {

                    if(rain_map[i][j] < .325f) rain_map[i][j] = 0;         //DESERT
                }
            } 

            DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, rain_map, "rain_map");

        }

        private List<List<float>> GenerateWaterMap(float scale, Vector2 max_min){
            List<List<float>> river_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(river_map, map_size, scale);
            FilterLandRiversMap(river_map, map_size, max_min);

            return river_map;
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

        public static void FilterRegionMap(List<List<float>> map, Vector2 map_size){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {

                    if(map[i][j] < .325f) map[i][j] = 0;         //DESERT
                    else if(map[i][j] < .4f) map[i][j] = 1;     //SAVANNAH
                    else if(map[i][j] < .6f) map[i][j] = 2;     //GRASSLAND
                    else if(map[i][j] < .925f) map[i][j] = 3;    //FOREST
                    else if(map[i][j] < 1f) map[i][j] = 4;      //JUNGLE
                }
            }   
        }



        public static void FilterLandRiversMap(List<List<float>> map, Vector2 map_size, Vector2 max_min){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    if(map[i][j] < max_min.x && map[i][j] > max_min.y) map[i][j] = 0;         //Water 
                    else map[i][j] = 1;     //Land
                }
            }   
        }

        public Vector2 GetMapSize(){
            return map_size;
        }

    }
}



  