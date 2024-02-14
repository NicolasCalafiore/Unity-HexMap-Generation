       
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Character
{
    public static class StructureEnums
    {
       
       public enum StructureType{
            None,
            Capital,
        }

                public static List<StructureType> GetStructureTypes()
        {
            return Enum.GetValues(typeof(StructureType)).Cast<StructureType>().ToList();
        }
                private static readonly Dictionary<float, StructureType> structureDict = new Dictionary<float, StructureType>
        {
            { (int) StructureType.None, StructureType.None },
            { (int) StructureType.Capital, StructureType.Capital },
        };






         public static StructureType GetStructureType(float structureValue)
         {
             return structureDict.TryGetValue(structureValue, out var structure) ? structure : default;
         }
    }
}