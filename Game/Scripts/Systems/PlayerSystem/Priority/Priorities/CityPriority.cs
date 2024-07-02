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

    public abstract class AIPriority
    {
        public string name;
        public float priority;
        public abstract MainPriority GetPriorityType();
        public abstract void CalculatePriority(Player player);
        

    }

}