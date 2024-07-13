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

    public class StabilityPriority : CityPriority
    {
        private int stability_critical_point = 30;
        public override string Name { get => name; }
        public StabilityPriority(){
            this.name = "Stability";
        }
        public override MainPriority GetPriorityType() => MainPriority.Stability;

        public override void CalculatePriority(Player player, bool isDebug)
        {
            Rule rule = new Rule();

            rule.AddCondition(new List<bool>{player.GetAllTraitsStr().Contains(StabilityTrait.name)}, 1f);
            rule.AddCondition(new List<bool>{player.GetStability() < stability_critical_point, player.government_type == GovernmentType.Dictatorship}, 3f);
            rule.AddCondition(new List<bool>{player.GetStability() < stability_critical_point, player.government_type == GovernmentType.Tribalism}, 2f);
            rule.AddCondition(new List<bool>{player.GetStability() < stability_critical_point}, 1f);

            this.priority = rule.GetSum();
        }

    }
}