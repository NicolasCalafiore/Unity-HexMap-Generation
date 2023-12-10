using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    public class MapFactor : RegionStrategy
    {

        public override List<List<float>> GenerateRegionsMap(Vector2 map_size)
        {
            GenerateMapFactors(map_size);
            throw new NotImplementedException();
        }

        private void GenerateMapFactors(Vector2 map_size){
            List<List<float>> rain_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(rain_map, map_size, 2.2f);

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {

                    if(rain_map[i][j] < .325f) rain_map[i][j] = 0;         //DESERT
                }
            } 
        }
    }
}