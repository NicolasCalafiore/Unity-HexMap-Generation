using System;
using System.Collections.Generic;
using Cabinet;
using Character;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            return title + " " + first_name + " " + last_name;
        }

        public string GetName(){
            return first_name + " " + last_name;
        }




        public void InitializeCharacteristics(){

            int max = 100;
            int min = 0;
            int random = UnityEngine.Random.Range(0, 100);
            if(random < 70){            //NORMAL PERSON
                max = 85 + UnityEngine.Random.Range(-5, 5);;
                min = 20 + UnityEngine.Random.Range(-5, 10);
            }
            else if(random < 80){       //BAD PERSON
                max = 60 + UnityEngine.Random.Range(-5, 5);
                min = 20 + UnityEngine.Random.Range(-5, 10);
            }
            else if(random < 95){       //GOOD PERSON
                max = 95 + UnityEngine.Random.Range(-5, 5);
                min = 45 + UnityEngine.Random.Range(-5, 10);
            }
            else{                       //SPECIAL PERSON
                max = 100 + UnityEngine.Random.Range(-5, 5);
                min = 70 + UnityEngine.Random.Range(-5, 10);
            }   

            charisma = UnityEngine.Random.Range(min, max);
            intelligence = UnityEngine.Random.Range(min, max);
            skill = UnityEngine.Random.Range(min, max);
            age = UnityEngine.Random.Range(13, 90);
            health = UnityEngine.Random.Range(min, max);
            loyalty = UnityEngine.Random.Range(min, max);
            wealth = UnityEngine.Random.Range(min, max);
            influence = UnityEngine.Random.Range(min, max);
        }

        public void AddRandomTrait(){
            int random_trait_amount = UnityEngine.Random.Range(1, max_traits + 1);

            for(int i = 0; i < random_trait_amount; i++){
                TraitBase trait_method = DomesticTraitBase.GetRandomDomesticTrait(owner_player); // default

                switch(this){
                    case Leader leader:
                        trait_method = TraitBase.GetRandomLeaderTrait(owner_player);
                        break;
                    case Domestic domestic:
                        trait_method = DomesticTraitBase.GetRandomDomesticTrait(owner_player);
                        break;
                    case Foreign foreign:
                        trait_method = ForeignTraitBase.GetRandomForeignTrait(owner_player);
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

        public TraitBase GetTrait(int index){
            if(index < traits.Count){
                return traits[index];
            }

            return null;
        }

        public List<TraitBase> GetTraits(){
            return traits;
        }

        public int GetRating(){
            return (charisma + intelligence + skill + loyalty + wealth + influence)/6;
        }


    }
}