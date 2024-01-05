using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    public abstract class ResourceStrategy
    {
        /*
            ResourceStrategy is used to generate resources on the map - abstract class
        */
        public abstract List<List<float>> GenerateResourceMap(Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map, List<List<float>> features_map);
        
    }
}