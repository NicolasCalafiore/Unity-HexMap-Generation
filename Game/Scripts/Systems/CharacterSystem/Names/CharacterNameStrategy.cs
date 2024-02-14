using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Character
{
    public abstract class CharacterNameStrategy
    {
         public abstract List<string> GenerateNames(Vector2 capital_coordinates, List<List<float>> regions_map, CharacterEnums.CharacterGender gender);
        
    }
}