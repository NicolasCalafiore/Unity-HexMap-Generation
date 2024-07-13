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

    public class DefensePriority : CityPriority
    {
        private int defense_diameter = 5;
        private int continental_conflict_diameter = 10;
        public override string Name { get => name; }
        public DefensePriority(){
            this.name = "Defense";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player, bool isDebug)
        {
            Rule rule = new Rule();

            List<Player> neighbors = player.government.cabinet.foreign_advisor.GetKnownPlayers();
            Vector2 player_capital = player.GetCapitalCoordinate();

            foreach(Player neighbor in neighbors){
                Vector2 neighbor_capital = neighbor.GetCapitalCoordinate();
                int capital_distance = PathFinding.GetManhattanDistance(player_capital, neighbor_capital);

                rule.AddCondition(new List<bool>{player.HasTrait(ContinentalClaimer.name), 
                                                PlayerUtils.HasSameCapitalContinent(player, neighbor),
                                                PathFinding.GetManhattanDistance(player_capital, neighbor_capital) < continental_conflict_diameter,}, .10f);

                rule.AddCondition(new List<bool>{capital_distance < defense_diameter, player.HasTrait(DefensiveTrait.name)}, 1f);

                    if(player.HasTrait(Foe.name))
                        foreach(TraitBase trait in player.GetAllTraits())
                            if(trait is Foe)
                                rule.AddCondition(new List<bool>{(trait as Foe).player_target == neighbor}, 1f);

                    rule.AddCondition(new List<bool>{player.HasTrait(GovernanceTrait.name), neighbor.government_type != player.government_type}, 2f);

                    rule.AddCondition(new List<bool>{player.GetRelationshipLevel(neighbor) < RelationshipLevel.Neutral}, 1f);  
                    rule.AddCondition(new List<bool>{player.GetRelationshipLevel(neighbor) < RelationshipLevel.Unfriendly}, 1f);
                    rule.AddCondition(new List<bool>{player.GetRelationshipLevel(neighbor) < RelationshipLevel.Unfriendly}, 1f);

                }

                 this.priority = rule.GetSum();
                
            }

       

             
        }



    }
