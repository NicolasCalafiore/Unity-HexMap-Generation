using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players;
using Terrain;
using UnityEngine;


namespace Diplomacy
{
    public abstract class ForeignStrategy
    {
        protected float capital_distance_level;
        protected float territory_distance_level;
        protected float similiar_government_level;
        protected float personal_relationships_level;
        protected float similiar_region_level;

        protected float capital_distance_weight;
        protected float territory_distance_weight;
        protected float similiar_government_weight;
        protected float personal_relationships_weight;
        protected float similiar_region_weight;

        
        public void SetStrategyWeights(){
            capital_distance_weight = 25;
            territory_distance_weight = 0;
            similiar_government_weight = 45;
            personal_relationships_weight = 0;
            similiar_region_weight = 30;
        }

        public abstract void SetStrategyValues(Player owner, Player foreigner);

        public float GenerateStartingRelationship(){
            float capital_distance_final = capital_distance_level * (int)capital_distance_weight;
            float similiar_government_final = similiar_government_level * (int)similiar_government_weight;
            float similiar_region_final = similiar_region_level * (int)similiar_region_weight;


            //Needs to add up to 100
            float total_final = capital_distance_final + 
                                similiar_government_final + 
                                similiar_region_final;


            
            return total_final;
        }

        public void RelationshipBreakdown(Player owner, Player foreigner){

            float capital_distance_final = capital_distance_level * (int)capital_distance_weight;
            float similiar_government_final = similiar_government_level * (int)similiar_government_weight;
            float similiar_region_final = similiar_region_level * (int)similiar_region_weight;


            //Needs to add up to 100
            float total_final = capital_distance_final + 
                                similiar_government_final + 
                                similiar_region_final;


            List<string> message = new List<string>();
            message.Add(owner.GetOfficialName() + " <--> " + foreigner.GetOfficialName() + "\n");
            message.Add("Capital Distance Level: " + capital_distance_level);
            message.Add("Similiar Government Level: " + similiar_government_level);
            message.Add("Similiar Region Level: " + similiar_region_level);
            message.Add("Capital Distance Impact: " + capital_distance_final);
            message.Add("Similiar Government Impact: " + similiar_government_final);
            message.Add("Similiar Region Impact: " + similiar_region_final);
            message.Add("Total Impact: " + total_final);
            message.Add("Status: " + GetRelationshipLevel((int)total_final));


            DebugHandler.DisplayMessage(message);
        }


        public RelationshipLevel GetRelationshipLevel(int level){
            if(level >= (int)RelationshipLevel.Ally){
                return RelationshipLevel.Ally;
            }
            else if(level >= (int)RelationshipLevel.Friendly){
                return RelationshipLevel.Friendly;
            }
            else if(level >= (int)RelationshipLevel.Neutral){
                return RelationshipLevel.Neutral;
            }
            else if(level >= (int)RelationshipLevel.Unfriendly){
                return RelationshipLevel.Unfriendly;
            }
            else{
                return RelationshipLevel.Enemy;
            }

        }



        public enum RelationshipLevel{
            Ally = 90,
            Friendly = 70,
            Neutral = 35,
            Unfriendly = 15,
            Enemy = 0
        }






    }
}

