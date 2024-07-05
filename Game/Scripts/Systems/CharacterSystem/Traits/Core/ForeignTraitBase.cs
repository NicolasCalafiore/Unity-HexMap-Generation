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
using AI;

namespace Character {
    public abstract class ForeignTraitBase : TraitBase {
        protected HexRegion region_target;
        public Player player_target;
        protected int integer_storage;
        public bool isBlankEffect = false;
        public abstract bool isActivated(Player player, Player other_player);
        public ForeignTraitBase(string description, int value) : base(description, value){}
        public abstract float GetTraitValue(Player player, Player known_player);



        public static ForeignTraitBase GetRandomForeignTrait(Player player){

            List<ForeignTraitBase> trait_list = new List<ForeignTraitBase>();

            AddNonConditionalTraits(trait_list);
            AddConditionalTraits(player, trait_list);
            
            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }

        // Add traits that require conditionals to trait_list
        public static void AddNonConditionalTraits(List<ForeignTraitBase> trait_list){
            trait_list.Add(new GovernanceTrait());
            trait_list.Add(new RegionalTrait());
            trait_list.Add(new RegionalConflictTrait());
            trait_list.Add(new Rude());
            trait_list.Add(new DiplomatTrait());
            trait_list.Add(new RegionRacistTrait());
            trait_list.Add(new DefensiveTrait());
            trait_list.Add(new NeighborlyTrait());
            trait_list.Add(new WealthAdmirer());
            trait_list.Add(new ContinentalClaimer());
            trait_list.Add(new ContinentalUniter());
            trait_list.Add(new ScientificTrait());
            trait_list.Add(new StabilityTrait());
            trait_list.Add(new CultureBias());
            trait_list.Add(new CultureClaimer());
            trait_list.Add(new CultureUniter());
        }

        public static void AddConditionalTraits(Player player, List<ForeignTraitBase> trait_list){
            if(!player.isIsolated()) trait_list.Add(new Foe(player));
            if(player.wealth > ((EconomyPriority) player.GetPriority("Economy")).GetCriticalPoint()) trait_list.Add(new PovertyDiscriminator());
        }

        public  bool isSameGovernmentType(Player known_player, Player player) =>
            known_player.government.government_type == player.government.government_type;
        
        public bool isSameRegionType(Player known_player, Player player) =>
            known_player.GetCapital().region_type == player.GetCapital().region_type;


        
    }
}