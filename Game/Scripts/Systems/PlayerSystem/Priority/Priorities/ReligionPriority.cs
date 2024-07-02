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

    public class ReligionPriority : AIPriority
    {
        public int critical_beleif_level = 2;
        public ReligionPriority(){
            this.name = "Religion";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;
            if(player.belief_level < critical_beleif_level)
                if(player.government_type == GovernmentType.Theocracy)
                    priority += 2;
                    
                else
                    priority += 1;

            this.priority = priority;
        }

    }
}