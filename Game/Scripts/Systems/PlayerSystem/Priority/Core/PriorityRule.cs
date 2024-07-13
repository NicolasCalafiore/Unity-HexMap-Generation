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

    public class PriorityReq
    {
        public string name;
        int value;
        Player player;
        Func<Player, bool> conditionsFunc;

        public PriorityReq(string name, Func<Player, bool> conditional, int value){
            this.name = name;
            this.value = value;
            Func<Player, bool> conditions = conditional;

        }


        
        public bool CheckConditions(){
            return conditionsFunc(player);
        }

    }
}