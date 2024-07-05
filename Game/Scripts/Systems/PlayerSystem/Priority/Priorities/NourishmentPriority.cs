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

    public class NourishmentPriority : AIPriority
    {
        private float nourishment_critical_point = 20;
        public override string Name { get => name; }
        public NourishmentPriority(){
            this.name = "Nourishment";
        }

        public override MainPriority GetPriorityType() => MainPriority.Religion;

        public override void CalculatePriority(Player player)
        {
            float priority = 0;

            if(player.GetNutrition() < nourishment_critical_point)
                priority += 1;

            foreach(HexTile tile in player.GetCapital().GetNourishmentTerritories())
                if(tile.upgrade_level < tile.max_level)
                    priority += .35f;

            this.priority = priority;
        }

    }
}