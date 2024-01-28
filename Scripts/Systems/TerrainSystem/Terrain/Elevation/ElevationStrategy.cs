using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public abstract class ElevationStrategy
    {
        /*
            ElevationStrategy is used to generate elevation on the map - abstract class
        */
        public abstract List<List<float>> GenerateElevationMap(List<List<float>> elevation_map, Vector2 map_size, List<List<float>> regions_map);

    }
}