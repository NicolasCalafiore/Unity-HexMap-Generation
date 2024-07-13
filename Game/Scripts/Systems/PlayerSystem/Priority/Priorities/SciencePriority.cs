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
using Character;




namespace AI {

    public class SciencePriority : CityPriority
    {
        public int knowledge_critical_point = 2;
        public override string Name { get => name; }
        public SciencePriority(){
            this.name = "Science";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player, bool isDebug)
        {
            Rule rule = new Rule();

            rule.AddCondition(new List<bool>{player.GetAllTraitsStr().Contains(ScientificTrait.name)}, 1f);
            rule.AddCondition(new List<bool>{player.knowledge_level < knowledge_critical_point, player.government_type == GovernmentType.Democracy}, 2f);
            rule.AddCondition(new List<bool>{player.knowledge_level < knowledge_critical_point}, 1f);
            this.priority = rule.GetSum();
        }

    }
}