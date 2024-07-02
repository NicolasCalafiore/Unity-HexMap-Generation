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




namespace AI {

    public class DefensePriority : CityPriority
    {
        public DefensePriority(){
            this.name = "Defense";
        }
        public override PlayerPriority GetPriorityType() => PlayerPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;
            foreach(Player neighbor in player.government.cabinet.foreign_advisor.GetKnownPlayers()){
                Vector2 player_capital = player.GetCapitalCoordinate();
                Vector2 neighbor_capital = neighbor.GetCapitalCoordinate();

                if(PathFinding.GetManhattanDistance(player_capital, neighbor_capital) < 5)
                    if(player.government.cabinet.foreign_advisor.GetRelationshipLevel(neighbor) < RelationshipLevel.Neutral)
                        priority += 3;
                    else 
                        priority += 1;
                
            }

             this.priority = priority;
        }


    }
}