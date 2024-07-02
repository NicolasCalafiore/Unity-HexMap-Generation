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

    public class NourishmentPriority : CityPriority
    {
        private int nourishment_critical_point = 20;
        public NourishmentPriority(){
            this.name = "Nourishment";
        }

        public override PlayerPriority GetPriorityType() => PlayerPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;
            if(player.GetNutrition() < nourishment_critical_point)
                priority += 1;

            this.priority = priority;
        }

    }
}