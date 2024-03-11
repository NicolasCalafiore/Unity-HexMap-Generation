       
        
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

        public static List<StructureType> GetStructureTypes() => 
                Enum.GetValues(typeof(StructureType)).Cast<StructureType>().ToList();
        public static StructureType GetStructureType(float structureValue) => 
                (StructureType)Enum.Parse(typeof(StructureType), structureValue.ToString());
    }
}