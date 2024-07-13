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
using Unity.Mathematics;




namespace AI {

    public class NourishmentPriority : CityPriority
    {
        private float nourishment_critical_point = 20;
        public override string Name { get => name; }
        public NourishmentPriority(){
            this.name = "Nourishment";
        }

        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player, bool isDebug)
        {

            Rule rule = new Rule();
            rule.AddCondition(new List<bool>{player.GetNutrition() < nourishment_critical_point, player.government_type == GovernmentType.Monarchy}, 2f);

            foreach(HexTile tile in player.GetCapital().GetNourishmentTerritories())
                rule.AddCondition(new List<bool>{tile.upgrade_level < tile.max_level}, .35f);

            this.priority = rule.GetSum();
        }

    }
}