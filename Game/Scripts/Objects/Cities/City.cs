using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Scripts.Objects
{
    public class City
    {
        /*
            City class is used to store information about the city
        */
        public List<HexTile> hex_territory_list = new List<HexTile>();
        private Vector2 COL_ROW;
        private string name;
        private int player_id;
        public float inhabitants = 500;
        public float stability = 50;
        public float nourishment = 50;
        public float construction = 15;

        public City(string name, int player_id, Vector2 col_row)
        {
            this.name = name;
            this.player_id = player_id;
            this.COL_ROW = col_row;
        }

        public void CalculateCityNourishment(TerritoryManager territory_manager){
            this.nourishment += territory_manager.CalculateCityNourishment(this);
        }

        public void CalculateCityConstruction(TerritoryManager territory_manager){
            this.construction += territory_manager.CalculateCityConstruction(this);
        }

        public string GetName()
        {
            return name;
        }

        public int GetPlayerId()
        {
            return player_id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Vector2 GetColRow()
        {
            return COL_ROW;
        }

    }
}