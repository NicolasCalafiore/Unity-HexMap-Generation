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
using static Terrain.RegionsEnums;


namespace Character {
    public class IdeologicalTrait : ForeignTraitBase {
        public IdeologicalTrait(){
            this.name = "Ideological";
            this.description = "Government";
            this.government_type_value = 5;
            this.id = 1;
            this.type = TraitType.Foreign;
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
            this.type = TraitType.Foreign;
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
            this.type = TraitType.Foreign;
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
            this.type = TraitType.Foreign;
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
            this.base_value = 7;
            this.id = 3;
            this.type = TraitType.Foreign;
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
            this.base_value = 7;
            this.id = 3;
            this.type = TraitType.Foreign;
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            return base_value;

        }

        public override bool isActivated(Player player, Player other_player){
            return true;
        }
    }

    public class RacistRegion : ForeignTraitBase {
        public RacistRegion(){
            HexRegion region_type = RegionsEnums.GetRandomRegionLandType();

            this.name = "Discrimatory towards " + region_type;
            this.description = "Racist";
            this.base_value = 5;
            this.id = 4;
            this.type = TraitType.Foreign;
            this.region_type = region_type; 
        }

        public override float GetTraitAlgorithmValue(Player player, Player other_player){
            if(other_player.GetCityByIndex(0).GetRegionType() == region_type) return base_value * -1;
            else return 0;
        }

        public override bool isActivated(Player player, Player other_player){
             if(other_player.GetCityByIndex(0).GetRegionType() == region_type) return true;
                else return false;
        }
    }

    public class RacistPlayer : ForeignTraitBase {
        public RacistPlayer(Player player){
            this.player_target = player.GetGovernment().GetForeign(0).GetRandomKnownPlayer();
            this.name = "Discrimatory towards " + player_target.GetOfficialName();
            this.description = "Racist";
            this.base_value = 5;
            this.id = 4;
            this.type = TraitType.Foreign;
        }

        public override float GetTraitAlgorithmValue(Player other_player, Player player){
            if(other_player == player_target) return base_value * -1;
            else return 0;
        }

        public override bool isActivated(Player other_player, Player player){
             if(other_player == player_target) return true;
                else return false;
        }
    }

    public class Neighborly : ForeignTraitBase {
        public Neighborly(){
            this.name = "Neighborly";
            this.description = "Neighborly";
            this.base_value = 5;
            this.id = 4;
            this.type = TraitType.Foreign;
        }

        public override float GetTraitAlgorithmValue(Player other_player, Player player){
            Vector2 position = player.GetCityByIndex(0).GetColRow();
            Vector2 other_position = other_player.GetCityByIndex(0).GetColRow();

            if(PathFinding.GetTileDistance(position, other_position) < 5) return base_value * -1;
            else return 0;
        }

        public override bool isActivated(Player other_player, Player player){
             if(other_player == player_target) return true;
                else return false;
        }
    }

    
    public class HomeFront : ForeignTraitBase {
        public HomeFront(){
            this.name = "HomeFront";
            this.description = "HomeFront";
            this.base_value = 5;
            this.id = 4;
            this.type = TraitType.Foreign;
        }

        public override float GetTraitAlgorithmValue(Player other_player, Player player){
            Vector2 position = player.GetCityByIndex(0).GetColRow();
            Vector2 other_position = other_player.GetCityByIndex(0).GetColRow();

            if(PathFinding.GetTileDistance(position, other_position) < 5) return base_value;
            else return 0;
        }

        public override bool isActivated(Player other_player, Player player){
             if(other_player == player_target) return true;
                else return false;
        }
    }
}
