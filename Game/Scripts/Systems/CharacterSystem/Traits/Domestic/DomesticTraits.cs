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
        public static string name = "Peace Keeper";
        public override string Name { get => name;}
        public PeaceKeeper(): base("Increases Stability", 5){

        }

        public override float GetTraitValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }

    }

    public class Financier : DomesticTraitBase {
        public static string name = "Financier";
        public override string Name { get => name;}
        public Financier() : base("Extra Gold", 5){
        }

        public override float GetTraitValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }
    }

    public class ProductionExpert : DomesticTraitBase {
        public static string name = "Production Expert";
        public override string Name { get => name;}
        public ProductionExpert() : base("Increased Production", 5){
        }

        public override float GetTraitValue(Player player){
            throw new NotImplementedException();
        }

        public override bool isActivated(Player player){
           throw new NotImplementedException();
        }
    }

}