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




namespace AI {

    public class ReligionPriority : CityPriority
    {
        public int critical_beleif_level = 2;
        public override string Name { get => name; }
        public ReligionPriority(){
            this.name = "Religion";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player, bool isDebug)
        {
            Rule rule = new Rule();
            rule.AddCondition(new List<bool>{player.belief_level < critical_beleif_level, player.government_type == GovernmentType.Theocracy}, 2f);
            rule.AddCondition(new List<bool>{player.belief_level < critical_beleif_level}, 1f);
            this.priority = rule.GetSum();
        }

    }
}