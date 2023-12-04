using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using TMPro;
using UnityEngine;



public static class DebugHandler
{
    public static void InitializeDebugHexComponents(Hex hex, GameObject hex_go){
        // Set the text of the child TextMeshPro component to the hex's column and row, and elevation
        hex_go.isStatic = true; // Hex Empty Object 
        hex_go.transform.GetChild(0).gameObject.isStatic = true; //3D Hex Model
        hex_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = string.Format("{0},{1}" , hex.GetColRow().x, hex.GetColRow().y);
        hex_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = string.Format("{0}" , hex.GetPosition().y);
        hex_go.transform.GetChild(3).GetComponent<TextMeshPro>().text = string.Format("{0}" , hex.elevation_type);

        // Set the name of the hex game object for Unity
        hex_go.name = "Hex - " + hex.GetColRow().x + "_" + hex.GetColRow().y;
    }

    public static void ShowTerrainTypes(List<Hex> hex_list){
        foreach(Hex hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            if(hex.GetElevationType() == TerrainUtils.HexElevation.Canyon){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(195, 143, 2, 255);
            }
            else if(hex.GetElevationType() == TerrainUtils.HexElevation.Valley){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(255, 209, 81, 255);
            }
            else if(hex.GetElevationType() == TerrainUtils.HexElevation.Flatland){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(21, 195, 2, 255);
            }
            else if(hex.GetElevationType() == TerrainUtils.HexElevation.Hill){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(17, 155, 2, 255);
            }
            else if(hex.GetElevationType() == TerrainUtils.HexElevation.Large_Hill){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(11, 105, 0, 255);
            }
            else if(hex.GetElevationType() == TerrainUtils.HexElevation.Mountain){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(160, 160, 160, 255);
            }
        }

    }


}
