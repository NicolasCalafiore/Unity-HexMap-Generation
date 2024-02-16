
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cabinet;
using Strategy.Assets.Scripts.Objects;
using UnityEngine;
using static Character.CharacterEnums;

namespace Character
{
    public static class CharacterFactory
    {
        private static CharacterNameStrategy character_names_strategy = new NameByRegion();
        private static int gender_male_chance = 75;

        public static ICharacter CreateCharacter(RoleType type, List<List<float>> regions_map, City city){
                CharacterGender gender = Random.Range(0, 100) < gender_male_chance ? CharacterGender.Male : CharacterGender.Female;
                List<string> names = character_names_strategy.GenerateNames(city.GetColRow(), regions_map, gender);

                switch(type){
                    case RoleType.Leader:
                        return new Leader(names, gender);
 
                    case RoleType.Domestic:
                        return new Domestic(names, gender);
                    
                    case RoleType.Foreign:
                        return new Foreign(names, gender);
                    
                }

                return null;


        }

    }
}