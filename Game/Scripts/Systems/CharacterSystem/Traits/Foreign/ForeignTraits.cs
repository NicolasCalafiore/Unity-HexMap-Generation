using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;
using static Terrain.RegionsEnums;


namespace Character {

    // Likes Similiar Government Type
    public class GovernanceTrait : ForeignTraitBase {
        public GovernanceTrait(): base(TraitManager.IDEOLOGICAL, "Likes Similiar Governments", 15) {}
        public override float GetTraitValue(Player player, Player other_player){
            return isSameGovernmentType(player, other_player) ? value : value * -1;
        }
        public override bool isActivated(Player other_player, Player player) => isSameGovernmentType(player, other_player);
        
        
    }

    // Likes Similiar Region Type
    public class RegionalTrait : ForeignTraitBase {
        public RegionalTrait() : base(TraitManager.REGIONAL_CONNECTION, "Likes Similiar Cultures", 15) {}
        public override float GetTraitValue(Player player, Player other_player){
            return isSameRegionType(player, other_player) ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player) => isSameRegionType(player, other_player);
        

    }

    // Dislikes Similiar Region Type
    public class CulturalConflictTrait : ForeignTraitBase {
        public CulturalConflictTrait() : base(TraitManager.HOMELAND, "Dislikes Similiar Cultures", 10) {}
        public override float GetTraitValue(Player player, Player other_player){
            return isSameRegionType(player, other_player) ?  value * -1 :  0;
        }
        public override bool isActivated(Player other_player, Player player) => isSameRegionType(player, other_player);
        
    }

    // Universal
    public class PeaceTrait : ForeignTraitBase {
        public PeaceTrait() : base(TraitManager.PEACE_PROMOTER, "Promotes Peace", 12) {}
        public override float GetTraitValue(Player player, Player other_player) => value;
        public override bool isActivated(Player other_player, Player player) => true;
    }

    // Universal
    public class WarTrait : ForeignTraitBase {
        public WarTrait() : base(TraitManager.WAR_MONGER, "War Hungry", 25){}
        public override float GetTraitValue(Player player, Player other_player) => value * -1;
        public override bool isActivated(Player other_player, Player player) => true;
        

    }

    // Universal
    public class DiplomatTrait : ForeignTraitBase {
        public DiplomatTrait() : base(TraitManager.DIPLOMAT, "Very Persuasive", 20){}
        public override float GetTraitValue(Player player, Player other_player) => value;
        public override bool isActivated(Player other_player, Player player) => true;
    }

    // Single Region Focused
    public class RegionRacistTrait : ForeignTraitBase {
        public RegionRacistTrait() : base(TraitManager.RACIST_REGION, "Discriminator Against a Region", 15){
            generic_region_target = RegionsEnums.GetRandomRegionLandType();
        }
        public override float GetTraitValue(Player player, Player other_player){
            return other_player.GetCapital().region_type == generic_region_target ? value * -1 : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().region_type == generic_region_target;
        }
    }

    // Single Player Focused
    public class Racist : ForeignTraitBase {
        public Racist(Player player) : base(TraitManager.RACIST, "Discriminatory Towards a Player", 15){
            this.generic_player_target = player.government.cabinet.foreign_advisor.GetRandomKnownPlayerNullable();
        }
        public override float GetTraitValue(Player other_player, Player player){
            return other_player == generic_player_target ? value * -1 : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return other_player == generic_player_target;
        }
    }

    // Likes Nearby Players
    public class FriendlyTrait : ForeignTraitBase {
        public FriendlyTrait() : base(TraitManager.NEIGHBORLY, "Likes Nearby Players", 15){
            generic_primary_int = 5;
        }
        public override float GetTraitValue(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < generic_primary_int ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < generic_primary_int;
        }
    }

    // Dislikes Nearby Players
    public class DefensiveTrait : ForeignTraitBase {
        public DefensiveTrait() : base(TraitManager.HOME_FRONT, "Dislikes Nearby Players", 20){
            generic_primary_int = 5;
        }

        public override float GetTraitValue(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < generic_primary_int ? value * -1 : 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < generic_primary_int;
        }
    }
}
