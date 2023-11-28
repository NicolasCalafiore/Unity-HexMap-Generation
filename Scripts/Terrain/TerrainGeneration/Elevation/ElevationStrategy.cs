using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace TerrainGeneration
{
    public abstract class ElevationStrategy
    {
        public abstract void ElevateHexTerrain(List<Hex> HEX_LIST, Vector2 map_size);
    }
}