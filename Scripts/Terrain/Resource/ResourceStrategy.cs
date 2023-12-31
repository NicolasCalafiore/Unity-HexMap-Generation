using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    public abstract class ResourceStrategy
    {
        public abstract List<List<float>> GenerateResourceMap(Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map);
        
    }
}