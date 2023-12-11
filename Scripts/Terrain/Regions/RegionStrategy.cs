using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    public abstract class RegionStrategy
    {
        public float perlin_scale = 4.5f;
        public abstract List<List<float>> GenerateRegionsMap(Vector2 map_size, GameObject perlin_map_object);
        
    }
}