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

    public class SciencePriority : AIPriority
    {
        public int knowledge_critical_point = 2;
        public SciencePriority(){
            this.name = "Science";
        }
        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            int priority = 0;
            
            if(player.GetAllTraitsStr().Contains(ScientificTrait.name))
                priority += 1;

            if(player.knowledge_level < knowledge_critical_point)
                if(player.government_type == GovernmentType.Democracy)
                    priority += 2;
                else
                    priority += 1;

            this.priority = priority;
        }

    }
}