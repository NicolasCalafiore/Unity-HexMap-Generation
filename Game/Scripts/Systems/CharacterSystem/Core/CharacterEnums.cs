        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

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

        private static readonly Dictionary<float, CharacterType> characterDict = new Dictionary<float, CharacterType>{
            {(int) CharacterType.None,  CharacterType.None},
            {(int) CharacterType.Leader,  CharacterType.Leader},

        };

        private static readonly Dictionary<float, CharacterGender> genderDict = new Dictionary<float, CharacterGender>
        {
            {(int) CharacterGender.Male, CharacterGender.Male},
            {(int) CharacterGender.Female, CharacterGender.Female},

        };

        private static readonly Dictionary<float, RoleType> roleDict = new Dictionary<float, RoleType>
        {
            {(int) RoleType.Foreign, RoleType.Foreign},
            {(int) RoleType.Domestic, RoleType.Domestic},
            {(int) RoleType.Leader, RoleType.Leader},
        };

        public static List<CharacterGender> GetCharacterGenders()
        {
            return Enum.GetValues(typeof(CharacterGender)).Cast<CharacterGender>().ToList();
        }

        public static List<CharacterType> GetCharacterTypes()
        {
            return Enum.GetValues(typeof(CharacterType)).Cast<CharacterType>().ToList();
        }

        public static CharacterType GetCharacterType(float characterValue)
        {
            return characterDict.TryGetValue(characterValue, out var character) ? character : default;
        }

        public static CharacterGender GetGenderType(float genderValue)
        {
            return genderDict.TryGetValue(genderValue, out var elevation) ? elevation : default;
        }

        public static RoleType GetRoleType(float roleValue)
        {
            return roleDict.TryGetValue(roleValue, out var role) ? role : default;
        }
    }
}