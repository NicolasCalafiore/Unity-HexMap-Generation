using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain{
    public class TerrainGameHandler
    {

        /*
            Uses List<List<floats>> maps passed in from MapGeneration
            Spawns all terrain GameObjects
            Spawns all GameObjects that are not HexTile objects
        */

        // public delegate void TerrainSpawnedEventHandler(object sender, EventArgs e);
        // public event TerrainSpawnedEventHandler TerrainSpawned;

        private static GameObject generic_hex;  //Grey Empty Hex-Object - No Region/Feature/Resource/Elevation Type
        public static List<GameObject> hex_go_list = new List<GameObject>();   // List of all Hex-Objects
        public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>(); // Given Hex gives Hex-Object
        public static Dictionary<GameObject, City> city_go_to_city = new Dictionary<GameObject, City>(); // Given City gives City-Game-Object
        public static Dictionary<Vector2, GameObject> col_row_to_hex_go = new Dictionary<Vector2, GameObject>(); // Given ColRow gives Hex-Object

        public void SpawnTerrain(Vector2 map_size, List<HexTile> hex_list){ // Called from MapGeneration
        
            generic_hex = Resources.Load<GameObject>("Prefab/Core/Hex_Generic_No_TMP"); 
            
            SpawnHexTiles(hex_list);    // Spawn all generic_hex GameObjects into GameWorld

            InitializeVisuals(hex_list); // Spawns all GameObjects that are not HexTile objects (Region/Feature/Resource/Elevation)

        

        }



        private void SpawnHexTiles(List<HexTile> hex_list){                     // Spawns all generic_hex GameObjects into GameWorld    
            foreach(HexTile hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
                col_row_to_hex_go.Add(hex.GetColRow(), hex_object);
                hex_object.name = "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y;
            }
        }

        private void InitializeVisuals( List<HexTile> hex_list){                // Spawns all GameObjects that are not HexTile objects (Region/Feature/Resource/Elevation)
            ShowRegionTypes(hex_list);                                          // Set Region Types
            ShowOceanTypes(hex_list);                                           // Set Ocean Types 
            SpawnHexFeature(hex_list);                                          // Spawn Features
            SpawnHexResource(hex_list);                                         // Spawn Resources    
            SpawnTerritoryFlags(hex_list);                                       // Spawn Capitals

        }
        private void SpawnHexFeature(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainGameHandler.hex_to_hex_go[hex];
                GameObject feature = null;

                if(hex.feature_type != EnumHandler.HexNaturalFeature.None){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Natural_Features/" + hex.feature_type.ToString()));
                }

                if(feature != null){
                    feature.transform.SetParent(hex_object.transform);
                    feature.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
        }

        private void SpawnHexResource(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainGameHandler.hex_to_hex_go[hex];
                GameObject resource = null;

                if(hex.resource_type != EnumHandler.HexResource.None) 
                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/" + hex.resource_type.ToString()));
                

                if(resource != null){
                    resource.transform.SetParent(hex_object.transform);
                    resource.transform.localPosition = new Vector3(0, 0, 0);
                }
            }

        }


        public void ShowRegionTypes(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_go = hex_to_hex_go[hex];
                if(hex.land_type != EnumHandler.LandType.Water){
                        hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.region_type.ToString());
                }

            }
        }

        public void ShowOceanTypes(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_go = hex_to_hex_go[hex];
                if(hex.land_type == EnumHandler.LandType.Water){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.region_type.ToString());
                }

            }
            
        }

        public void SpawnStructures(List<HexTile> hex_list, List<City> capitals_list, CityManager city_manager)   // Spawns all capitals into GameWorld
        {
            int counter = 0;

            foreach(HexTile hex in hex_list){

                GameObject hex_object = hex_to_hex_go[hex];  // Get hex GameObject from hex_to_hex_go Dictionary
                GameObject structure_go = null; // GameObject to be spawned (Capital)

                if(hex.structure_type == EnumHandler.StructureType.Capital){    // If hex is tagged as a capital, spawn capital
                    structure_go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/City"));
                    city_go_to_city.Add(structure_go, capitals_list[counter]);
                    counter++;
                }
                if(structure_go != null){   // If structure_go is not null, spawn structure_go
                    city_go_to_city[structure_go].SetName(city_manager.GenerateCityName(hex)); // Set city name

                    structure_go.transform.SetParent(hex_object.transform); // Set parent to hex_object --> Spawn structure on hex_game_object
                    structure_go.transform.localPosition = new Vector3(0, 0, 0);
                    structure_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = city_go_to_city[structure_go].GetName();
                    structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = GameManager.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].GetOfficialName();
                    structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().color = GameManager.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].team_color;


                    var CapitalFlags = structure_go.transform.GetChild(3);

                    foreach (Transform child in CapitalFlags)
                    {
                        child.GetChild(0).GetComponent<MeshRenderer>().material.color = GameManager.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].team_color;
                    }
                    
                }
            }
        }

        public void SpawnTerritoryFlags(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainGameHandler.hex_to_hex_go[hex];
                GameObject territory_flag = null;

                if(hex.owner_player != null){
                    territory_flag = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Territory_Flag"));
                    territory_flag.transform.SetParent(hex_object.transform);
                    territory_flag.transform.localPosition = new Vector3(0, 0, 0);
                    territory_flag.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex.owner_player.team_color; 

                }
            }
        }

    }
}