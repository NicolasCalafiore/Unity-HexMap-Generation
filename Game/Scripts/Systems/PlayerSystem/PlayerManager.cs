using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cabinet;
using PlayerGovernment;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class PlayerManager
{
    /*
        PlayerManager is used to manipulate ALL players in the game.
    */

    public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
    private List<Player> player_list = new List<Player>();
    private int player_count = 10;
    private int player_view;
    public PlayerManager(){
        
    }

    
    public void GeneratePlayers(){
        CreatePlayers(0);
        SetGovernmentTypes(0);
        SetStatePrefix();
    }

    public void CreatePlayers(int player_id){        // TO DO: EVALUATE FUNCTIONAL PROGRAMMING
        if(player_id < player_count){
            player_list.Add(new Player("player", "Player " + player_id, player_id));
            CreatePlayers(player_id + 1);
        }
    }

    private void SetGovernmentTypes(int index){
        if(index < player_list.Count){
            int random_index = UnityEngine.Random.Range(1, EnumHandler.GetGovernmentTypes().Count);
            player_list[index].SetGovernmentType(EnumHandler.GetGovernmentTypes()[random_index]);
            SetGovernmentTypes(index + 1);
        }
    }

    //Sets the state's government-related prefix
    private void SetStatePrefix()
    {
        foreach(Player player in player_list){
            List<string> state_prefixes = IOHandler.ReadPrefixNamesRegionSpecified(player.government_type.ToString());
            int random_index = UnityEngine.Random.Range(0, state_prefixes.Count);
            string state_prefix = state_prefixes[random_index];
            player.SetStatePrefix(state_prefix);
        }
    }

    //
    public void SetStateName(HexManager hex_manager)
    {
        List<HexTile> hex_list = hex_manager.GetHexList();

        foreach(Player player in player_list){
            Vector2 capital_coordinates = player.GetCityByIndex(0).GetColRow();
            HexTile capital_hex = HexManager.col_row_to_hex[capital_coordinates];

            //Given player get capitals hextile

            EnumHandler.HexRegion hex_region = capital_hex.region_type;

            List<string> state_names = IOHandler.ReadStateNamesRegionSpecified(hex_region.ToString());

            int random_index = UnityEngine.Random.Range(0, state_names.Count);
            string state_name = state_names[random_index];

            player.SetStateName(state_name);

        }
    }


    public void GenerateGovernments(){   
        foreach(Player player in player_list){
            Government government = new Government(player.government_type);
            player.SetGovernment(government);
        }
    }


    public Player GetPlayerView(){
        return player_list[player_view];
    }

    public int GetPlayerViewIndex(){
        return player_view;
    }

    public List<Player> GetPlayerList(){
        return player_list;
    }

    



    

}
