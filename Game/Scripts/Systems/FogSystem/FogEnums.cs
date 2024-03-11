        
        
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

        public static FogType GetFogType(float fogValue) => (FogType)Enum.Parse(typeof(FogType), fogValue.ToString());
        public static List<FogType> GetFogTypes() => Enum.GetValues(typeof(FogType)).Cast<FogType>().ToList();
    }
}