using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace TerrainGeneration
{
    /// <summary>
    /// Contains classes related to terrain generation.
    /// </summary>
    public class RandomElevation : ElevationStrategy
    {        
        /// <summary>
        /// Elevates the terrain of a list of hexes with random elevation values.
        /// </summary>

        
        public override void ElevateHexTerrain(List<Hex> HEX_LIST, Vector2 map_size)
        {
            List<List<float>> elevationMap = ElevationUtils.GenerateElevationMap(map_size);
            GenerateRandomTerrain(elevationMap);
            ElevationUtils.UpdateHexElevation(HEX_LIST, map_size, elevationMap);

        }
        
    private void GenerateRandomTerrain(List<List<float>> elevationMap){
        int[] values = (int[]) Enum.GetValues(typeof(ElevationUtils.HexElevationTypes));

        for(int i = 0; i < elevationMap.Count; i++){
            for(int j = 0; j < elevationMap[i].Count; j++){
                elevationMap[i][j] =  values[UnityEngine.Random.Range(0, values.Length) / 100];
            }
        }

    }



    }
}

