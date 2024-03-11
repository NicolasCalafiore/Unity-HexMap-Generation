using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using UnityEngine;

namespace Terrain
{
    public abstract class CapitalSpawnStrategy
    {
        // ElevationStrategy is used to generate elevation on the map - abstract class
        private int capital_minimum_distance = 2;
        public abstract List<List<float>> GenerateCapitalMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map, List<List<float>> city_map);

        // This method is used to get a valid random coordinate
        // It gets a random coordinate and then checks if it is a valid coordinate
        // Returns true if water_map at the given coordinate is water or if 
        // city_map at the given coordinate is a capital or is too close to another capital
        protected bool isInvalidCoordinate(Vector3 random_coor, List<List<float>> water_map, List<List<float>> city_map){
            
            return water_map[(int) random_coor.x][(int) random_coor.z] == (int) LandEnums.LandType.Water || 
                   city_map[(int) random_coor.x][(int) random_coor.z] == (int) StructureEnums.StructureType.Capital ||
                   MapUtils.CircularSearch( (int) random_coor.x, (int) random_coor.z, city_map, (int) StructureEnums.StructureType.Capital, capital_minimum_distance);
        }
    }
}