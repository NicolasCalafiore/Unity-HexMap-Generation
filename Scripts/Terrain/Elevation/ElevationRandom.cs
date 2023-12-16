using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public class ElevationRandom : ElevationStrategy
    {
        public override List<List<float>> GenerateElevationMap(List<List<float>> map, Vector2 map_size)
        {
            float[] values = TerrainUtils.GetElevationValues();

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    int random = UnityEngine.Random.Range(0, values.Length);
                    map[i][j] = values[random];
                }
            }

            return map;

        }
    }

}