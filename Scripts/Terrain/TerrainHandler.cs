using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using TMPro;
using UnityEngine;

namespace Terrain{
    public static class TerrainHandler
    {
        private static GameObject generic_hex;
        private static List<Hex> hex_list = new List<Hex>();
        private static List<GameObject> hex_go_list = new List<GameObject>();
        public static Dictionary<Hex, GameObject> hex_to_hex_go = new Dictionary<Hex, GameObject>();

        public static void SpawnTerrain(Vector2 map_size, GameObject generic_hex, List<List<float>> elevation_map, List<List<float>> regions_map, List<List<float>> water_map, GameObject perlin_map_object){
            
            TerrainHandler.generic_hex = generic_hex;

            hex_list = CreateHexObjects(map_size);

            SetHexElevation(elevation_map);

            SetHexRegion(regions_map);

            SetHexLand(water_map);
            
            SpawnHexTiles();

            //DebugHandler.InitializeDebugHexComponents(hex_list);
            //DebugHandler.ShowElevationTypes(GetHexList());
            DebugHandler.ShowRegionTypes(GetHexList());
            DebugHandler.ShowOceanTypes(GetHexList());
            //DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, new List<List<List<float>>>(){elevation_map, regions_map, water_map});
        }

        private static List<Hex> CreateHexObjects(Vector2 map_size){
            List<Hex> hex_list = new List<Hex>();

            // Create Hex objects for each column and row in the map
            for(int column = 0; column < map_size.x; column++)
            {
                for(int row = 0; row < map_size.y; row++)
                {
                    Hex hex = new Hex(column, row);
                    hex_list.Add(hex);
                }
            }

            return hex_list;
        }

        private static void SetHexLand(List<List<float>> land_map)
        {
            foreach(Hex hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetLandType(TerrainUtils.GetLandType(land_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        private static void SetHexElevation(List<List<float>> elevation_map){
            foreach(Hex hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetElevation(elevation_map[ (int) coordinates.x][ (int) coordinates.y], TerrainUtils.GetElevationType(elevation_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        private static void SpawnHexTiles(){
            foreach(Hex hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
            }
        }

        public static List<Hex> GetHexList(){
            return hex_list;
        }

        private static void SetHexRegion(List<List<float>> regions_map){
            foreach(Hex hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetRegionType(TerrainUtils.GetRegionType(regions_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }












    }
}