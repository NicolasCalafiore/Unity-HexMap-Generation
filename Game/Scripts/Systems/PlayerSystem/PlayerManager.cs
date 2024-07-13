using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Character;
using static Terrain.GovernmentEnums;
using Cities;
using Diplomacy;
using Graphics;

namespace Players {
    public class PlayerManager {
        public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
        public static List<Player> player_list = new List<Player>();
        public static Player player_view; 
        private static int player_count = 100; 

        
        public static void GeneratePlayers(){
            CreatePlayers(0);
            SetGovernmentTypes(0);
            SetStateNameTitle();

            player_id_to_player.Add(-1, null);  //NULL PLAYER/ID=-1 --> None/Null Player
        }

        public static void CreatePlayers(int player_id){        // TO DO: EVALUATE FUNCTIONAL PROGRAMMING
            if(player_id < player_count){
                player_list.Add(new Player("ERRL", player_id));
                CreatePlayers(player_id + 1);
            }
        }


        public static void SimulateGovernments(){
            foreach(Player i in player_list)     
                i.SimulateGovernment();
        }

        private static void SetGovernmentTypes(int index){
            if(index >= player_list.Count) return;

            int random_index = UnityEngine.Random.Range(1, GetGovernmentTypes().Count);
            player_list[index].government_type = GetGovernmentTypes()[random_index];
            SetGovernmentTypes(index + 1);
        }
        private static void SetStateNameTitle()
        {
            foreach(Player player in player_list){  //TO DO: OPTIMIZATION POINT
                if(Random.Range(0, 100) < 50){
                    List<string> state_prefixes = IOHandler.ReadPrefixNamesRegionSpecified(player.government_type.ToString());
                    player.state_prefix = state_prefixes[Random.Range(0, state_prefixes.Count)];
                }
                else{
                    List<string> state_suffixes = IOHandler.ReadSuffixNamesRegionSpecified(player.government_type.ToString());
                    player.state_suffix = state_suffixes[Random.Range(0, state_suffixes.Count)];
                }
            }
        }

        public static void SetStateNames()
        {
            foreach(Player player in player_list){
                HexTile capital_hex = HexManager.col_row_to_hex[player.GetCapitalCoordinate()];
                List<string> state_names = IOHandler.ReadStateNamesRegionSpecified(capital_hex.region_type.ToString());
                player.name = state_names[Random.Range(0, state_names.Count)];
            }
        }

        //Creates Government Object Based on Government Type
        public static void GenerateGovernments(){   
            foreach(Player player in player_list){
                Government government = new Government(player.government_type);
                player.government = government;
            }
        }

        public static void SetPlayerView(Player player){
            player_view = player;

            DebugHandler.ClearLogConsole();
            player.CalculatePriorities(true);       //Print for DEBUG purposes
            UIManager.LoadPlayerName(player_view);
            CameraMovement.CenterCamera();
            GraphicsManager.SpawnAIFlags();
            UIManager.UpdatePlayerUI(player_view);  
        }

        public static void NextPlayer(){

            if(player_view == player_list[player_list.Count - 1])
                SetPlayerView(player_list[0]);
            else
                SetPlayerView(player_list[player_list.IndexOf(player_view) + 1]);
        }

        public static void AllScanForNewPlayers(){
            foreach(Player player in player_list)
                player.government.cabinet.foreign_advisor.ScanForNewPlayers(player.GetFogOfWarMap(), player);
        }

        public static void InitializePlayerPriorities(){
            foreach(Player player in player_list)
                player.CalculatePriorities();
            
        }
    }
}