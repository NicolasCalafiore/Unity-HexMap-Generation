        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class FogEnums
    {
        
        public enum FogType{
            Undiscovered = 0,
            Discovered = 1,
            DiscoveredUnvisible,
            DiscoveredVisible,

        }

        private static readonly Dictionary<float, FogType> fogDict = new Dictionary<float, FogType>{
            {(int) FogType.Undiscovered,  FogType.Undiscovered},
            {(int) FogType.Discovered,  FogType.Discovered},
            {(int) FogType.DiscoveredUnvisible,  FogType.DiscoveredUnvisible},
            {(int) FogType.DiscoveredVisible,  FogType.DiscoveredVisible},
        };


        public static FogType GetFogType(float fogValue)
        {
            return fogDict.TryGetValue(fogValue, out var character) ? character : default;
        }


        public static List<FogType> GetFogTypes()
        {
            return Enum.GetValues(typeof(FogType)).Cast<FogType>().ToList();
        }
    }
}