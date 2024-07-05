using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Cities;
using Character;
using TMPro;
using Terrain;




namespace Graphics {

    public static class  HexGraphicManager
    {     
        public static GameObject generic_hex;
        public static Dictionary<GameObject, Color> hex_color = new Dictionary<GameObject, Color>();
        public static List<GameObject> hex_go_list = new List<GameObject>(); 
        public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>(); 
        public static Dictionary<GameObject, HexTile> hex_go_to_hex = new Dictionary<GameObject, HexTile>();
        public static Dictionary<Vector2, GameObject> col_row_to_hex_go = new Dictionary<Vector2, GameObject>();  

        public static void Spawn(HexTile hex){
            GameObject hex_go = GameObject.Instantiate(generic_hex);
            hex_go.transform.position = hex.GetWorldPosition();
            Add(hex, hex_go);
        }
        public static void Add(HexTile hex, GameObject hex_object){
            Debug.Log("Adding hex: " + hex.GetColRow().x + " " + hex.GetColRow().y);
            
            hex_go_list.Add(hex_object);
            hex_to_hex_go.Add(hex, hex_object);
            col_row_to_hex_go.Add(hex.GetColRow(), hex_object);
            hex_go_to_hex.Add(hex_object, hex);

            hex_object.name = "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y;
        }

        public static GameObject GetHexGoByHex(HexTile hex) => hex_to_hex_go[hex];
        public static HexTile GetHexByHexGo(GameObject hex_go) => hex_go_to_hex[hex_go];
        public static GameObject GetHexGoByColRow(Vector2 col_row) => col_row_to_hex_go[col_row];
        public static HexTile GetHexByColRow(Vector2 col_row) => hex_go_to_hex[col_row_to_hex_go[col_row]];
        public static void ClearHexColor() => hex_color.Clear();
        public static void AddHexColor(GameObject hex_object) => hex_color.Add(hex_object, hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color);
        public static Dictionary<GameObject, Color> GetHexColor() => hex_color;
        
        
        
        

    }
}