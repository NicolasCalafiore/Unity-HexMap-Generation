using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Cities;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;


namespace Diplomacy
{
    public class ForeignStandard : ForeignStrategy
    {
        private const float LEADER_MULTIPLIER = 1.5f;
        private const float DISIMILAIR_TRAIT_MULTIPLIER = .1f;
        public ForeignStandard(){}

        //This method calculates the starting relationship between two players
        //It takes into account the traits of the leader and the government characters
        public override float GenerateStartingRelationship(Player known_player, Player player){
            return CalculateTraitRelationshipImpact(player, known_player) + 
                            CalculateSimiliarTraitsImpact(player, known_player);
        }

        //This method calculates the relationship between two players
        //It takes into account the traits of the leader and the government characters
        private float CalculateTraitRelationshipImpact(Player player, Player known_player){
            float current_relationship_level = 0;
        
            foreach(ForeignTraitBase i in player.government.GetForeignByIndex(0).traits)
                current_relationship_level += i.GetTraitValue(known_player, player);
            
            foreach(TraitBase i in player.government.leader.traits)
                if(i is ForeignTraitBase)
                    current_relationship_level += ((ForeignTraitBase)i).GetTraitValue(known_player, player) * LEADER_MULTIPLIER;
            
            return current_relationship_level;
        }

        //This method calculates the relationship between two players
        //It takes into account the similiar traits of the characters
        private float CalculateSimiliarTraitsImpact(Player player, Player known_player){
            var known_traits = known_player.GetAllCharacters().SelectMany(c => c.traits);
            var player_traits = player.GetAllCharacters().SelectMany(c => c.traits);

            int similar_views_counter = known_traits.Count(iTrait => player_traits.Any(jTrait => jTrait.name == iTrait.name));
            int dissimilar_views_counter = known_traits.Count(iTrait => player_traits.All(jTrait => jTrait.name != iTrait.name));

            float current_relationship_level = similar_views_counter - (dissimilar_views_counter * DISIMILAIR_TRAIT_MULTIPLIER);

            return current_relationship_level;
        }

    }
}
