using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Players;
using Terrain;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain{
    public class TerrainHandler
    {

        /*
            Uses List<List<floats>> maps passed in from MapGeneration
            Spawns all terrain GameObjects
            Spawns all GameObjects that are not HexTile objects
        */

        // public delegate void TerrainSpawnedEventHandler(object sender, EventArgs e);
        // public event TerrainSpawnedEventHandler TerrainSpawned;

        private static GameObject generic_hex;  //Grey Empty Hex-Object - No Region/Feature/Resource/Elevation Type
        private static List<GameObject> hex_go_list = new List<GameObject>();   // List of all Hex-Objects
        public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>(); // Given Hex gives Hex-Object

        public void SpawnTerrain(Vector2 map_size, List<HexTile> hex_list){ // Called from MapGeneration
        
            generic_hex = Resources.Load<GameObject>("Prefab/Hex_Generic_No_TMP"); 
            
            SpawnHexTiles(hex_list);    // Spawn all generic_hex GameObjects into GameWorld

            InitializeVisuals(hex_list); // Spawns all GameObjects that are not HexTile objects (Region/Feature/Resource/Elevation)
        

            //OnTerrainSpawned();
        }

        // protected virtual void OnTerrainSpawned()
        // {
        //     if(TerrainSpawned != null){
        //         TerrainSpawned(this, EventArgs.Empty);
        //     }
        //     else{
        //         Debug.Log("Event is null");
        //     }

        // }

        private void SpawnHexTiles(List<HexTile> hex_list){                     // Spawns all generic_hex GameObjects into GameWorld    
            foreach(HexTile hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
            }
        }

        private void InitializeVisuals( List<HexTile> hex_list){                // Spawns all GameObjects that are not HexTile objects (Region/Feature/Resource/Elevation)
            ShowRegionTypes(hex_list);                                          // Set Region Types
            ShowOceanTypes(hex_list);                                           // Set Ocean Types 
            SpawnHexFeature(hex_list);                                          // Spawn Features
            SpawnHexResource(hex_list);                                         // Spawn Resources                                           // Spawn Capitals

        }
        private void SpawnHexFeature(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainHandler.hex_to_hex_go[hex];
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
        }

        private void SpawnHexResource(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainHandler.hex_to_hex_go[hex];
                GameObject resource = null;

                if(hex.GetResourceType() == EnumHandler.HexResource.Iron){
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Iron"));
                }
                if(hex.GetResourceType() == EnumHandler.HexResource.Cattle){
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Cattle"));
                }
                if(hex.GetResourceType() == EnumHandler.HexResource.Gems){
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Gems"));
                }
                if(hex.GetResourceType() == EnumHandler.HexResource.Stone){
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Stone"));
                }
                if(hex.GetResourceType() == EnumHandler.HexResource.Bananas){
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Bananas"));
                }
                if(hex.GetResourceType() == EnumHandler.HexResource.Incense){
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/Incense"));
                }

                if(resource != null){
                    resource.transform.SetParent(hex_object.transform);
                    float local_position_y = hex_object.transform.GetChild(0).transform.localPosition.y;
                    resource.transform.localPosition = new Vector3(0, local_position_y, 0);
                }
            }

        }


        public void ShowRegionTypes(List<HexTile> hex_list){
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

        public void ShowOceanTypes(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
                if(hex.GetRegionType() == EnumHandler.HexRegion.Ocean){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Ocean");
                }
                else if(hex.GetRegionType() == EnumHandler.HexRegion.River){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/River");
                }

            }
            
        }

    }
}