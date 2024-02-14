using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class LandEnums
    {

        public enum LandType{   //Make sure to update getter function if you add more land types
            Water,
            Land,
        }

        public static List<LandType> GetLandTypes()
        {
            return Enum.GetValues(typeof(LandType)).Cast<LandType>().ToList();
        }


        private static readonly Dictionary<float, LandType> landDict = new Dictionary<float, LandType>
        {
            { (int) LandType.Water, LandType.Water },
            { (int) LandType.Land, LandType.Land },
        };

        public static LandType GetLandType(float landValue)
        {
            return landDict.TryGetValue(landValue, out var land) ? land : default;
        }


    }
}