using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Strategy.Assets.Scripts.Objects;
using System.Linq;




namespace Terrain {

    public class TerritoryManager
    {

        public TerritoryManager()
        {

        }

        public float CalculateCityNourishment(City city){

            float nourishment = 0;
            foreach(HexTile hex in city.hex_territory_list){
                nourishment += hex.nourishment;
            }
            return nourishment;
        }

        public float CalculateCityConstruction(City city){

            float construction = 0;
            foreach(HexTile hex in city.hex_territory_list){
                construction += hex.construction;
            }
            return construction;
        }

        
















    }
}