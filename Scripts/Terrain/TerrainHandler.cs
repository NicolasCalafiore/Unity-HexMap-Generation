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

        public static void SpawnTerrain(Vector2 map_size, List<List<float>> elevation_map, List<List<float>> regions_map, List<List<float>> water_map, List<List<float>> features_map){
            
            generic_hex = Resources.Load<GameObject>("Prefab/Hex_Generic_No_TMP");

            hex_list = CreateHexObjects(map_size);

            SetHexElevation(elevation_map);

            SetHexRegion(regions_map);

            SetHexLand(water_map);
            
            SetHexFeatures(features_map);
            
            SpawnHexTiles();

            InitializeDebugComponents();
        }

        private static void InitializeDebugComponents(){
            DebugHandler.ShowRegionTypes(GetHexList());
            DebugHandler.ShowOceanTypes(GetHexList());
            //DebugHandler.InitializeDebugHexComponents(hex_list);
            //DebugHandler.ShowElevationTypes(GetHexList());
            //DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, new List<List<List<float>>>(){elevation_map, regions_map, water_map});
        }

        private static void SetHexFeatures(List<List<float>> features_map)
        {
            foreach(Hex hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetFeatureType(EnumHandler.GetFeatureType(features_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
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
                hex.SetLandType(EnumHandler.GetLandType(land_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        private static void SetHexElevation(List<List<float>> elevation_map){
            foreach(Hex hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetElevation(elevation_map[ (int) coordinates.x][ (int) coordinates.y], EnumHandler.GetElevationType(elevation_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        private static void SpawnHexTiles(){
            foreach(Hex hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                SpawnHexFeature(hex, hex_object);
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
            }
        }

        private static void SpawnHexFeature(Hex hex, GameObject hex_object){

                GameObject feature = null;

                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Forest){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Forest"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Oasis){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Oasis"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.WheatField){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Wheat"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Rocks){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Rocks"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Jungle){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Jungle"));
                }
                if(feature != null){
                    feature.transform.SetParent(hex_object.transform);
                    float local_position_y = hex_object.transform.GetChild(0).transform.localPosition.y;
                    feature.transform.localPosition = new Vector3(0, local_position_y, 0);
                }

        }

        public static List<Hex> GetHexList(){
            return hex_list;
        }

        private static void SetHexRegion(List<List<float>> regions_map){
            foreach(Hex hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetRegionType(EnumHandler.GetRegionType(regions_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }












    }
}