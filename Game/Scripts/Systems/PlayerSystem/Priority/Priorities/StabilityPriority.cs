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

    public class StabilityPriority : AIPriority
    {
        private int stability_critical_point = 30;
        public StabilityPriority(){
            this.name = "Stability";
        }
        public override MainPriority GetPriorityType() => MainPriority.Stability;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;

            
            if(player.GetAllTraitsStr().Contains(StabilityTrait.name))
                priority += 1;
                
            if(player.GetStability() < stability_critical_point)
                if(player.government_type == GovernmentType.Dictatorship)
                    priority += 3;
                
                else if (player.government_type == GovernmentType.Tribalism)
                    priority += 2;
                
                else
                    priority += 1;
            
            this.priority = priority;
        }

    }
}