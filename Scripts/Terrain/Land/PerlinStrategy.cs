using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Water
{
    public class PerlinStrategy : WaterStrategy
    {
        private float river_scale = 4f;
        private float ocean_scale = 5f;
        private Vector2 river_max_min = new Vector2( .5f, .46f);
        private Vector2 ocean_max_min = new Vector2( .7f, .45f);

        public override List<List<float>> GenerateWaterMap(Vector2 map_size, string type)
        {
            List<List<float>> map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.NormalizePerlinMap(map);
        
            if(type == "river"){
                TerrainUtils.GeneratePerlinNoiseMap(map, map_size, river_scale);
                FilterPerlinMap(map, map_size, river_max_min);
            }
            else if(type == "ocean"){
                TerrainUtils.GeneratePerlinNoiseMap(map, map_size, ocean_scale);
                FilterPerlinMap(map, map_size, ocean_max_min);
            }

            SetLand(map);
            return map;
        }
        public static void SetLand(List<List<float>> map)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if(map[i][j] != (int)EnumHandler.LandType.Water)
                    map[i][j] = (int)EnumHandler.LandType.Land;
                }
            }
        }
        
        public static void FilterPerlinMap(List<List<float>> map, Vector2 map_size, Vector2 max_min){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    if(map[i][j] <= max_min.x && map[i][j] >= max_min.y) map[i][j] = (int) EnumHandler.LandType.Water;         //Water 
                }
            }   
        }

        



    }
}