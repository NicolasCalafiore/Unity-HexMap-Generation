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
        
        public const int max_traits = 3;
        public const string HOMELAND = "Homeland", REGIONAL_CONNECTION = "Regional Connection", PEACE_PROMOTER = "Peace Promoter", 
        WAR_MONGER = "War Monger", HOME_FRONT = "Home Front", NEIGHBORLY = "Neighborly", IDEOLOGICAL = "Ideological", 
        RACIST_REGION = "Regional Discrimination", DIPLOMAT = "Diplomat", RACIST = "Racist", PEACE_KEEPER = "Peace Keeper", 
        FINANCIER = "Financier", PRODUCTION_EXPERT = "Production Expert";

        public static Dictionary<string, string> barred_traits = new Dictionary<string, string>(){
            
            {HOMELAND, REGIONAL_CONNECTION},
            {REGIONAL_CONNECTION, HOMELAND},

            {PEACE_PROMOTER, WAR_MONGER},
            {WAR_MONGER, PEACE_PROMOTER},

            {HOME_FRONT, NEIGHBORLY},
            {NEIGHBORLY, HOME_FRONT},

        };

        // Generates random traits for all characters
        // Requires all possible trait dependencies to be initialized (i.e cities)
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

                    if(barred_traits.ContainsKey(i.name) && barred_traits[i.name] == trait.name) return false;
                    
                    if(i.name == trait.name) return false;
                }
            }
            return true;
        }


        // Adds random traits to the character
        public static void AddRandomTraits(AbstractCharacter character,  Player player)
        {
            int random_trait_amount = Random.Range(1, max_traits + 1);

            for(int i = 0; i < random_trait_amount; i++){
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