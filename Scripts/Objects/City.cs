using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Assets.Scripts.Objects
{
    public class City
    {
        private int player_number;
        private int city_number;
        private string name;
        private Vector3 position;
        private GameObject city_object;
        public City(string name, int player_number, int city_number)
        {
            this.name = name;
            this.player_number = player_number;
            this.city_number = city_number;
        }




    }
}