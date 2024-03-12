using System;
using System.Collections.Generic;
using System.Linq;
using Cabinet;
using Players;
using UnityEngine;
using static Character.CharacterEnums;

namespace Character
{
    public abstract class AbstractCharacter
    {
        public CharacterType character_type {get; set;}
        public CharacterGender gender {get; set;}
        public List<TraitBase> traits = new List<TraitBase>();
        public Player owner_player {get; set;}
        public  string first_name {get; set;}
        public string last_name {get; set;}
        public string title {get; set;}
        public int charisma {get; set;}
        public int intelligence {get; set;}
        public int skill {get; set;}
        public int age {get; set;}
        public int health {get; set;}
        public int loyalty {get; set;}
        public int wealth {get; set;}
        public int influence {get; set;}

        public AbstractCharacter(List<string> names, CharacterGender gender, Player player, List<string> titles){
            first_name = names[0];
            last_name = names[1];
            this.gender = gender;
            character_type = CharacterType.Leader;
            owner_player = player;         
            title = titles[UnityEngine.Random.Range(0, titles.Count)];
        }

        // Returns the full name of the character
        public string GetFullName() => $"{title} {first_name} {last_name}";

        // Returns the name of the character
        public string GetName() => $"{first_name} {last_name}";

        // Initializes the characteristics of the character
        public void InitializeCharacteristics()
        {
            int max = 0;
            int min = 0;

            var personTypes = new List<(int min_qualifier, int maxRange, int minRange)>
            {
                (95, 100, 70), // SPECIAL PERSON
                (85, 95, 45),  // GOOD PERSON
                (75, 60, 20),  // BAD PERSON
                (0, 85, 20)  // NORMAL PERSON
            };

            int random = UnityEngine.Random.Range(0, 100);
            foreach (var (min_qualifier, maxRange, minRange) in personTypes)
            {
                if (random >= min_qualifier)    //TO DO: OPTIMIZATION POINT
                {
                    max = GenerateRandomRange(maxRange, -5, 5);
                    min = GenerateRandomRange(minRange, -5, 10);
                    break;
                }
            }

            charisma = UnityEngine.Random.Range(min, max);
            intelligence = UnityEngine.Random.Range(min, max);
            skill = UnityEngine.Random.Range(min, max);
            age = UnityEngine.Random.Range(18, 90);
            health = UnityEngine.Random.Range(min, max);
            loyalty = UnityEngine.Random.Range(min, max);
            wealth = UnityEngine.Random.Range(min, max);
            influence = UnityEngine.Random.Range(min, max);
        }

        private int GenerateRandomRange(int baseValue, int minOffset, int maxOffset)
        {
            return baseValue + UnityEngine.Random.Range(minOffset, maxOffset);
        }


        
        // Returns a trait at a specific index
        public TraitBase GetTrait(int index) => index < traits.Count ? traits[index] : null;

        // Returns all traits of the character
        public List<TraitBase> Traits => traits;

        // Returns the rating of the character
        public int GetRating() => (charisma + intelligence + skill + loyalty + wealth + influence) / 6;
    }
}