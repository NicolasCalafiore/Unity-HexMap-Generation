using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using UnityEngine;



public static class DebugHandler
{

    /*
        DebugHandler is used to spawn debug viewers
        DebugHandler is used to print debug messages
    */


    public static void GetHexInformation(GameObject gameObject){  // Used to read HexTile object from GameObject from MouseInputHandler
        string MESSAGE = "Hex Information: \n";
        GameObject hex_go = gameObject.transform.parent.gameObject;
        HexTile hex = TerrainGameHandler.hex_to_hex_go.FirstOrDefault(x => x.Value == hex_go).Key;
        MESSAGE += "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y + "\n";
        MESSAGE += "Elevation: " + hex.GetPosition().y  + "\n";
        MESSAGE += "Elevation Type: " + hex.elevation_type  + "\n";
        MESSAGE += "Region Type: " + hex.region_type  + "\n";
        MESSAGE += "Land Type: " + hex.land_type + "\n";
        MESSAGE += "Feature Type: " + hex.feature_type + "\n";
        MESSAGE += "Resource Type: " + hex.resource_type + "\n";
        MESSAGE += "Structure Type: " + hex.structure_type + "\n";
        MESSAGE += "Nourishment: " + hex.nourishment + "\n";
        MESSAGE += "Construction: " + hex.construction + "\n";
        MESSAGE += "Movement Cost:" + hex.MovementCost + "\n";

        if(hex.owner_player != null){
            MESSAGE += "Player Owner: " + hex.owner_player.name + "\n";
        }
        else{
            MESSAGE += "Player Owner: None\n";
        }

        if(hex.owner_city != null){
            MESSAGE += "City Owner: " + hex.owner_city.GetName() + "\n";
        }
        else{
            MESSAGE += "City Owner: None\n";
        }
        Debug.Log(MESSAGE);
    }

    public static void PrintMapDebug(string title,  List<List<float>> map){ // Used to print List<List<float>> maps
        string message = title + "\n";
        foreach(List<float> row in map){
            foreach(float value in row){
                message += value + " ";
            }
            message += "\n";
        }

        Debug.Log(message);
    }
    // public static void SetHexAsChildren(MapGeneration map_generation){
    // foreach(HexTile i in map_generation.GetHexList()){
    //     GameObject hex_go = TerrainHandler.hex_to_hex_go[i];
    //     //hex_go.transform.SetParent(map_generation.transform);
    //     hex_go.name = "Hex - " + i.GetColRow().x + "_" + i.GetColRow().y + " - " + i.GetRegionType() + " - " + i.GetElevationType();
    //     }   
    // }


    internal static void GetPlayerInformation(GameObject city_collider)
    {
        string MESSAGE = "Player Information: \n";
        GameObject city_go = city_collider.transform.parent.gameObject;
        City city = TerrainGameHandler.city_go_to_city[city_go];
        Player player = GameManager.player_id_to_player[city.GetPlayerId()];
        MESSAGE += "Player State: " +  player.GetOfficialName() + "\n";
        MESSAGE += "Government Type: " + player.government_type + "\n";
        MESSAGE += "Color: " + player.team_color + "\n";
        MESSAGE += "Cities: " + player.cities.Count + "\n";
        MESSAGE += "Wealth: " + player.wealth + "\n";
        MESSAGE += "Knowledge: " + player.knowledge_points + "\n";
        MESSAGE += "Heritage: " + player.heritage_points + "\n";
        MESSAGE += "Belief: " + player.belief_points + "\n";
                
        Debug.Log(MESSAGE);
    }


}
