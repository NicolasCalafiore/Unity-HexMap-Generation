using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class FeaturesEnums
    {

        public enum HexNaturalFeature{   //Make sure to update getter function if you add more feature types
            None,
            Forest,
            Oasis,
            Heavy_Vegetation,
            Rocks,
            Jungle,
            Swamp,
        }


        private static readonly Dictionary<float, HexNaturalFeature> featureDict = new Dictionary<float, HexNaturalFeature>
        {
            { (int) HexNaturalFeature.None, HexNaturalFeature.None },
            { (int) HexNaturalFeature.Forest, HexNaturalFeature.Forest },
            { (int) HexNaturalFeature.Oasis, HexNaturalFeature.Oasis },
            { (int) HexNaturalFeature.Heavy_Vegetation, HexNaturalFeature.Heavy_Vegetation },
            { (int) HexNaturalFeature.Rocks, HexNaturalFeature.Rocks },
            { (int) HexNaturalFeature.Jungle, HexNaturalFeature.Jungle },
            { (int) HexNaturalFeature.Swamp, HexNaturalFeature.Swamp },
        };


        
        public static List<HexNaturalFeature> GetNaturalFeatureTypes()
        {
            return Enum.GetValues(typeof(HexNaturalFeature)).Cast<HexNaturalFeature>().ToList();
        }

        public static HexNaturalFeature GetNaturalFeatureType(float featureValue)
        {
            featureValue = Mathf.Round(featureValue);
            return featureDict.TryGetValue(featureValue, out var feature) ? feature : default;
        }


    }
}