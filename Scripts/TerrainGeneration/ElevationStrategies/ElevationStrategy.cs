using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace TerrainGeneration
{
    public abstract class ElevationStrategy
    {
        public float min_elevation = -0.5f;
        public float max_elevation = .5f;
        public float step_increment = 0.25f;
        public abstract void ElevateHexTerrain(List<Hex> HEX_LIST, Vector2 map_size);
    }
}