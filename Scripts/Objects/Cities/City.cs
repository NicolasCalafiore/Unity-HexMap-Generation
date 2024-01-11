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
        int inhabitants = 1;
        int stability = 50;
        int nourishment = 3;
        List<HexTile> hex_territory_list = new List<HexTile>();

        public City(string name, int player_id, Vector2 col_row)
        {
            this.name = name;
            this.player_id = player_id;
            this.col_row = col_row;
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