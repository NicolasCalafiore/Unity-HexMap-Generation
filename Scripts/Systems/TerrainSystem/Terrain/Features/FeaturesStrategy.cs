using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Terrain
{
    public abstract class FeaturesStrategy
    {
        /*
            FeaturesStrategy is used to generate features on the map - abstract class
            Features are things like Forests/Oasis's - Natural Features
        */
        public abstract List<List<float>> GenerateFeaturesMap(Vector2 map_size, List<List<float>> regions_map, List<List<float>> ocean_map);
    }
}