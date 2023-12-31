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

        public enum HexElevation{   //Make sure to update getter function if you add more elevation types
            Canyon = -50,
            Valley = -25,
            Flatland = 0,
            Hill = 25,
            Large_Hill = 50,
            Mountain = 150,
        }

        public enum HexRegion{   //Make sure to update getter function if you add more region types
            Ocean ,
            River,
            Desert,
            Plains,
            Grassland,
            Tundra,
            Highlands,
            Jungle,
            Swamp,

        }

        public enum HexNaturalFeature{   //Make sure to update getter function if you add more feature types
            None,
            Forest,
            Oasis,
            WheatField,
            Rocks,
            Jungle,
            Swamp,
        }

        public enum LandType{   //Make sure to update getter function if you add more land types
            Water,
            Land,
        }

        public static LandType GetLandType(float landValue){
            Dictionary<float, LandType> landDict = new Dictionary<float, LandType>(){
                { (int) LandType.Water, LandType.Water},
                { (int) LandType.Land, LandType.Land},
            };

            return landDict[landValue];
    
        }
        public static HexElevation GetElevationType(float elevationValue)
        {
            Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>(){
                { (int) HexElevation.Canyon, HexElevation.Canyon},
                { (int) HexElevation.Valley, HexElevation.Valley},
                { (int) HexElevation.Flatland, HexElevation.Flatland},
                { (int) HexElevation.Hill, HexElevation.Hill},
                { (int) HexElevation.Large_Hill, HexElevation.Large_Hill},
                { (int) HexElevation.Mountain, HexElevation.Mountain},
            };

            return elevationDict[elevationValue];
            
        }

        public static HexRegion GetRegionType(float regionValue)
        {
            regionValue = Mathf.Round(regionValue);

            Dictionary<float, HexRegion> regionDict = new Dictionary<float, HexRegion>(){
                { (int) HexRegion.Ocean, HexRegion.Ocean},
                { (int) HexRegion.River, HexRegion.River},
                { (int) HexRegion.Desert, HexRegion.Desert},
                { (int) HexRegion.Plains, HexRegion.Plains},
                { (int) HexRegion.Grassland, HexRegion.Grassland},
                { (int) HexRegion.Tundra, HexRegion.Tundra},
                { (int) HexRegion.Highlands, HexRegion.Highlands},
                { (int) HexRegion.Jungle, HexRegion.Jungle},
                { (int) HexRegion.Swamp, HexRegion.Swamp},
            };


            return regionDict[regionValue];
            
        }

        public static HexNaturalFeature GetFeatureType(float featureValue)
        {
            featureValue = Mathf.Round(featureValue);

            Dictionary<float, HexNaturalFeature> featureDict = new Dictionary<float, HexNaturalFeature>(){
                { (int) HexNaturalFeature.None, HexNaturalFeature.None},
                { (int) HexNaturalFeature.Forest, HexNaturalFeature.Forest},
                { (int) HexNaturalFeature.Oasis, HexNaturalFeature.Oasis},
                { (int) HexNaturalFeature.WheatField, HexNaturalFeature.WheatField},
                { (int) HexNaturalFeature.Rocks, HexNaturalFeature.Rocks},
                { (int) HexNaturalFeature.Jungle, HexNaturalFeature.Jungle},
                { (int) HexNaturalFeature.Swamp, HexNaturalFeature.Swamp},
            };

            return featureDict[featureValue];
            
        }

    }
}