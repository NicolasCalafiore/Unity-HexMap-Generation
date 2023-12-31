using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Terrain;
using TMPro;
using Unity.VisualScripting;
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

            InitializeVisuals(hex_list);
        }

        private static void SpawnHexTiles(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                SpawnHexFeature(hex, hex_object);
                SpawnHexResource(hex, hex_object);
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
            }
        }

        private static void InitializeVisuals( List<HexTile> hex_list){
            ShowRegionTypes(hex_list);
            ShowOceanTypes(hex_list);

        }
        private static void SpawnHexFeature(HexTile hex, GameObject hex_object){

                GameObject feature = null;

                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Forest){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Features/Forest"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Oasis){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Features/Oasis"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.WheatField){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Features/Wheat"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Rocks){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Features/Rocks"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Jungle){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Features/Jungle"));
                }
                if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Swamp){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Features/Swamp"));
                }

                if(feature != null){
                    feature.transform.SetParent(hex_object.transform);
                    float local_position_y = hex_object.transform.GetChild(0).transform.localPosition.y;
                    feature.transform.localPosition = new Vector3(0, local_position_y, 0);
                }

        }

    private static void SpawnHexResource(HexTile hex, GameObject hex_object){
        GameObject resource = null;

        if(hex.GetResourceType() == EnumHandler.HexResource.Iron){
            resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Iron"));
        }

        if(resource != null){
            resource.transform.SetParent(hex_object.transform);
            float local_position_y = hex_object.transform.GetChild(0).transform.localPosition.y;
            resource.transform.localPosition = new Vector3(0, local_position_y, 0);
        }

    }


    public static void ShowRegionTypes(List<HexTile> hex_list){
        foreach(HexTile hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            if(hex.GetLandType() != EnumHandler.LandType.Water){
                if(hex.GetRegionType() == EnumHandler.HexRegion.Desert){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Desert");
                }

                if(hex.GetRegionType() == EnumHandler.HexRegion.Plains){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Savannah");
                }

                if(hex.GetRegionType() == EnumHandler.HexRegion.Grassland){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Grassland");
                }
                
                if(hex.GetRegionType() == EnumHandler.HexRegion.Tundra){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Tundra");
                }
                if(hex.GetRegionType() == EnumHandler.HexRegion.Highlands){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Highlands");
                }
                if(hex.GetRegionType() == EnumHandler.HexRegion.Jungle){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Jungle");
                }
                if(hex.GetRegionType() == EnumHandler.HexRegion.Swamp){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Swamp");
                }
                
            }

        }
    }

        public static void ShowOceanTypes(List<HexTile> hex_list){
        foreach(HexTile hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            if(hex.region_type == EnumHandler.HexRegion.Ocean){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Ocean");
            }
            else if(hex.region_type == EnumHandler.HexRegion.River){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/River");
            }

        }
        
    }









    }
}