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

    public class EconomyPriority : CityPriority
    {
        private int wealth_critical_point = 500;
        public override string Name { get => name; }
        public EconomyPriority(){
            this.name = "Economy";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player, bool isDebug)
        {
            Rule rule = new Rule();
            
            rule.AddCondition(new List<bool>{player.wealth < wealth_critical_point, player.government_type == GovernmentType.Monarchy}, 2f);
            rule.AddCondition(new List<bool>{player.wealth < wealth_critical_point}, 1f);
            rule.AddCondition(new List<bool>{player.GetAllTraitsStr().Contains(WealthAdmirer.name)}, 1f);

            this.priority = rule.GetSum();
        }

        public int GetCriticalPoint(){
            return wealth_critical_point;
        }

    }
}