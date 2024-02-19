using System;
using System.Collections.Generic;
using Cabinet;
using Character;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;

namespace Character
{
    public abstract class ICharacter
    {
        public CharacterEnums.CharacterType character_type;
        public CharacterEnums.CharacterGender gender;
        public List<TraitBase> traits = new List<TraitBase>();
        public Player owner_player;
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
        private int max_traits = 3;

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

        public void AddRandomTrait(){
            int random_trait_amount = UnityEngine.Random.Range(1, max_traits);

            for(int i = 0; i < random_trait_amount; i++){
                TraitBase trait_method = DomesticTraitBase.GetRandomDomesticTrait(); // default

                switch(this){
                    case Leader leader:
                        trait_method = TraitBase.GetRandomLeaderTrait();
                        break;
                    case Domestic domestic:
                        trait_method = DomesticTraitBase.GetRandomDomesticTrait();
                        break;
                    case Foreign foreign:
                        trait_method = ForeignTraitBase.GetRandomForeignTrait();
                        break;
                }



                TraitBase random_trait;
                random_trait = trait_method;
                if(!isValidTrait(random_trait)){i--; continue;}

                traits.Add(random_trait);
            }

        }

        

        private bool isValidTrait(TraitBase trait){
            foreach(TraitBase i in traits){
                if(i is ForeignTraitBase && trait is ForeignTraitBase || i is DomesticTraitBase && trait is DomesticTraitBase){
                    if(i.id == trait.id) return false;
                    if(i.name == trait.name) return false;
                }
            }
            return true;
        }


    }
}