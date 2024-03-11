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
    public class PeaceKeeper : DomesticTraitBase {
        public PeaceKeeper(): base(TraitManager.PEACE_KEEPER, "Increases Stability", 5){}

        public override float GetTraitAlgorithmValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }

    }

    public class Financier : DomesticTraitBase {
        public Financier() : base(TraitManager.FINANCIER, "Extra Gold", 5){}

        public override float GetTraitAlgorithmValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }
    }

    public class ProductionExpert : DomesticTraitBase {
        public ProductionExpert() : base(TraitManager.PRODUCTION_EXPERT, "Increased Production", 5){}

        public override float GetTraitAlgorithmValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }
    }

}