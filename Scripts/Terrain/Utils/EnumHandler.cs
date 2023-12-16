using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Terrain
{
    public static class EnumHandler
    {
        public enum LandType{   //Make sure to update getter function if you add more land types
            Water = 0,
            Land = 1,
        }


        public enum HexElevation{   //Make sure to update getter function if you add more elevation types
            Canyon = -50,
            Valley = -25,
            Flatland = 0,
            Hill = 25,
            Large_Hill = 50,
            Mountain = 150,
        }

        public enum HexRegion{   //Make sure to update getter function if you add more region types
            Ocean = 0,
            Desert = 1,
            Plains = 2,
            Grassland = 3,
            Tundra = 4,

        }

        public enum HexFeatures{   //Make sure to update getter function if you add more feature types
            Ocean = 0,
            Forest = 1,
            Jungle = 2,
            Swamp = 3,
            Savanna = 4,
            Oasis = 5,
        }

        public static LandType GetLandType(float landValue){
            Dictionary<float, LandType> landDict = new Dictionary<float, LandType>(){
                { 0, LandType.Water},
                { 1, LandType.Land},
            };

            return landDict[landValue];
    
        }
        public static HexElevation GetElevationType(float elevationValue)
        {
            Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>(){
                { -50, HexElevation.Canyon},
                { -25, HexElevation.Valley},
                { 0, HexElevation.Flatland},
                { 25, HexElevation.Hill},
                { 50, HexElevation.Large_Hill},
                { 150, HexElevation.Mountain},
            };

            return elevationDict[elevationValue];
            
        }

        public static HexRegion GetRegionType(float regionValue)
        {
            regionValue = Mathf.Round(regionValue);

            Dictionary<float, HexRegion> regionDict = new Dictionary<float, HexRegion>(){
                { 0, HexRegion.Ocean},
                { 1, HexRegion.Desert},
                { 2, HexRegion.Plains},
                { 3, HexRegion.Grassland},
                { 4, HexRegion.Tundra},
            };

            return regionDict[regionValue];
            
        }

        public static HexFeatures GetFeatureType(float featureValue)
        {
            featureValue = Mathf.Round(featureValue);

            Dictionary<float, HexFeatures> featureDict = new Dictionary<float, HexFeatures>(){
                { 0, HexFeatures.Ocean},
                { 1, HexFeatures.Forest},
                { 2, HexFeatures.Jungle},
                { 3, HexFeatures.Swamp},
                { 4, HexFeatures.Savanna},
                { 5, HexFeatures.Oasis},
            };

            return featureDict[featureValue];
            
        }

    }
}