using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Character
{
    // CharacterNameStrategy is a strategy for generating names of a character
    public abstract class CharacterNameStrategy
    {
         public abstract List<string> GenerateNames(Vector2 capital_coordinates, List<List<float>> regions_map, CharacterEnums.CharacterGender gender);
        
    }
}