using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class RegionsEnums
    {
        
        
        
        private static readonly Dictionary<float, HexRegion> 
        regionDict = new Dictionary<float, HexRegion>
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


        public static List<HexRegion> GetRegionTypes()
        {
            return Enum.GetValues(typeof(HexRegion)).Cast<HexRegion>().ToList();
        }

        public static HexRegion GetRandomRegionType(){
            return GetRegionType(UnityEngine.Random.Range(0, GetRegionTypes().Count));
        }

        public static HexRegion GetRandomRegionLandType(){
            List<HexRegion> regions =  GetRegionTypes();
            regions.Remove(HexRegion.Ocean);
            regions.Remove(HexRegion.River);
            regions.Remove(HexRegion.Shore);

            return regions[UnityEngine.Random.Range(0, regions.Count)];
        }
        
        public static HexRegion GetRegionType(float regionValue)
        {
            regionValue = Mathf.Round(regionValue);
            return regionDict.TryGetValue(regionValue, out var region) ? region : default;
        }

    }
}