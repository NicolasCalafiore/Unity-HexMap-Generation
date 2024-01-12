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
        public static Dictionary<GameObject, City> city_go_to_city = new Dictionary<GameObject, City>(); // Given Hex gives Hex-Object
        private string name;
        private int player_id;
        Vector2 col_row;
        public float inhabitants = 500;
        public float stability = 50;
        public float nourishment = 50;
        public float construction = 15;
        public List<HexTile> hex_territory_list = new List<HexTile>();

        public City(string name, int player_id, Vector2 col_row)
        {
            this.name = name;
            this.player_id = player_id;
            this.col_row = col_row;
        }

        public void CalculateCityNourishment(TerritoryManager territory_manager){
            this.nourishment += territory_manager.CalculateCityNourishment(this);
        }

        public void CalculateCityConstruction(TerritoryManager territory_manager){
            this.construction += territory_manager.CalculateCityConstruction(this);
        }

        public void AddHexTileTerritory(TerritoryManager territory_manager, List<HexTile> hex_list)
        {
            territory_manager.AddHexTileTerritoryToCities(hex_list , this);
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
            return col_row;
        }

    }
}