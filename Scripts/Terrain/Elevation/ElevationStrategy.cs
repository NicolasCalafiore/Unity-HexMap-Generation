using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public abstract class ElevationStrategy
    {
        public abstract List<List<float>> GenerateElevationMap(List<List<float>> elevation_map, Vector2 map_size);

    }
}