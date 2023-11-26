using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

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
            List<float> elevationLevels = ElevationLevels(min_elevation, max_elevation, step_increment);
            foreach (Hex hex in HEX_LIST)
            {
                float randomValue = elevationLevels[UnityEngine.Random.Range(0, elevationLevels.Count)];
                hex.SetElevation(randomValue);

            }
        }

        /// <summary>
        /// Generates a list of elevation levels based on the specified parameters.
        /// </summary>
        /// <returns>A list of elevation levels.</returns>
        private List<float> ElevationLevels(float min_elevation, float max_elevation, float step_increment)
        {
            List<float> elevationLevels = new List<float>();

            for (float i = min_elevation; i <= max_elevation; i += step_increment)
            {
                elevationLevels.Add(i);
            }
            elevationLevels.Add(1.5f);
            return elevationLevels;
        }
    }
}

