using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using DevionGames;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain{
    public static class TerrainManager
    {
        private static GameObject generic_hex;  //Grey Empty Hex-Object - No Region/Feature/Resource/Elevation Type
        public static List<GameObject> hex_go_list = new List<GameObject>();   // List of all Hex-Objects
        public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>(); // Given Hex gives Hex-Object
        public static Dictionary<GameObject, City> city_go_to_city = new Dictionary<GameObject, City>(); // Given City gives City-Game-Object
        public static Dictionary<Vector2, GameObject> col_row_to_hex_go = new Dictionary<Vector2, GameObject>(); // Given ColRow gives Hex-Object
        public static List<GameObject> alerts_go_list = new List<GameObject>(); // List of all Alerts
        public static List<GameObject> line_renderer_list = new List<GameObject>(); // List of all LineRenderers

        public static void SpawnTerrain(){ // Called from MapGeneration
            List<HexTile> hex_list = HexTile.GetHexList();
        
            generic_hex = Resources.Load<GameObject>("Prefab/Core/Hex_Generic_No_TMP"); 
            
            SpawnHexTiles(hex_list);    // Spawn all generic_hex GameObjects into GameWorld

            InitializeVisuals(hex_list); // Spawns all GameObjects that are not HexTile objects (Region/Feature/Resource/Elevation)
        }



        private static void SpawnHexTiles(List<HexTile> hex_list){                     // Spawns all generic_hex GameObjects into GameWorld    
            foreach(HexTile hex in hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetPosition();
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
                col_row_to_hex_go.Add(hex.GetColRow(), hex_object);
                hex_object.name = "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y;
            }
        }

        private static void InitializeVisuals( List<HexTile> hex_list){                // Spawns all GameObjects that are not HexTile objects (Region/Feature/Resource/Elevation)
            ShowRegionTypes(hex_list);                                          // Set Region Types
            ShowOceanTypes(hex_list);                                           // Set Ocean Types 
            SpawnHexFeature(hex_list);                                          // Spawn Features
            SpawnHexResource(hex_list);                                         // Spawn Resources    
            SpawnTerritoryFlags(hex_list);                                       // Spawn Capitals
            SpawnElevationGraphics(hex_list);                                   // Spawn Elevation Graphics

        }
        private static void SpawnHexFeature(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainManager.hex_to_hex_go[hex];
                GameObject feature = null;

                if(hex.GetFeatureType() != FeaturesEnums.HexNaturalFeature.None){
                    feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Natural_Features/" + hex.GetFeatureType().ToString()));
                }

                if(feature != null){
                    feature.transform.SetParent(hex_object.transform);
                    feature.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
        }

        private static void SpawnHexResource(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainManager.hex_to_hex_go[hex];
                GameObject resource = null;

                if(hex.GetResourceType() != ResourceEnums.HexResource.None) 

                    resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/" + hex.GetResourceType().ToString()));

                if(resource != null){
                    resource.transform.SetParent(hex_object.transform);
                    resource.transform.localPosition = new Vector3(0, 0, 0);
                }
            }

        }


        public static void ShowRegionTypes(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_go = hex_to_hex_go[hex];
                if(hex.GetLandType() != LandEnums.LandType.Water){
                        hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.GetRegionType().ToString());
                }

            }
        }

        public static void ShowOceanTypes(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_go = hex_to_hex_go[hex];
                if(hex.GetLandType() == LandEnums.LandType.Water){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.GetRegionType().ToString());
                }

            }
            
        }

        public static void SpawnStructures()   // Spawns all capitals into GameWorld
        {
            int counter = 0;
            List<HexTile> hex_list = HexTile.GetHexList();

            foreach(HexTile hex in hex_list){

                GameObject hex_object = hex_to_hex_go[hex];  // Get hex GameObject from hex_to_hex_go Dictionary
                GameObject structure_go = null; // GameObject to be spawned (Capital)

                if(hex.GetStructureType() == StructureEnums.StructureType.Capital){    // If hex is tagged as a capital, spawn capital
                    structure_go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/City"));
                    city_go_to_city.Add(structure_go, City.GetCapitalsList()[counter]);
                    City.city_to_city_go.Add(City.GetCapitalsList()[counter], structure_go);
                    counter++;


                }
                if(structure_go != null){   // If structure_go is not null, spawn structure_go
                    city_go_to_city[structure_go].SetName(City.GenerateCityName(hex)); // Set city name

                    structure_go.transform.SetParent(hex_object.transform); // Set parent to hex_object --> Spawn structure on hex_game_object
                    structure_go.transform.localPosition = new Vector3(0, 0, 0);
                    structure_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = city_go_to_city[structure_go].GetName();
                    structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = Player.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].GetOfficialName();
                    structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().color = Player.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].GetTeamColor();


                    var CapitalFlags = structure_go.transform.GetChild(3);

                    foreach (Transform child in CapitalFlags)
                    {
                        child.GetChild(0).GetComponent<MeshRenderer>().material.color = Player.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].GetTeamColor();
                    }
                    
                }
            }
        }

        public static void SpawnTerritoryFlags(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                GameObject hex_object = TerrainManager.hex_to_hex_go[hex];
                GameObject territory_flag = null;

                if(hex.GetOwnerPlayer() != null){
                    territory_flag = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Territory_Flag"));
                    territory_flag.transform.SetParent(hex_object.transform);
                    territory_flag.transform.localPosition = new Vector3(0, 0, 0);
                    territory_flag.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex.GetOwnerPlayer().GetTeamColor(); 

                }
            }
        }

        public static void SpawnConstructionAlert(HexTile hextile){
            GameObject alert = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Construction_Alert"));
            GameObject hex_object = TerrainManager.hex_to_hex_go[hextile];

            alert.transform.SetParent(hex_object.transform);
            alert.transform.localPosition = new Vector3(0, 0, 0);

            alerts_go_list.Add(alert);

        }

        public static void SpawnExpansionAlert(HexTile hextile){
            GameObject alert = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Expansion_Alert"));
            GameObject hex_object = TerrainManager.hex_to_hex_go[hextile];

            alert.transform.SetParent(hex_object.transform);
            alert.transform.localPosition = new Vector3(0, 0, 0);

            alerts_go_list.Add(alert);

        }

        public static void RemoveAlerts(){
            if(alerts_go_list.Count != 0){
                for (int i = alerts_go_list.Count - 1; i >= 0; i--) //Cannot use foreach because we are removing items from the list being iterated over
                {
                    GameObject.Destroy(alerts_go_list[i]);
                    alerts_go_list.RemoveAt(i);
                }
            }
        }


        public static void DrawForeignLine(GameObject capital_city, GameObject foreign_city, int relationship = 0){

            Color color = Color.white;

            if(relationship < 25) color = Color.red;
            else if(relationship < 50) color = Color.yellow;
            else if(relationship < 75) color = Color.green;
            else if(relationship < 100) color = Color.blue;
                

            GameObject line_render_object = new GameObject();
            line_render_object.transform.SetParent(capital_city.transform);
            line_render_object.transform.localPosition = new Vector3(0, 0, 0);
            line_render_object.AddComponent<LineRenderer>();

            LineRenderer lineRenderer = line_render_object.GetComponent<LineRenderer>();
              
            Vector3 capital_point = capital_city.transform.position;
            capital_point.y += .05f;

            Vector3 foreign_point = foreign_city.transform.position;
            foreign_point.y += .05f;

            lineRenderer.SetPosition(0, capital_point);
            lineRenderer.SetPosition(1, foreign_point);
            lineRenderer.startWidth = .1f;
            lineRenderer.endWidth = .1f;
            lineRenderer.material.color = color;

            line_renderer_list.Add(line_render_object);
        }

        public static void RemoveForeignLines(){
            if(line_renderer_list.Count != 0){
                for (int i = line_renderer_list.Count - 1; i >= 0; i--) //Cannot use foreach because we are removing items from the list being iterated over
                {
                    GameObject.Destroy(line_renderer_list[i]);
                    line_renderer_list.RemoveAt(i);
                }
            }
        }




        public static void SpawnAIFlags(){
            RemoveAlerts();
            RemoveForeignLines();
            Player player = Player.GetPlayerView();
        
            List<HexTile> construction_tiles = player.GetGovernment().GetDomestic(0).GetPotentialConstructionTiles();
            foreach(HexTile hex_tile in construction_tiles){
                SpawnConstructionAlert(hex_tile);   // TO DO: FIX CONSTRUCTION BUG (NOT SHOWING UP)
            }

            List<HexTile> expansion_tiles = player.GetGovernment().GetDomestic(0).GetPotentialExpansionTiles();
            foreach(HexTile hex_tile in expansion_tiles){
                SpawnExpansionAlert(hex_tile);
            }

            Dictionary<Player, int> relationships = player.GetGovernment().GetForeign(0).relations;
            foreach(KeyValuePair<Player, int> relationship in relationships){
                player.GetGovernment().cabinet.GetForeign(0).foreign_strategy.RelationshipBreakdown(player, relationship.Key);
            }
            

            List<Player> known_players = player.GetGovernment().GetForeign(0).GetKnownPlayers();
            foreach(Player i in known_players){
                City foreign_capital_city = i.GetCityByIndex(0);
                GameObject foreign_capital_city_go = City.city_to_city_go[foreign_capital_city];
                
                City capital_city = player.GetCityByIndex(0); 
                GameObject capital_city_go = City.city_to_city_go[capital_city];

                int relationship = player.GetGovernment().GetForeign(0).GetRelationship(i);
                TerrainManager.DrawForeignLine(capital_city_go, foreign_capital_city_go, relationship);
            }
        }

        public static void SpawnElevationGraphics(List<HexTile> hex_list){
                // TO DO: FINISH
            foreach(HexTile hex in hex_list){
                GameObject hex_object = hex_to_hex_go[hex];
                
                if(hex.GetElevationType() == ElevationEnums.HexElevation.Small_Hill){
                    Color hex_color = hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                    hex_color += new Color(.1f, .1f, .1f, 0f);
                    hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex_color;

                }

                if(hex.GetElevationType() == ElevationEnums.HexElevation.Valley || hex.GetElevationType() == ElevationEnums.HexElevation.Canyon){
                    Color hex_color = hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                    hex_color -= new Color(.1f, .1f, .1f, 0f);
                    hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex_color;
                }
            }
        }

    }
}