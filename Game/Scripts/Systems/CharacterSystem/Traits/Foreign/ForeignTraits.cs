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
using Diplomacy;


namespace Character {


    // Likes Similiar Government Type
    public class GovernanceTrait : ForeignTraitBase {
        public static string name = "Governance";
        public override string Name { get => name;}
        public GovernanceTrait(): base("Likes Similiar Governments", 10) {
            
        }
        public override float GetTraitValue(Player player, Player other_player){
            return isSameGovernmentType(player, other_player) ? value : value * -1;
        }
        public override bool isActivated(Player other_player, Player player) => isSameGovernmentType(player, other_player);

    }

    // Likes Similiar Region Type
    public class RegionalTrait : ForeignTraitBase {
        public static string name = "Regional";
        public override string Name { get => name;}
        public RegionalTrait() : base("Likes Similiar Cultures", 5) {
            isBlankEffect = true;
            banned_traits = new List<string>(){GovernanceTrait.name};
        }
        public override float GetTraitValue(Player player, Player other_player){
            return isSameRegionType(player, other_player) ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player) => isSameRegionType(player, other_player);
        

    }

    // Dislikes Similiar Region Type
    public class RegionalConflictTrait : ForeignTraitBase {
        private static string name = "Regional Conflict";
        public override string Name { get => name;}
        public RegionalConflictTrait() : base("Dislikes Similiar Regions", -5) {
            isBlankEffect = true;
            banned_traits = new List<string>(){RegionalTrait.name};
        }
        public override float GetTraitValue(Player player, Player other_player){
            return isSameRegionType(player, other_player) ?  value :  0;
        }
        public override bool isActivated(Player other_player, Player player) => isSameRegionType(player, other_player);
        
    }


    // Universal
    public class Rude : ForeignTraitBase {
        public static string name = "Rude";
        public override string Name { get => name;}
        public Rude() : base("Rude", -7){
            isBlankEffect = true;
            banned_traits = new List<string>(){DiplomatTrait.name};
        }
        public override float GetTraitValue(Player player, Player other_player) => value;
        
        public override bool isActivated(Player other_player, Player player) => true;
        
    }

    // Universal
    public class DiplomatTrait : ForeignTraitBase {
        public static string name = "Diplomat";
        public override string Name { get => name;}
        public DiplomatTrait() : base("Very Persuasive", 7){
            isBlankEffect = true;
            banned_traits = new List<string>(){Rude.name};

        }
        public override float GetTraitValue(Player player, Player other_player) => value;
        public override bool isActivated(Player other_player, Player player) => true;
    }

    // Single Region Focused
    public class RegionRacistTrait : ForeignTraitBase {
        public static string name = "Region Racist";
        public override string Name { get => name;}
        public RegionRacistTrait() : base("Discriminator Against a Region", -10){
            region_target = RegionsEnums.GetRandomRegionLandType();
        }
        public override float GetTraitValue(Player player, Player other_player){
            return other_player.GetCapital().region_type == region_target ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().region_type == region_target;
        }
    }

    // Single Player Focused
    public class Foe : ForeignTraitBase {
        public static string name = "Foe";
        public override string Name { get => name;}
        public Foe(Player player) : base("Discriminatory Towards a Player", -12){
            repeatable = true;
            this.player_target = player.government.cabinet.foreign_advisor.GetRandomKnownPlayerNullable();
        }
        public override float GetTraitValue(Player other_player, Player player){
            return other_player == player_target ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return other_player == player_target;
        }
    }

    // Likes Nearby Players
    public class NeighborlyTrait : ForeignTraitBase {
        public static string name = "Neighborly";
        public override string Name { get => name;}
        public NeighborlyTrait() : base("Likes Nearby Players", 11){
            integer_storage = 5;
            banned_traits = new List<string>(){DefensiveTrait.name};
        }
        public override float GetTraitValue(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < integer_storage ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < integer_storage;
        }
    }

    // Dislikes Nearby Players
    public class DefensiveTrait : ForeignTraitBase {
        public static string name = "Defensive";
        public override string Name { get => name;}
        public DefensiveTrait() : base("Dislikes Nearby Players", -12){
            integer_storage = 5;
            banned_traits = new List<string>(){NeighborlyTrait.name};
        }

        public override float GetTraitValue(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < integer_storage ? value : 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < integer_storage;
        }
    }


    public class StabilityTrait : ForeignTraitBase {
        public static string name = "Stability";
        public override string Name { get => name;}
        public StabilityTrait() : base("Prefers players with stable governments", 6){
            integer_storage = 75;
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.GetStability() > integer_storage;
        }
    }

    public class ScientificTrait : ForeignTraitBase {
        public static string name = "Science";
        public override string Name { get => name;}
        public ScientificTrait() : base("Prefers players with a focus on science", 6){
            integer_storage = 3;
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.knowledge_level > integer_storage;
        }
    }

    public class ContinentalUniter : ForeignTraitBase {
        public static string name = "Continental Uniter";
        public override string Name { get => name;}
        public ContinentalUniter() : base("Prefers players within his continent", 8){
            isBlankEffect = true;
            banned_traits = new List<string>(){ContinentalClaimer.name};
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().hex_territory_list[0].continent_id == player.GetCapital().hex_territory_list[0].continent_id;
        }
    }

    public class ContinentalClaimer : ForeignTraitBase {
        public static string name = "Continental Claimer";
        public override string Name { get => name;}
        public ContinentalClaimer() : base("Dislikes players within his continent", -8){
            isBlankEffect = true;
            banned_traits = new List<string>(){ContinentalUniter.name};
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().hex_territory_list[0].continent_id == player.GetCapital().hex_territory_list[0].continent_id;
        }
    }

    public class WealthAdmirer : ForeignTraitBase {
        public static string name = "Wealth Admirer";
        public override string Name { get => name;}
        public WealthAdmirer() : base("Prefers players with high wealth", 8){
            integer_storage = 1500;
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.wealth > integer_storage;
        }
    }

    public class PovertyDiscriminator : ForeignTraitBase {
        public static string name = "Poverty Discriminator";
        public override string Name { get => name;}
        public PovertyDiscriminator() : base("Dislikes players with low wealth", -8){
            integer_storage = 500;
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.wealth < integer_storage;
        }
    }

    public class CultureClaimer : ForeignTraitBase {
        public static string name = "Culture Claimer";
        public override string Name { get => name;}
        public CultureClaimer() : base("Dislikes Players with Same Culture", -12){
            isBlankEffect = true;
            banned_traits = new List<string>(){CultureUniter.name};
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().hex_territory_list[0].culture_id == player.GetCapital().hex_territory_list[0].culture_id;
        }
    }


    public class CultureUniter : ForeignTraitBase {
        public static string name = "Culture Uniter";
        public override string Name { get => name;}
        public CultureUniter() : base("Likes Players with Same Culture", 15){
            isBlankEffect = true;
            banned_traits = new List<string>(){CultureClaimer.name};
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            return 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().hex_territory_list[0].culture_id == player.GetCapital().hex_territory_list[0].culture_id;
        }
    }

    public class CultureBias : ForeignTraitBase {
        public static string name = "Culture Bias";
        public override string Name { get => name;}
        public CultureBias() : base("Dislikes Players with Different Culture", 11){
            isBlankEffect = true;
            banned_traits = new List<string>(){CultureUniter.name, CultureClaimer.name};
        }

        public override float GetTraitValue(Player other_player, Player player){
            if(isActivated(other_player, player)) return value;
            else return -value;
        }

        public override bool isActivated(Player other_player, Player player){
            return other_player.GetCapital().hex_territory_list[0].culture_id == player.GetCapital().hex_territory_list[0].culture_id;
        }
    }

}
