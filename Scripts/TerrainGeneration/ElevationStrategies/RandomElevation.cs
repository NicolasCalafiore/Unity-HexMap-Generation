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
        /// <param name="HEX_LIST">The list of hexes to elevate.</param>
        /// <param name="map_size">The size of the map.</param>
        /// <param name="min_elevation">The minimum elevation value.</param>
        /// <param name="max_elevation">The maximum elevation value.</param>
        /// <param name="step_increment">The increment value for elevation levels.</param>
        public override void ElevateHexTerrain(List<Hex> HEX_LIST, Vector2 map_size, float min_elevation = 0f, float max_elevation = 1f, float step_increment = 0.25f)
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
        /// <param name="min_elevation">The minimum elevation value.</param>
        /// <param name="max_elevation">The maximum elevation value.</param>
        /// <param name="step_increment">The increment value for elevation levels.</param>
        /// <returns>A list of elevation levels.</returns>
        private List<float> ElevationLevels(float min_elevation, float max_elevation, float step_increment)
        {
            List<float> elevationLevels = new List<float>();

            for (float i = min_elevation; i <= max_elevation; i += step_increment)
            {
                elevationLevels.Add(i);
            }

            return elevationLevels;
        }
    }
}

