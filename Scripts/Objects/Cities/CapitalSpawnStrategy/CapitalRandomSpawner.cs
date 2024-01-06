using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Terrain
{
    public class CapitalRandomSpawner : CapitalSpawnStrategy
    {

        public override List<List<float>> GenerateCapitalMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map, List<List<float>> city_map){
            for(int i = 0; i < player_list.Count; i++){
                
                Vector3 random_coor = TerrainUtils.RandomVector3(map_size);

                while(water_map[(int) random_coor.x][(int) random_coor.z] == (int) EnumHandler.LandType.Water){ // If random_coor is water, generate new random_coor
                    random_coor = TerrainUtils.RandomVector3(map_size);
                }

                ClearSpaceForCapital(random_coor, city_map, feature_map, resource_map); // Clear space for capital
            }
            return city_map;
        }

        private void ClearSpaceForCapital(Vector3 random_coor, List<List<float>> city_map, List<List<float>> feature_map, List<List<float>> resource_map){
            city_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.StructureType.Capital;
            feature_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.HexNaturalFeature.None;
            resource_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.HexResource.None;
        }

    }
}