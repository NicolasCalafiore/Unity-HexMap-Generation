using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;

namespace Character {
    public static class TraitManager {    
        
        private static int max_traits = 3;
        

        public static void GenerateCharacterTraits(){

            foreach(Player player in PlayerManager.player_list){
                foreach(AbstractCharacter character in player.government.GetCharacterList()){
                    AddRandomTraits(character, player);
                }
            }
        }

        
        internal static TraitBase GetRandomLeaderTrait(Player player) 
        {
            if(Random.Range(0, 100) < 50) 
                return DomesticTraitBase.GetRandomDomesticTrait(player);
            else
                return ForeignTraitBase.GetRandomForeignTrait(player);
        }

        public static bool IsValidTrait(TraitBase trait, AbstractCharacter character){

            foreach(TraitBase i in character.traits){
                if(i is ForeignTraitBase && trait is ForeignTraitBase || i is DomesticTraitBase && trait is DomesticTraitBase){
                    
                    if(i == null) Debug.Log("Trait is null");
                    if(i.banned_traits == null) Debug.Log("Banned traits is null");
                    if(i.Name == null) Debug.Log("Name is null");

                    if(i.banned_traits.Contains(i.Name)) return false;
                    
                    if(i.Name == trait.Name) return false;
                }
            }
            return true;
        }


        // Adds random traits to the character
        public static void AddRandomTraits(AbstractCharacter character,  Player player)
        {
            //int random_trait_amount = Random.Range(1, max_traits + 1);

            for(int i = 0; i < 3; i++){
                TraitBase random_trait = GetRandomTrait(character, player);

                if(!IsValidTrait(random_trait, character)){i--; continue;}

                character.traits.Add(random_trait);
            }
        }

        
        private static TraitBase GetRandomTrait(AbstractCharacter character, Player player)
        {
            var traitMethods = new Dictionary<Type, Func<Player, TraitBase>>
            {
                { typeof(Leader), GetRandomLeaderTrait },
                { typeof(Domestic), DomesticTraitBase.GetRandomDomesticTrait },
                { typeof(Foreign), ForeignTraitBase.GetRandomForeignTrait }
            };

            return traitMethods[character.GetType()](player);
        }
    }
}