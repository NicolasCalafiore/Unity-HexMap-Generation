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
    public class IdeologicalTrait : ForeignTraitBase {
        public IdeologicalTrait(){
            this.name = "Ideological";
            this.description = "Government";
            this.government_type_value = 5;
            this.id = 1;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            if(isSameGovernmentType(player, other_player)) return government_type_value;
            else return government_type_value * -1;
        }

        public override bool isActivated(Player player, Player other_player){
            return isSameGovernmentType(player, other_player);
        }
        
    }

    public class RegionalConnection : ForeignTraitBase {
        public RegionalConnection(){
            this.name = "RegionalConnection";
            this.description = "Regions";
            this.region_type_value = 5;
            this.id = 2;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            if(isSameRegionType(player, other_player)) return region_type_value;
            else return 0;
        }

        public override bool isActivated(Player player, Player other_player){
            return isSameRegionType(player, other_player);
        }

    }

    public class HomelandTrait : ForeignTraitBase {
        public HomelandTrait(){
            this.name = "Homeland";
            this.description = "Homeland";
            this.region_type_value = 8;
            this.id = 2;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            if(isSameRegionType(player, other_player)) return region_type_value * -1;
            else return region_type_value/2;
        }

        public override bool isActivated(Player player, Player other_player){
            return true;
        }
    }

    public class PeacePromoter : ForeignTraitBase {
        public PeacePromoter(){
            this.name = "Peace Promoter";
            this.description = "Peace";
            this.base_value = 5;
            this.id = 3;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            return base_value;
        }

        public override bool isActivated(Player player, Player other_player){
            return true;
        }
    }

    public class WarMonger : ForeignTraitBase {
        public WarMonger(){
            this.name = "War Monger";
            this.description = "War";
            this.base_value = 10;
            this.id = 3;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            return base_value * -1;
        }

        public override bool isActivated(Player player, Player other_player){
            return true;
        }

    }

    public class Diplomat : ForeignTraitBase {
        public Diplomat(){
            this.name = "Diplomat";
            this.description = "Diplomacy";
            this.base_value = 10;
            this.id = 3;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            return base_value;

        }

        public override bool isActivated(Player player, Player other_player){
            return true;
        }
    }
}