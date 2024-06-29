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
            foreach(Player player in PlayerManager.player_list){
                foreach(Player known_player in player.GetKnownPlayers()){
                    float relationship = 0;
                    relationship += CalculateBaseRelationship(player, known_player);
                     relationship += CalculateTraitRelationshipImpact(player, known_player); 
                    // relationship += CalculateSimiliarTraitsImpact(player, known_player);
                    player.government.cabinet.foreign_advisor.relations.Add(known_player, relationship);
                }
            }
        }

        public static float CalculateBaseRelationship(Player player, Player known_player){
            float relationship = 0;
            if(player.government_type == known_player.government_type) relationship += 10;
            if(player.home_region == known_player.home_region) relationship += 10;
            
            return relationship;
        }

        public static float CalculateTraitRelationshipImpact(Player player, Player known_player){
            float relationship = 0;
            foreach(TraitBase leader_trait in player.government.leader.traits)
                if(leader_trait is ForeignTraitBase)
                    relationship += ((ForeignTraitBase) leader_trait).GetTraitValue(player, known_player);
                
            foreach(ForeignTraitBase foreign_trait in player.government.cabinet.foreign_advisor.traits){
                relationship += foreign_trait.GetTraitValue(player, known_player);
            }
            
            return relationship;
        }

        public static float CalculateSimiliarTraitsImpact(Player player, Player known_player){
            float relationship = 0;
            foreach(ForeignTraitBase foreign_trait in player.government.cabinet.foreign_advisor.traits){
                if(known_player.government.cabinet.foreign_advisor.traits.Contains(foreign_trait)){
                    relationship += 5;
                }
            }
            foreach(TraitBase foreign_trait in player.government.leader.traits){
                if(known_player.government.cabinet.foreign_advisor.traits.Contains(foreign_trait)){
                    relationship += 5;
                }
            }

            return relationship;
        }
    }
}
