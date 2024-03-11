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
        protected Player player_target;
        protected int primary_int;

        public abstract float GetTraitValue(Player player, Player other_player);
        public abstract bool isActivated(Player player, Player other_player);

        public ForeignTraitBase(string name, string description, int value) : base(name, description, value){}

        public static ForeignTraitBase GetRandomForeignTrait(Player player){

            //Non-Dependent traits
            List<ForeignTraitBase> trait_list = new List<ForeignTraitBase>(){
                new IdeologicalTrait(),
                new RegionalConnection(),
                new HomelandTrait(),
                new PeacePromoter(),
                new WarMonger(),
                new Diplomat(),
                new RacistRegion(),
                new HomeFront(),
                new Neighborly(),
            };

            //Dependent traits
            AddConditionalTraits(player, trait_list);
            
            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }

        // Add traits that require conditionals to trait_list
        public static void AddConditionalTraits(Player player, List<ForeignTraitBase> trait_list){
            if(player.GetRandomKnownPlayerNullable() != null) trait_list.Add(new RacistPlayer(player));
        }

        public  bool isSameGovernmentType(Player known_player, Player player) =>
            known_player.government.government_type == player.government.government_type;
        

        public bool isSameRegionType(Player known_player, Player player) =>
            known_player.GetCityByIndex(0).region_type == player.GetCityByIndex(0).region_type;
        
    }
}