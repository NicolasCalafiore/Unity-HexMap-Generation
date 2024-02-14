using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;

namespace Players {
    public class Player {
        /*
            Player class is used to store information about the player
        */
        private static int player_count = 10; //**
        public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
        private static List<Player> player_list = new List<Player>();
        private static Player player_view; //**


        public string state_prefix;
        public string name;
        public Color team_color;
        public int id;
        public List<City> cities = new List<City>();
        public List<List<float>> fog_of_war_map;
        public GovernmentEnums.GovernmentType government_type;
        public Government government;
        public float wealth = 1000;
        public int knowledge_points = 0;
        public int heritage_points = 0;
        public int belief_points = 0;


        public Player(string state_prefix, string name, int id){
            this.state_prefix = state_prefix;
            this.name = name;


             this.team_color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));

            this.id = id;

            Player.player_id_to_player.Add(id, this);
        }

        public void GovernmentSimulation(MapManager map_generation){
            //List<List<float>> territory_map = TerrainUtils.GenerateMap(map_generation.GetMapSize(), 0);
            //DebugHandler.PrintMapDebug("Territory Map", territory_map);   //TO DO: REEVALUATE EFFECT

            List<List<float>> territory_map = map_generation.territory_map_handler.territory_map;
            government.cabinet.StartDomesticTurn(territory_map, id, fog_of_war_map);
            government.cabinet.StartForeignTurn(territory_map, id, fog_of_war_map);
        }

        public void AddCity(City city){
            cities.Add(city);
        }

        public void SetFogOfWarMap(List<List<float>> fog_of_war_map){
            this.fog_of_war_map = fog_of_war_map;
        }

        public void SetStatePrefix(string state_prefix){
            this.state_prefix = state_prefix;
        }

        public void SetStateName(string name){
            this.name = name;
        }

        public string GetOfficialName(){
            return state_prefix + " " + name;
        }

        internal void SetGovernmentType(GovernmentEnums.GovernmentType governmentType)
        {
            this.government_type = governmentType;
        }

        public void SetGovernment(Government government){
            this.government = government;
        }

        public Government GetGovernment(){
            return government;
        }

        public City GetCityByIndex(int index){
            return cities[index];
        }

        //**

        public static void GeneratePlayers(){
        CreatePlayers(0);
        SetGovernmentTypes(0);
        SetStatePrefix();
    }

        public static void CreatePlayers(int player_id){        // TO DO: EVALUATE FUNCTIONAL PROGRAMMING
            if(player_id < player_count){
                player_list.Add(new Player("player", "Player " + player_id, player_id));
                Debug.Log("Player " + player_id + " created");
                CreatePlayers(player_id + 1);
            }
        }

        private static void SetGovernmentTypes(int index){
            if(index < player_list.Count){
                int random_index = UnityEngine.Random.Range(1, GovernmentEnums.GetGovernmentTypes().Count);
                player_list[index].SetGovernmentType(GovernmentEnums.GetGovernmentTypes()[random_index]);
                SetGovernmentTypes(index + 1);
            }
        }

        //Sets the state's government-related prefix
        private static void SetStatePrefix()
        {
            foreach(Player player in player_list){
                List<string> state_prefixes = IOHandler.ReadPrefixNamesRegionSpecified(player.government_type.ToString());
                int random_index = UnityEngine.Random.Range(0, state_prefixes.Count);
                string state_prefix = state_prefixes[random_index];
                player.SetStatePrefix(state_prefix);
            }
        }

        //
        public static void SetStateNames()
        {
            List<HexTile> hex_list = HexTile.GetHexList();

            foreach(Player player in player_list){
                Vector2 capital_coordinates = player.GetCityByIndex(0).GetColRow();
                HexTile capital_hex = HexTile.col_row_to_hex[capital_coordinates];

                //Given player get capitals hextile

                RegionsEnums.HexRegion hex_region = capital_hex.GetRegionType();

                List<string> state_names = IOHandler.ReadStateNamesRegionSpecified(hex_region.ToString());

                int random_index = UnityEngine.Random.Range(0, state_names.Count);
                string state_name = state_names[random_index];

                player.SetStateName(state_name);

            }
        }


        public static void GenerateGovernments(){   
            foreach(Player player in player_list){
                Government government = new Government(player.government_type);
                player.SetGovernment(government);
            }
        }


        public static Player GetPlayerView(){
            return player_view;
        }

        public static void NextPlayer(){
            if(player_view == player_list[player_list.Count - 1]){
                player_view = player_list[0];
            }
            else{
                player_view = player_list[player_list.IndexOf(player_view) + 1];
            }

                Debug.Log("Printing player view government and advisors:");
                DebugHandler.DisplayCharacter(player_view.GetGovernment().GetLeader());
                DebugHandler.DisplayCharacter(player_view.GetGovernment().GetDomestic(0));
                DebugHandler.DisplayCharacter(player_view.GetGovernment().GetForeign(0));
        }

        public static List<Player> GetPlayerList(){
            return player_list;
        }

        public static void SetPlayerView(int index){
            player_view = player_list[index];
        }

    }
}