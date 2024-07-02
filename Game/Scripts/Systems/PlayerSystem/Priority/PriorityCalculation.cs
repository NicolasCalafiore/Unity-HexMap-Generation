using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Terrain;
using static Terrain.PriorityEnums;
using Unity.IO.LowLevel.Unsafe;
using static Terrain.GovernmentEnums;




namespace Cities {

    public static class PriorityCalculation
    {

        public static PlayerPriority CalculateMainPriority(Player player){

            List<string> message = new List<string>();
            Dictionary<PlayerPriority, float> priority_values = new Dictionary<PlayerPriority, float>();

            foreach(PlayerPriority priority in PriorityEnums.GetPlayerPriorities())
                priority_values.Add(priority, 0);
            

            if(player.GetStability() < player.stability_critical_point)
                if(player.government_type == GovernmentType.Dictatorship){
                    priority_values[PlayerPriority.Stability] += 3;
                }
                else if (player.government_type == GovernmentType.Tribalism){
                    priority_values[PlayerPriority.Stability] += 2;
                }
                else{
                    priority_values[PlayerPriority.Stability] += 1;
                }

            
            if(player.belief_level < 2)
                if(player.government_type == GovernmentType.Theocracy){
                    priority_values[PlayerPriority.Religion] += 3;
                    }
                else{
                    priority_values[PlayerPriority.Religion] += 1f;
                    }

            if(player.wealth < 500)
                if(player.government_type == GovernmentType.Monarchy){
                    priority_values[PlayerPriority.Economy] += 3;
                    }
                else{
                    priority_values[PlayerPriority.Economy] += 1f;
                    }

            if(player.knowledge_level < 2)
                if(player.government_type == GovernmentType.Democracy){
                    priority_values[PlayerPriority.Science] += 3;
                    }
                else{
                    priority_values[PlayerPriority.Science] += 1f;
                    }

            if(player.GetNutrition() < player.nourishment_critical_point){
                priority_values[PlayerPriority.Nourishment] += 1f;
                }

            if(player.GetProduction() < player.production_critical_point){
                priority_values[PlayerPriority.Production] += 1f;
                }

            
            foreach(Player neighbor in player.government.cabinet.foreign_advisor.GetKnownPlayers()){
                Vector2 player_capital = player.GetCapitalCoordinate();
                Vector2 neighbor_capital = neighbor.GetCapitalCoordinate();

                if(PathFinding.GetManhattanDistance(player_capital, neighbor_capital) < 5){
                    priority_values[PlayerPriority.Defense] += 1f;
                }
            }

            return GetHighestPriority(priority_values);
        }

        private static PlayerPriority GetHighestPriority(Dictionary<PlayerPriority, float> priority_values)
        {
            PlayerPriority highest_priority = PlayerPriority.Economy;
            float highest_value = 0;

            foreach(KeyValuePair<PlayerPriority, float> priority in priority_values){
                if(priority.Value > highest_value){
                    highest_priority = priority.Key;
                    highest_value = priority.Value;
                }
            }

            return highest_priority;
        }
    }
}