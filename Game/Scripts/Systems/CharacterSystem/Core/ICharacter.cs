using System;
using System.Collections.Generic;
using Cabinet;
using Character;
using Terrain;
using UnityEngine;

namespace Character
{
    public abstract class ICharacter
    {
        public CharacterEnums.CharacterType character_type;
        public CharacterEnums.CharacterGender gender;
        public string first_name;
        public string last_name;
        public string title;
        public int charisma;
        public int intelligence;
        public int skill;
        public int age;
        public int health;
        public int loyalty;
        public int wealth;
        public int influence;

        public string GetFullName(){
            return title + first_name + " " + last_name;
        }


        public void InitializeCharacteristics(){
            charisma = UnityEngine.Random.Range(0, 100);
            intelligence = UnityEngine.Random.Range(0, 100);
            skill = UnityEngine.Random.Range(0, 100);
            age = UnityEngine.Random.Range(13, 100);
            health = UnityEngine.Random.Range(0, 100);
            loyalty = UnityEngine.Random.Range(0, 100);
            wealth = UnityEngine.Random.Range(0, 100);
            influence = UnityEngine.Random.Range(0, 100);
        }




    }
}