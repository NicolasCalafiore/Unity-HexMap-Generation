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
    public abstract class DomesticTraitBase : TraitBase {

        public abstract float GetTraitAlgorithmValue(Player player);
        public abstract bool isActivated(Player player);

        public static DomesticTraitBase GetRandomDomesticTrait(){
            List<DomesticTraitBase> trait_list = new List<DomesticTraitBase>();
            trait_list.Add(new PeaceKeeper());
            trait_list.Add(new Financier());
            trait_list.Add(new ProductionExpert());

            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }
    }
}