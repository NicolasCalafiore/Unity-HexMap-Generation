using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public City(string name, int player_id)
        {
            this.name = name;
            this.player_id = player_id;
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
    }
}