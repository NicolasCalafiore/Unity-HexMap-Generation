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

        public static void GenerateHexAppeal(){

            GenerateHexIndependentAppeal();
            GenerateHexDependantAppeal();
        }

        private static void GenerateHexIndependentAppeal()
        {
            foreach(HexTile hex in HexManager.hex_list){
                if(hex.defense > 7) hex.appeal += 4;
                else if(hex.defense > 5) hex.appeal += 3;
                else if(hex.defense > 3) hex.appeal += 2;
                else if(hex.defense > 1) hex.appeal += 1;
                else hex.appeal += 0;

                if(hex.nourishment > 7) hex.appeal += 4;
                else if(hex.nourishment > 5) hex.appeal += 3;
                else if(hex.nourishment > 3) hex.appeal += 2;
                else if(hex.nourishment > 1) hex.appeal += 1;
                else hex.appeal += 0;

                if(hex.construction > 7) hex.appeal += 4;
                else if(hex.construction > 5) hex.appeal += 3;
                else if(hex.construction > 3) hex.appeal += 2;
                else if(hex.construction > 1) hex.appeal += 1;
                else hex.appeal += 0;

                if(hex.resource_type != ResourceEnums.HexResource.None) hex.appeal += 3;
            }
        }

        public static void GenerateHexDependantAppeal(){
            foreach(HexTile hex in HexManager.hex_list){
                foreach(HexTile hex_neighbor in hex.GetNeighbors()){
                    if(hex_neighbor.appeal > 0) hex.appeal += Convert.ToInt32(hex_neighbor.appeal * .17);
                }
            }
        }
    }
}