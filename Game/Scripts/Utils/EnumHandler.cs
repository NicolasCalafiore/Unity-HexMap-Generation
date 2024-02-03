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

        /*
            Used to store all Enums
            Contains functions to convert float values (from List<List<float> map) to enums
        */

        public enum HexElevation{   //Make sure to update getter function if you add more elevation types
            Canyon = -50,
            Valley = -25,
            Flatland = 0,
            Small_Hill = 25,
            Large_Hill = 50,
            Mountain = 150,
        }
        
        public enum HexRegion{   //Make sure to update getter function if you add more region types
            Ocean ,
            River,
            Desert,
            Plain,
            Grassland,
            Tundra,
            Highland,
            Jungle,
            Swamp,
            Shore,

        }

      
        public enum HexNaturalFeature{   //Make sure to update getter function if you add more feature types
            None,
            Forest,
            Oasis,
            Heavy_Vegetation,
            Rocks,
            Jungle,
            Swamp,
        }

       
        public enum LandType{   //Make sure to update getter function if you add more land types
            Water,
            Land,
        }

    

        public enum HexResource{
            None,
            Iron,
            Cattle,
            Gems,
            Stone,
            Bananas,
            Spices,
            Wheat,
            Pigs,
            Citrus,
            Foxes,
            Gold,
            Grain,
            Grapes,
            Rice,
            Salt,
            Horses


        }

    
        public enum StructureType{
            None,
            Capital,
        }


        public enum GovernmentType{
            None,
            Democracy,
            Monarchy,
            Dictatorship,
            Theocracy,
            Tribalism,

        }

        public enum CharacterType{
            None,
            Leader,
            Advisor,
        }

        public enum CharacterGender{
            Male,
            Female,
        }

        public enum FogType{
            Undiscovered,
            Discovered,
            DiscoveredUnvisible,
            DiscoveredVisible,

        }

        public enum GoalType{
            Foreign,
            Domestic,
        }



        private static readonly Dictionary<float, FogType> fogDict = new Dictionary<float, FogType>{
            {(int) FogType.Undiscovered,  FogType.Undiscovered},
            {(int) FogType.Discovered,  FogType.Discovered},
            {(int) FogType.DiscoveredUnvisible,  FogType.DiscoveredUnvisible},
            {(int) FogType.DiscoveredVisible,  FogType.DiscoveredVisible},
        };

        private static readonly Dictionary<float, CharacterType> characterDict = new Dictionary<float, CharacterType>{
            {(int) CharacterType.None,  CharacterType.None},
            {(int) CharacterType.Leader,  CharacterType.Leader},

        };

        private static readonly Dictionary<float, CharacterGender> genderDict = new Dictionary<float, CharacterGender>
        {
            {(int) CharacterGender.Male, CharacterGender.Male},
            {(int) CharacterGender.Female, CharacterGender.Female},

        };
        

        private static readonly Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>
        {
            { (int) HexElevation.Canyon, HexElevation.Canyon },
            { (int) HexElevation.Valley, HexElevation.Valley },
            { (int) HexElevation.Flatland, HexElevation.Flatland },
            { (int) HexElevation.Small_Hill, HexElevation.Small_Hill },
            { (int) HexElevation.Large_Hill, HexElevation.Large_Hill },
            { (int) HexElevation.Mountain, HexElevation.Mountain },
        };

        private static readonly Dictionary<float, HexRegion> regionDict = new Dictionary<float, HexRegion>
        {
            { (int) HexRegion.Ocean, HexRegion.Ocean },
            { (int) HexRegion.River, HexRegion.River },
            { (int) HexRegion.Desert, HexRegion.Desert },
            { (int) HexRegion.Plain, HexRegion.Plain },
            { (int) HexRegion.Grassland, HexRegion.Grassland },
            { (int) HexRegion.Tundra, HexRegion.Tundra },
            { (int) HexRegion.Highland, HexRegion.Highland },
            { (int) HexRegion.Jungle, HexRegion.Jungle },
            { (int) HexRegion.Swamp, HexRegion.Swamp },
            { (int) HexRegion.Shore, HexRegion.Shore },
        };

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

        private static readonly Dictionary<float, LandType> landDict = new Dictionary<float, LandType>
        {
            { (int) LandType.Water, LandType.Water },
            { (int) LandType.Land, LandType.Land },
        };

        private static readonly Dictionary<float, HexResource> resourceDict = new Dictionary<float, HexResource>
        {
            { (int) HexResource.None, HexResource.None },
            { (int) HexResource.Iron, HexResource.Iron },
            { (int) HexResource.Cattle, HexResource.Cattle },
            { (int) HexResource.Gems, HexResource.Gems },
            { (int) HexResource.Stone, HexResource.Stone },
            { (int) HexResource.Bananas, HexResource.Bananas },
            { (int) HexResource.Spices, HexResource.Spices },
            { (int) HexResource.Wheat, HexResource.Wheat },
            { (int) HexResource.Pigs, HexResource.Pigs },
            { (int) HexResource.Citrus, HexResource.Citrus },
            { (int) HexResource.Foxes, HexResource.Foxes },
            { (int) HexResource.Gold, HexResource.Gold },
            { (int) HexResource.Grain, HexResource.Grain },
            { (int) HexResource.Grapes, HexResource.Grapes },
            { (int) HexResource.Rice, HexResource.Rice },
            { (int) HexResource.Salt, HexResource.Salt },
            { (int) HexResource.Horses, HexResource.Horses },
        };

        private static readonly Dictionary<float, StructureType> structureDict = new Dictionary<float, StructureType>
        {
            { (int) StructureType.None, StructureType.None },
            { (int) StructureType.Capital, StructureType.Capital },
        };

        private static readonly Dictionary<float, GovernmentType> governmentDict = new Dictionary<float, GovernmentType>
        {
            { (int) GovernmentType.None, GovernmentType.None },
            { (int) GovernmentType.Democracy, GovernmentType.Democracy },
            { (int) GovernmentType.Monarchy, GovernmentType.Monarchy },
            { (int) GovernmentType.Dictatorship, GovernmentType.Dictatorship },
            { (int) GovernmentType.Theocracy, GovernmentType.Theocracy },
            { (int) GovernmentType.Tribalism, GovernmentType.Tribalism },
        };

        public static CharacterGender GetGenderType(float genderValue)
        {
            return genderDict.TryGetValue(genderValue, out var elevation) ? elevation : default;
        }

        public static HexElevation GetElevationType(float elevationValue)
        {
            return elevationDict.TryGetValue(elevationValue, out var elevation) ? elevation : default;
        }

        public static HexRegion GetRegionType(float regionValue)
        {
            regionValue = Mathf.Round(regionValue);
            return regionDict.TryGetValue(regionValue, out var region) ? region : default;
        }

        public static HexNaturalFeature GetNaturalFeatureType(float featureValue)
        {
            featureValue = Mathf.Round(featureValue);
            return featureDict.TryGetValue(featureValue, out var feature) ? feature : default;
        }

        public static LandType GetLandType(float landValue)
        {
            return landDict.TryGetValue(landValue, out var land) ? land : default;
        }

        public static HexResource GetResourceType(float resourceValue)
        {
            return resourceDict.TryGetValue(resourceValue, out var resource) ? resource : default;
        }

         public static StructureType GetStructureType(float structureValue)
         {
             return structureDict.TryGetValue(structureValue, out var structure) ? structure : default;
         }

         public static GovernmentType GetGovernmentType(float governmentValue)
        {
            return governmentDict.TryGetValue(governmentValue, out var government) ? government : default;
        }

        public static CharacterType GetCharacterType(float characterValue)
        {
            return characterDict.TryGetValue(characterValue, out var character) ? character : default;
        }

        public static FogType GetFogType(float fogValue)
        {
            return fogDict.TryGetValue(fogValue, out var character) ? character : default;
        }


        public static List<EnumHandler.GovernmentType> GetGovernmentTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.GovernmentType)).Cast<EnumHandler.GovernmentType>().ToList();
        }

        public static List<EnumHandler.CharacterGender> GetCharacterGenders()
        {
            return Enum.GetValues(typeof(EnumHandler.CharacterGender)).Cast<EnumHandler.CharacterGender>().ToList();
        }

        public static List<EnumHandler.CharacterType> GetCharacterTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.CharacterType)).Cast<EnumHandler.CharacterType>().ToList();
        }

        public static List<EnumHandler.HexElevation> GetElevationTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.HexElevation)).Cast<EnumHandler.HexElevation>().ToList();
        }

        public static List<EnumHandler.HexRegion> GetRegionTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.HexRegion)).Cast<EnumHandler.HexRegion>().ToList();
        }

        public static List<EnumHandler.HexNaturalFeature> GetNaturalFeatureTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.HexNaturalFeature)).Cast<EnumHandler.HexNaturalFeature>().ToList();
        }

        public static List<EnumHandler.LandType> GetLandTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.LandType)).Cast<EnumHandler.LandType>().ToList();
        }

        public static List<EnumHandler.HexResource> GetResourceTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.HexResource)).Cast<EnumHandler.HexResource>().ToList();
        }

        public static List<EnumHandler.StructureType> GetStructureTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.StructureType)).Cast<EnumHandler.StructureType>().ToList();
        }

        public static List<EnumHandler.FogType> GetFogTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.FogType)).Cast<EnumHandler.FogType>().ToList();
        }

        public static List<EnumHandler.GoalType> GetGoalTypes()
        {
            return Enum.GetValues(typeof(EnumHandler.GoalType)).Cast<EnumHandler.GoalType>().ToList();
        }

        
    }
}