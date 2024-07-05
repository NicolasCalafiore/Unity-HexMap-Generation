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
using static Terrain.ForeignEnums;
using Character;
using Cities;




namespace AI {

    public class DefensePriority : AIPriority
    {
        private int defense_diameter = 5;
        public override string Name { get => name; }
        public DefensePriority(){
            this.name = "Defense";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;
            foreach(Player neighbor in player.government.cabinet.foreign_advisor.GetKnownPlayers()){
                Vector2 player_capital = player.GetCapitalCoordinate();
                Vector2 neighbor_capital = neighbor.GetCapitalCoordinate();

                if(player.GetAllTraitsStr().Contains(ContinentalClaimer.name))
                     foreach(Player known_players in player.government.cabinet.foreign_advisor.GetKnownPlayers())
                        if(CityManager.city_to_hex[known_players.GetCapital()].continent_id == CityManager.city_to_hex[player.GetCapital()].continent_id)
                            priority += 2;

                if(PathFinding.GetManhattanDistance(player_capital, neighbor_capital) < defense_diameter){

                    if(player.GetAllTraitsStr().Contains(DefensiveTrait.name))
                        priority += 1;

                    if(player.GetAllTraitsStr().Contains(Foe.name))
                        foreach(TraitBase trait in player.GetAllTraits())
                            if(trait is Foe)
                                if( (trait as Foe).player_target == neighbor)
                                    priority += 3;
                    
                    if(player.government.cabinet.foreign_advisor.GetRelationshipLevel(neighbor) < RelationshipLevel.Neutral)
                        priority += 1;
                    
                    if(player.government.cabinet.foreign_advisor.GetRelationshipLevel(neighbor) < RelationshipLevel.Unfriendly)
                        priority += 1;


                    
                }
                
            }

             this.priority = priority;
        }


    }
}