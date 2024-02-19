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

namespace Character {
    public abstract class TraitBase {
        public string name;
        public string description;
        public int id;

        internal static TraitBase GetRandomLeaderTrait() 
        {
            if(Random.Range(0, 100) < 50) return DomesticTraitBase.GetRandomDomesticTrait();
            else return ForeignTraitBase.GetRandomForeignTrait();
        }

        public string GetName(){
            return name;
        }

    }
}