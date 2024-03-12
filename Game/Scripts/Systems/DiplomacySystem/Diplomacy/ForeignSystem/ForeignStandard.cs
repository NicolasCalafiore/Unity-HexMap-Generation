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
using static Terrain.ForeignEnums;


namespace Diplomacy
{
    public class ForeignStandard : ForeignStrategy
    {
        private const float LEADER_MULTIPLIER = 1.5f;
        private const float DISIMILAIR_TRAIT_MULTIPLIER = .1f;

        public static List<string> DEBUG_MESSAGE = new List<string>();
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
        
            foreach(ForeignTraitBase i in player.government.GetForeignByIndex(0).traits){
                DEBUG_MESSAGE.Add($"{player.government.GetForeignByIndex(0).GetName()}: {i.name} : {i.isActivated(player, known_player)} --> {i.GetTraitValue(known_player, player)}");
                current_relationship_level += i.GetTraitValue(known_player, player);
            }
            
            foreach(TraitBase i in player.government.leader.traits)
                if(i is ForeignTraitBase){
                    DEBUG_MESSAGE.Add($"{player.government.leader.GetName()}: {((ForeignTraitBase) i).name} : {((ForeignTraitBase) i).isActivated(player, known_player) } --> {((ForeignTraitBase) i).GetTraitValue(known_player, player) * LEADER_MULTIPLIER}");
                    current_relationship_level += ((ForeignTraitBase) i).GetTraitValue(known_player, player) * LEADER_MULTIPLIER;
                }

            DEBUG_MESSAGE.Add($"Total by Trait: {current_relationship_level}");
            return current_relationship_level;
        }

        //This method calculates the relationship between two players
        //It takes into account the similiar traits of the characters
        public float CalculateSimiliarTraitsImpact(Player player, Player known_player){
            var known_traits = known_player.GetAllCharacters().SelectMany(c => c.traits);
            var player_traits = player.GetAllCharacters().SelectMany(c => c.traits);

            int similar_views_counter = known_traits.Count(iTrait => player_traits.Any(jTrait => jTrait.name == iTrait.name));
            int dissimilar_views_counter = known_traits.Count(iTrait => player_traits.All(jTrait => jTrait.name != iTrait.name));

            float current_relationship_level = similar_views_counter - (dissimilar_views_counter * DISIMILAIR_TRAIT_MULTIPLIER);
            
            DEBUG_MESSAGE.Add($"dissimilar_views_counter: {dissimilar_views_counter} * {DISIMILAIR_TRAIT_MULTIPLIER}");
            DEBUG_MESSAGE.Add($"similar_views_counter: {similar_views_counter}");
            DEBUG_MESSAGE.Add($"Total by Trait Similarity/Disimiarity: {current_relationship_level}");
            return current_relationship_level;
        }

        //Calculates the impact of one players relationship due to the relationship of another player towards itself
        //Requires relationships to already be calculated
        public override float CalculateRelationshipDependantRelationshipImpact(Player player, Player known_player){
            var other_rel = known_player.government.GetForeignByIndex(0).GetRelationshipLevel(player);
            if(other_rel == RelationshipLevel.Ally)
                return (float) RelationshipLevel.Ally/2;
            else if(other_rel == RelationshipLevel.Friendly)
                return (float) RelationshipLevel.Friendly/2;
            else if(other_rel == RelationshipLevel.Neutral)
                return 0;
            else if(other_rel == RelationshipLevel.Unfriendly)
                return (float) RelationshipLevel.Unfriendly/2;
            else
                return (float) RelationshipLevel.Enemy/2;
        }

    }
}
