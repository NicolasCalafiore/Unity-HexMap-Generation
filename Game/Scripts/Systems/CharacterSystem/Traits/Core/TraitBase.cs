using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;
using static Character.TraitEnums;

namespace Character {
    public abstract class TraitBase {
        public string name;
        public string description;
        public int id;
        public TraitType type;
        

        internal static TraitBase GetRandomLeaderTrait(Player player) 
        {
            if(Random.Range(0, 100) < 50) return DomesticTraitBase.GetRandomDomesticTrait(player);
            else return ForeignTraitBase.GetRandomForeignTrait(player);
        }

        public string GetName(){
            return name;
        }

    }
}