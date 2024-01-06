using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players;
using UnityEngine;

namespace Terrain
{
    public abstract class CapitalSpawnStrategy
    {
        /*
            ElevationStrategy is used to generate elevation on the map - abstract class
        */
        private List<List<float>> city_map;
        public abstract List<List<float>> GenerateCapitalMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map, List<List<float>> city_map);

    }
}