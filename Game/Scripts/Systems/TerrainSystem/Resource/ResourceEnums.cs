using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class ResourceEnums
    {

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


        public static List<HexResource> GetResourceTypes()
        {
            return Enum.GetValues(typeof(HexResource)).Cast<HexResource>().ToList();
        }

        
        public static HexResource GetResourceType(float resourceValue)
        {
            return resourceDict.TryGetValue(resourceValue, out var resource) ? resource : default;
        }



    }
}