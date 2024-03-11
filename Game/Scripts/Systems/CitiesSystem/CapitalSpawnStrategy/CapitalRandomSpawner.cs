using System.Collections.Generic;
using Character;
using Players;
using UnityEngine;

namespace Terrain
{
    public class CapitalRandomSpawner : CapitalSpawnStrategy
    {
        // This class is used to spawn capitals at random locations

        // This method is used to generate the capital map
        public override List<List<float>> GenerateCapitalMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map, List<List<float>> city_map)
        {
            player_list.ForEach(_ => PlaceCapitalAtRandomLocation(map_size, water_map, city_map, feature_map, resource_map));
            return city_map;
        }

        // This method is used to place the capital at a random location
        // It first gets a random coordinate and then checks if it is a valid coordinate
        // If it is not a valid coordinate, it gets a new random coordinate
        // Once a valid coordinate is found, the space is cleared for the capital
        private void PlaceCapitalAtRandomLocation(Vector2 map_size, List<List<float>> water_map, List<List<float>> city_map, List<List<float>> feature_map, List<List<float>> resource_map)
        {
            Vector3 random_coor = GetValidRandomCoordinate(map_size, water_map, city_map);
            ClearSpaceForCapital(random_coor, city_map, feature_map, resource_map);
        }

        // This method is used to check if a coordinate is invalid
        // It checks if the coordinate is on water or if it is already occupied by a city
        // If the coordinate is invalid, it returns true
        private Vector3 GetValidRandomCoordinate(Vector2 map_size, List<List<float>> water_map, List<List<float>> city_map)
        {
            Vector3 random_coor;
            do
            {
                random_coor = MapUtils.RandomVector3(map_size);
            } while (isInvalidCoordinate(random_coor, water_map, city_map));

            return random_coor;
        }

        // This method clears the space for the capital
        // It sets the feature_map and resource_map at the given coordinate to None
        private void ClearSpaceForCapital(Vector3 random_coor, List<List<float>> city_map, List<List<float>> feature_map, List<List<float>> resource_map)
        {
            city_map[(int)random_coor.x][(int)random_coor.z] = (int)StructureEnums.StructureType.Capital;
            feature_map[(int)random_coor.x][(int)random_coor.z] = (int)FeaturesEnums.HexNaturalFeature.None;
            resource_map[(int)random_coor.x][(int)random_coor.z] = (int)ResourceEnums.HexResource.None;
        }
    }
}