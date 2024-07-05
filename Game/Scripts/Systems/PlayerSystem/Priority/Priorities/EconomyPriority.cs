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

    public class EconomyPriority : AIPriority
    {
        private int wealth_critical_point = 500;
        public override string Name { get => name; }
        public EconomyPriority(){
            this.name = "Economy";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;
            if(player.wealth < wealth_critical_point)
                if(player.government_type == GovernmentType.Monarchy)
                    priority += 2;
                else
                    priority += 1;

    
            if(player.GetAllTraitsStr().Contains(WealthAdmirer.name))
                priority += 1;

            this.priority = priority;
        }

        public int GetCriticalPoint(){
            return wealth_critical_point;
        }

    }
}