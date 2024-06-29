using UnityEngine;
using Terrain;
using Cities;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;
using static Terrain.RegionsEnums;

namespace Character {
    public abstract class ForeignTraitBase : TraitBase {
        protected HexRegion generic_region_target;
        protected Player generic_player_target;
        protected int generic_primary_int;

        public abstract bool isActivated(Player player, Player other_player);

        public ForeignTraitBase(string name, string description, int value) : base(name, description, value){}

        public abstract float GetTraitValue(Player player, Player known_player);

        public static ForeignTraitBase GetRandomForeignTrait(Player player){

            //Non-Dependent traits
            List<ForeignTraitBase> trait_list = new List<ForeignTraitBase>(){
                new GovernanceTrait(),
                new RegionalTrait(),
                new CulturalConflictTrait(),
                new PeaceTrait(),
                new WarTrait(),
                new DiplomatTrait(),
                new RegionRacistTrait(),
                new DefensiveTrait(),
                new FriendlyTrait(),
            };

            //Dependent traits
            AddConditionalTraits(player, trait_list);
            
            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }

        // Add traits that require conditionals to trait_list
        public static void AddConditionalTraits(Player player, List<ForeignTraitBase> trait_list){
            if(player.GetRandomKnownPlayerNullable() != null) trait_list.Add(new Racist(player));
        }

        public  bool isSameGovernmentType(Player known_player, Player player) =>
            known_player.government.government_type == player.government.government_type;
        

        public bool isSameRegionType(Player known_player, Player player) =>
            known_player.GetCapital().region_type == player.GetCapital().region_type;
        
    }
}