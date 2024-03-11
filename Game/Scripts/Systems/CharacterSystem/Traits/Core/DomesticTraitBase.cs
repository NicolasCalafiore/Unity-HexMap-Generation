using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;

namespace Character {
    public abstract class DomesticTraitBase : TraitBase {
        // Traits for Domestic Advisor

        public abstract float GetTraitAlgorithmValue(Player player);
        public abstract bool isActivated(Player player);

        public DomesticTraitBase(string name, string description, int value) : base(name, description, value){}

        public static DomesticTraitBase GetRandomDomesticTrait(Player player){

            List<DomesticTraitBase> trait_list = new List<DomesticTraitBase>(){
                new PeaceKeeper(),
                new Financier(),
                new ProductionExpert()
            };

            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }
    }
}