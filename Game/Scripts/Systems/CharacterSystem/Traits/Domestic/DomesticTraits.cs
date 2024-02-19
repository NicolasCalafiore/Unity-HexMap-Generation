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
    public class PeaceKeeper : DomesticTraitBase {
        public PeaceKeeper(){
            this.name = "Peace Keeper";
            this.description = "Peace";
            this.id = 1;

        }

        public override float GetTraitAlgorithmValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }

    }

    public class Financier : DomesticTraitBase {
        public Financier(){
            this.name = "Financier";
            this.description = "Financier";
            this.id = 2;
        }

        public override float GetTraitAlgorithmValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }
    }

    public class ProductionExpert : DomesticTraitBase {
        public ProductionExpert(){
            this.name = "Production Expert";
            this.description = "Production";
            this.id = 3;
        }

        public override float GetTraitAlgorithmValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }
    }

}