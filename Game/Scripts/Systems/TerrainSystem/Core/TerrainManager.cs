using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Cities;
using Players;
using Terrain;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain{
    public static class TerrainManager
    {
        // TerrainManager is used to spawn all terrain GameObjects into GameWorld
        // Manages all terrain GameObjects

        public static List<GameObject> hex_go_list = new List<GameObject>(); 
        public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>(); 
        public static Dictionary<GameObject, City> city_go_to_city = new Dictionary<GameObject, City>();
        public static Dictionary<Vector2, GameObject> col_row_to_hex_go = new Dictionary<Vector2, GameObject>(); 
        private static List<GameObject> alerts_go_list = new List<GameObject>();
        private static List<GameObject> line_renderer_list = new List<GameObject>(); 
        private static GameObject generic_hex;
        private const int PERLIN_VISUAL_SCALE = 15;

        private static Dictionary<ForeignEnums.RelationshipLevel, Color> relationship_color = new Dictionary<ForeignEnums.RelationshipLevel, Color>(){
            {ForeignEnums.RelationshipLevel.Enemy, Color.red},
            {ForeignEnums.RelationshipLevel.Unfriendly, Color.yellow},
            {ForeignEnums.RelationshipLevel.Neutral, Color.gray},
            {ForeignEnums.RelationshipLevel.Friendly, Color.green},
            {ForeignEnums.RelationshipLevel.Ally, Color.blue}
        };

        public static void SpawnTerrain(){ 

            generic_hex = Resources.Load<GameObject>("Prefab/Core/Hex_Generic_No_TMP"); 
            
            SpawnHexTiles();

            InitializeVisuals(); 
        }



        private static void SpawnHexTiles(){                     // Spawns all generic_hex GameObjects into GameWorld    
            foreach(HexTile hex in HexManager.hex_list){
                GameObject hex_object = GameObject.Instantiate(generic_hex);
                hex_object.transform.position = hex.GetWorldPosition();
                hex_go_list.Add(hex_object);
                hex_to_hex_go.Add(hex, hex_object);
                col_row_to_hex_go.Add(hex.GetColRow(), hex_object);
                hex_object.name = "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y;
            }
        }

        private static void InitializeVisuals(){

            List<List<float>> visual_map = GeneratePerlinVisuals();                
            foreach(HexTile hex in HexManager.hex_list){
                ShowRegionTypes(hex);                                         
                ShowOceanTypes(hex);                                          
                SpawnHexFeature(hex);                                         
                SpawnHexResource(hex);                                        
                SpawnTerritoryFlags(hex);                                     
                SpawnPerlinTerrainGraphics(hex, visual_map);                              
            }
        }

        private static List<List<float>> GeneratePerlinVisuals()
        {
            List<List<float>> map = MapUtils.GenerateMap();
            MapUtils.GeneratePerlinNoiseMap(map, MapManager.GetMapSize(), PERLIN_VISUAL_SCALE);
            MapUtils.NormalizePerlinMap(map);
            MapUtils.RatioPerlinMap(.4f, map);
            DebugHandler.Print2DMap(map);

            return map;
        }

        private static void SpawnHexFeature(HexTile hex){

            if(hex.feature_type == FeaturesEnums.HexNaturalFeature.None) return;

            GameObject hex_object = hex_to_hex_go[hex];
            GameObject feature = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Natural_Features/" + hex.feature_type.ToString()));
            feature.transform.SetParent(hex_object.transform);
            feature.transform.localPosition = new Vector3(0, 0, 0);
        }

        private static void SpawnHexResource(HexTile hex){

            if(hex.resource_type == ResourceEnums.HexResource.None) return;

            GameObject hex_object = hex_to_hex_go[hex];
            GameObject resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/" + hex.resource_type.ToString()));
            resource.transform.SetParent(hex_object.transform);
            resource.transform.localPosition = new Vector3(0, 0, 0);
        }


        public static void ShowRegionTypes(HexTile hex){
            if(hex.land_type == LandEnums.LandType.Water) return;
            hex_to_hex_go[hex].transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.region_type.ToString());
        }

        public static void ShowOceanTypes(HexTile hex){
            if(hex.land_type != LandEnums.LandType.Water) return;
            hex_to_hex_go[hex].transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.region_type.ToString());
        }

        private static void SpawnPerlinTerrainGraphics(HexTile hex, List<List<float>> map){

            GameObject hex_object = hex_to_hex_go[hex];
            Vector2 hex_col_row = hex.GetColRow();

            Color hex_color = hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
            float color_change = map[ (int) hex_col_row.x][ (int) hex_col_row.y];
            hex_color -= new Color(color_change, color_change, color_change, 0f);
            hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex_color;

        }

        public static void SpawnStructures()  
        {
            int counter = 0;
            List<HexTile> hex_list = HexManager.hex_list;

            foreach(HexTile hex in hex_list)
                if(hex.structure_type == StructureEnums.StructureType.Capital) counter = SpawnCapital(counter, hex);
            
        }

        private static int SpawnCapital(int counter, HexTile hex){
            GameObject hex_object = hex_to_hex_go[hex];  
            GameObject structure_go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/City"));
            
            city_go_to_city.Add(structure_go, CityManager.GetCapitalsList()[counter]);
            CityManager.city_to_city_go.Add(CityManager.GetCapitalsList()[counter], structure_go);
            counter++;
            
            city_go_to_city[structure_go].name = CityManager.GenerateCityName(hex); 
            Player player =  city_go_to_city[structure_go].owner_player;

            structure_go.transform.SetParent(hex_object.transform);
            structure_go.transform.localPosition = new Vector3(0, 0, 0);
            structure_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = city_go_to_city[structure_go].name;
            structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = player.GetOfficialName();
            structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().color = player.team_color;


            var CapitalFlags = structure_go.transform.GetChild(3);

            foreach (Transform child in CapitalFlags)
            {
                child.GetChild(0).GetComponent<MeshRenderer>().material.color = player.team_color;
            }

            return counter;;
        }

        public static void SpawnTerritoryFlags(HexTile hex){

            if(hex.owner_player == null) return;

            GameObject hex_object = TerrainManager.hex_to_hex_go[hex];
            GameObject territory_flag = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Territory_Flag"));
            
            territory_flag.transform.SetParent(hex_object.transform);
            territory_flag.transform.localPosition = new Vector3(0, 0, 0);
            territory_flag.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex.owner_player.team_color;
        }

        public static void SpawnConstructionAlert(HexTile hex){
            
            GameObject alert = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Construction_Alert"));
            GameObject hex_object = hex_to_hex_go[hex];

            alert.transform.SetParent(hex_object.transform);
            alert.transform.localPosition = new Vector3(0, 0, 0);

            alerts_go_list.Add(alert);
        }

        public static void SpawnExpansionAlert(HexTile hextile){
            
            GameObject alert = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Expansion_Alert"));
            GameObject hex_object = hex_to_hex_go[hextile];

            alert.transform.SetParent(hex_object.transform);
            alert.transform.localPosition = new Vector3(0, 0, 0);

            alerts_go_list.Add(alert);
        }

        public static void RemoveAlerts(){

            if(alerts_go_list.Count == 0) return;

            for (int i = alerts_go_list.Count - 1; i >= 0; i--) //Cannot use foreach because we are removing items from the list being iterated over
            {
                GameObject.Destroy(alerts_go_list[i]);
                alerts_go_list.RemoveAt(i);
            }
        }

        public static void DrawForeignLine(GameObject capital_city, GameObject foreign_city, float relationship_float = 0){

            Color color = relationship_color[ForeignEnums.GetRelationshipLevel(relationship_float)];
            
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
            lineRenderer.endWidth = .001f;
            lineRenderer.material.color = color;
            line_renderer_list.Add(line_render_object);
        }

        public static void RemoveForeignLines(){
            if(line_renderer_list.Count == 0) return;

            for (int i = line_renderer_list.Count - 1; i >= 0; i--) //Cannot use foreach because we are removing items from the list being iterated over
            {
                GameObject.Destroy(line_renderer_list[i]);
                line_renderer_list.RemoveAt(i);
            }
        }

        public static void SpawnAIFlags(Player player = null){
            RemoveAlerts();
            RemoveForeignLines();

            if(player == null) player = PlayerManager.player_view;
        
            List<HexTile> construction_tiles = player.government.GetDomesticByIndex(0).GetPotentialConstructionTiles();
            List<HexTile> expansion_tiles = player.government.GetDomesticByIndex(0).GetPotentialExpansionTiles();
            List<Player> known_players = player.government.GetForeignByIndex(0).GetKnownPlayers();

            foreach(HexTile hex_tile in construction_tiles){
                SpawnConstructionAlert(hex_tile);   // TO DO: FIX CONSTRUCTION BUG (NOT SHOWING UP)
            }

            foreach(HexTile hex_tile in expansion_tiles){
                SpawnExpansionAlert(hex_tile);
            }

            foreach(Player i in known_players){
                City foreign_capital_city = i.GetCapital();
                GameObject foreign_capital_city_go = CityManager.city_to_city_go[foreign_capital_city];
                
                City capital_city = player.GetCapital();
                GameObject capital_city_go = CityManager.city_to_city_go[capital_city];

                float relationship_float = player.government.GetForeignByIndex(0).GetRelationshipFloat(i);
                DrawForeignLine(capital_city_go, foreign_capital_city_go, relationship_float);
            }
        }

        public static void ShowRelationships(){
            
            List<Player> visible_players = FogManager.GetVisiblePlayers();
            
            foreach(Player i in visible_players){
                List<Player> known_players_2 = i.government.GetForeignByIndex(0).GetKnownPlayers();
                foreach(Player j in known_players_2){
                    
                City foreign_capital_city = j.GetCapital();
                GameObject foreign_capital_city_go = CityManager.city_to_city_go[foreign_capital_city];
                
                City capital_city = i.GetCapital();
                GameObject capital_city_go = CityManager.city_to_city_go[capital_city];

                float relationship = i.government.GetForeignByIndex(0).GetRelationshipFloat(j);
                DrawForeignLine(capital_city_go, foreign_capital_city_go, relationship);
                }
            }
        }
    }
}