using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Terrain;
using TMPro;
using UnityEngine;

namespace Terrain{
    public static class TerrainHandler
    {
        private static GameObject generic_hex;
        private static List<GameObject> hex_go_list = new List<GameObject>();
        public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>();

        public static void SpawnTerrain(Vector2 map_size, List<HexTile> hex_list){
            
            generic_hex = Resources.Load<GameObject>("Prefab/Hex_Generic_No_TMP");
            
            SpawnHexTiles(hex_list);

            InitializeDebugComponents(hex_list);
        }

        private static void SpawnHexTiles(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                SpawnHexFeature(hex, hex_object);
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
            }
        }


        private static void InitializeDebugComponents( List<HexTile> hex_list){
            DebugHandler.ShowRegionTypes(hex_list);
            DebugHandler.ShowOceanTypes(hex_list);
            //DebugHandler.InitializeDebugHexComponents(hex_list);
            //DebugHandler.ShowElevationTypes(GetHexList());
            //DebugHandler.SpawnPerlinViewers(map_size, perlin_map_object, new List<List<List<float>>>(){elevation_map, regions_map, water_map});
        }

      

        private static void SpawnHexFeature(HexTile hex, GameObject hex_object){

                GameObject feature = null;

                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Forest){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Forest"));
                    hex = new ForestDecorator(hex);
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Oasis){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Oasis"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.WheatField){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Wheat"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Rocks){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Rocks"));
                    hex = new RockDecorator(hex);
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Jungle){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Jungle"));
                    hex = new JungleDecorator(hex);
                }
                if(feature != null){
                    feature.transform.SetParent(hex_object.transform);
                    float local_position_y = hex_object.transform.GetChild(0).transform.localPosition.y;
                    feature.transform.localPosition = new Vector3(0, local_position_y, 0);
                }

        }












    }
}