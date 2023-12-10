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
        private float river_scale = 8;
        private float ocean_scale = 4;

        public override List<List<float>> GenerateWaterMap(Vector2 max_min, Vector2 map_size, string type)
        {
            List<List<float>> map = TerrainUtils.GenerateMap(map_size);
        
            if(type == "river") TerrainUtils.GeneratePerlinNoiseMap(map, map_size, river_scale);
            else if(type == "ocean") TerrainUtils.GeneratePerlinNoiseMap(map, map_size, ocean_scale);
            else TerrainUtils.GeneratePerlinNoiseMap(map, map_size, 1);
            FilterPerlinMap(map, map_size, max_min);
            return map;
        }
        


        public static void FilterPerlinMap(List<List<float>> map, Vector2 map_size, Vector2 max_min){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    if(map[i][j] < max_min.x && map[i][j] > max_min.y) map[i][j] = (int) TerrainUtils.LandType.Water;         //Water 
                    else map[i][j] = (int) TerrainUtils.LandType.Land;     //Land
                }
            }   
        }

        



    }
}