using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using UnityEngine;


namespace Terrain
{
    public class ResourceRandom : ResourceStrategy
    {
        int iron_chance = 7;
        public override List<List<float>> GenerateResourceMap(Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map){

            List<List<float>> resource_map = TerrainUtils.GenerateMap(map_size);

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Tundra){
                        if(Random.Range(0, 100) < iron_chance) resource_map[i][j] = (int) EnumHandler.HexResource.Iron;
                    }
                }
            }

            return resource_map;

        }

    }
}