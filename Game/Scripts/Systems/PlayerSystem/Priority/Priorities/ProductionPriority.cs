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

    public class ProductionPriority : AIPriority
    {
        private int production_critical_point = 20;
        public ProductionPriority(){
            this.name = "Production";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;

            if(player.GetProduction() < production_critical_point)
                priority += 1;

            if(player.GetAllTraitsStr().Contains(ProductionExpert.name))
                priority += 1;
                
                

            this.priority = priority;
        }

    }
}