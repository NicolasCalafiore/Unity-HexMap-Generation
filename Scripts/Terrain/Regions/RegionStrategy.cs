using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    public abstract class RegionStrategy
    {
        public abstract List<List<float>> GenerateRegionsMap(Vector2 map_size, List<List<float>> ocean_map);
        
    }
}