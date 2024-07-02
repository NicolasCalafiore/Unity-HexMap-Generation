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
    public static class DiplomacyManager
    {


        public static void GenerateStartingRelationships(){
            foreach(Player player in PlayerManager.player_list)
                foreach(Player known_player in player.GetKnownPlayers()){
                    float relationship = 0;
                    relationship += CalculateTraitRelationshipImpact(player, known_player); 
                    relationship += CalculateTraitComparisonsImpact(player, known_player);
                    player.government.cabinet.foreign_advisor.relations.Add(known_player, relationship);
                }
            
        }

        public static float CalculateTraitRelationshipImpact(Player player, Player known_player){
            float relationship = 0;
            foreach(TraitBase leader_trait in player.government.leader.traits)
                if(leader_trait is ForeignTraitBase)
                    relationship += ((ForeignTraitBase) leader_trait).GetTraitValue(player, known_player) * Leader.TRAIT_MULTIPLIER;
                
            foreach(ForeignTraitBase foreign_trait in player.government.cabinet.foreign_advisor.traits)
                relationship += foreign_trait.GetTraitValue(player, known_player);
            
        
            return relationship;
        }

        public static float CalculateTraitComparisonsImpact(Player player, Player known_player){
            float relationship = 0;
            foreach(TraitBase player_leader_trait in player.government.leader.traits)
                foreach(TraitBase other_leader_trait in known_player.government.leader.traits)
                    if(player_leader_trait == other_leader_trait)
                        relationship += player_leader_trait.value;
                    else if(player_leader_trait.banned_traits.Contains(other_leader_trait.Name))
                        relationship -= player_leader_trait.value;
            
            return relationship;
        }
    }
}
