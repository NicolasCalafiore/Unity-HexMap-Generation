using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
namespace Character
{

    public class NameByRegion : CharacterNameStrategy
    {
        public override List<string> GenerateNames(Vector2 capital_coordinates, List<List<float>> regions_map, EnumHandler.CharacterGender gender)
        {            
            List<string> first_names = IOHandler.ReadFirstNamesRegionSpecified( 
                EnumHandler.GetRegionType(regions_map[ (int) capital_coordinates.x][ (int) capital_coordinates.y]).ToString(), gender.ToString());
            
            List<string> last_names = IOHandler.ReadLastNamesRegionSpecified(
                EnumHandler.GetRegionType(regions_map[ (int) capital_coordinates.x][ (int) capital_coordinates.y]).ToString());


            System.Random r = new System.Random();
            int first_random_index = r.Next(0, first_names.Count() - 1);
            int second_random_index = r.Next(0, last_names.Count() - 1);
            
            List<string> names = new List<string>(){
                first_names[first_random_index], 
                last_names[second_random_index]
                };

            return names;
            
        }
    }
}