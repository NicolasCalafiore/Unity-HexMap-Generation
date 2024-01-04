using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strategy.Assets.Game.Scripts.Terrain;
using Terrain;
using TMPro;
using UnityEngine;



public static class DebugHandler
{


    public static void GetHexFromInput(GameObject gameObject){
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
        MESSAGE += "Food: " + hex.food + "\n";
        MESSAGE += "Production: " + hex.production + "\n";
        MESSAGE += "Movement Cost:" + hex.MovementCost + "\n";
        Debug.Log(MESSAGE);
    }

    public static void PrintMapDebug(string title,  List<List<float>> map){
        string message = title + "\n";
        foreach(List<float> row in map){
            foreach(float value in row){
                message += value + " ";
            }
            message += "\n";
        }

        Debug.Log(message);
    }


    public static void SpawnPerlinViewers(Vector2 map_size,  List<List<float>> map, string name){
            PerlinViewer pv = new PerlinViewer(map_size, map, name);

    }

    public static void SetHexAsChildren(MapGeneration map_generation){
    foreach(HexTile i in map_generation.GetHexList()){
        GameObject hex_go = TerrainHandler.hex_to_hex_go[i];
        //hex_go.transform.SetParent(map_generation.transform);
        hex_go.name = "Hex - " + i.GetColRow().x + "_" + i.GetColRow().y + " - " + i.GetRegionType() + " - " + i.GetElevationType();
        }   
    }

}
