        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;

namespace Character
{
    public static class CharacterEnums
    {

        public enum CharacterType{
            None,
            Leader,
            Advisor,
        }

        public enum CharacterGender{
            Male,
            Female,
        }

        public enum RoleType
        {
            Foreign,
            Domestic,
            Leader
        }

        public static List<CharacterGender> GetCharacterGenders() => Enum.GetValues(typeof(CharacterGender)).Cast<CharacterGender>().ToList();

        public static List<CharacterType> GetCharacterTypes() => Enum.GetValues(typeof(CharacterType)).Cast<CharacterType>().ToList();

        public static CharacterType GetCharacterType(float characterValue) => (CharacterType)Enum.Parse(typeof(CharacterType), characterValue.ToString());

        public static CharacterGender GetGenderType(float genderValue) => (CharacterGender)Enum.Parse(typeof(CharacterGender), genderValue.ToString());
        
        public static RoleType GetRoleType(float roleValue) => (RoleType)Enum.Parse(typeof(RoleType), roleValue.ToString());
        
    }
}