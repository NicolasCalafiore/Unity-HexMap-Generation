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
        string MESSAGE = "";
        GameObject hex_go = gameObject.transform.parent.gameObject;
        HexTile hex = TerrainHandler.hex_to_hex_go.FirstOrDefault(x => x.Value == hex_go).Key;
        MESSAGE += "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y + "\n";
        MESSAGE += "Elevation: " + hex.GetPosition().y  + "\n";
        MESSAGE += "Elevation Type: " + hex.GetElevationType()  + "\n";
        MESSAGE += "Region Type: " + hex.GetRegionType()  + "\n";
        MESSAGE += "Land Type: " + hex.GetLandType() + "\n";
        MESSAGE += "Feature Type: " + hex.GetFeatureType() + "\n";
        MESSAGE += "Resource Type: " + hex.GetResourceType() + "\n";
        MESSAGE += "Structure Type: " + hex.GetStructureType() + "\n";
        MESSAGE += "Food: " + hex.food + "\n";
        MESSAGE += "Production: " + hex.production + "\n";
        MESSAGE += "Movement Cost:" + hex.MovementCost + "\n";

        if(hex.GetOwnerPlayer() != null){
            MESSAGE += "Owner: " + hex.GetOwnerPlayer().GetName() + "\n";
        }
        else{
            MESSAGE += "Owner: None\n";
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

    internal static void GetCityInformation(GameObject city_collider)
    {
        string MESSAGE = "";
        GameObject city_go = city_collider.transform.parent.gameObject;
        City city = TerrainHandler.city_go_to_city[city_go];
        MESSAGE += "City Name: " + city.GetName() + "\n";
        MESSAGE += "Owner: " + GameManager.player_id_to_player[city.GetPlayerId()].GetName() + "\n";
        Debug.Log(MESSAGE);

        GetStateInformation(city_collider);
    }

    internal static void GetStateInformation(GameObject city_collider)
    {
        string MESSAGE = "";
        GameObject city_go = city_collider.transform.parent.gameObject;
        City city = TerrainHandler.city_go_to_city[city_go];
        MESSAGE += "Player State: " +  GameManager.player_id_to_player[city.GetPlayerId()].GetOfficialName() + "\n";
        MESSAGE += "Government Type: " + GameManager.player_id_to_player[city.GetPlayerId()].GetGovernmentType() + "\n";
        Debug.Log(MESSAGE);
    }


}
