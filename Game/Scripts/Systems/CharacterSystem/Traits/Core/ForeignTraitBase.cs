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
        protected HexRegion region_target;
        public Player player_target;
        protected int integer_storage;
        public abstract bool isActivated(Player player, Player other_player);
        public ForeignTraitBase(string description, int value) : base(description, value){}
        public abstract float GetTraitValue(Player player, Player known_player);



        public static ForeignTraitBase GetRandomForeignTrait(Player player){

            //Non-Dependent traits
            List<ForeignTraitBase> trait_list = new List<ForeignTraitBase>(){
                new GovernanceTrait(),
                new RegionalTrait(),
                new CulturalConflictTrait(),
                new Rude(),
                new DiplomatTrait(),
                new RegionRacistTrait(),
                new DefensiveTrait(),
                new NeighborlyTrait(),
                new WealthAdmirer(),
                new PovertyDiscriminator(),
                new ContinentalClaimer(),
                new ContinentalUniter(),
                new ScientificTrait(),
                new StabilityTrait(),
            };

            //Dependent traits
            AddConditionalTraits(player, trait_list);
            
            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }

        // Add traits that require conditionals to trait_list
        public static void AddConditionalTraits(Player player, List<ForeignTraitBase> trait_list){
            if(player.GetRandomKnownPlayerNullable() != null) trait_list.Add(new Foe(player));
        }

        public  bool isSameGovernmentType(Player known_player, Player player) =>
            known_player.government.government_type == player.government.government_type;
        

        public bool isSameRegionType(Player known_player, Player player) =>
            known_player.GetCapital().region_type == player.GetCapital().region_type;


        
    }
}