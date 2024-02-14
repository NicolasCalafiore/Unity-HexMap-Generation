using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class ElevationEnums
    {
        public enum HexElevation
        {
            Canyon = -50,
            Valley = -25,
            Flatland = 0,
            Small_Hill = 25,
            Large_Hill = 50,
            Mountain = 150,
        }

        public static List<HexElevation> GetElevationTypes()
        {
            return Enum.GetValues(typeof(HexElevation)).Cast<HexElevation>().ToList();
        }

        public static HexElevation GetElevationType(float elevationValue)
        {
            return elevationDict.TryGetValue(elevationValue, out var elevation) ? elevation : default;
        }

        private static readonly Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>
        {
            { (int) HexElevation.Canyon, HexElevation.Canyon },
            { (int) HexElevation.Valley, HexElevation.Valley },
            { (int) HexElevation.Flatland, HexElevation.Flatland },
            { (int) HexElevation.Small_Hill, HexElevation.Small_Hill },
            { (int) HexElevation.Large_Hill, HexElevation.Large_Hill },
            { (int) HexElevation.Mountain, HexElevation.Mountain },
        };

        
        
    }
}