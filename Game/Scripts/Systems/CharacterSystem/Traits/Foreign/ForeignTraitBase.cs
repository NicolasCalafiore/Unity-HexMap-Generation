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
    public abstract class ForeignTraitBase : TraitBase {
        protected int government_type_value = 0;
        protected int region_type_value = 0;
        protected int base_value = 0;

        public abstract float GetTraitAlgorithmValue(Player player, Player other_player);
        public abstract bool isActivated(Player player, Player other_player);
        public static ForeignTraitBase GetRandomForeignTrait(){
            List<ForeignTraitBase> trait_list = new List<ForeignTraitBase>();
            trait_list.Add(new IdeologicalTrait());
            trait_list.Add(new RegionalConnection());
            trait_list.Add(new HomelandTrait());
            trait_list.Add(new PeacePromoter());
            trait_list.Add(new WarMonger());
            trait_list.Add(new Diplomat());


            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }

        public bool isSameGovernmentType(Player known_player, Player player){
            return known_player.GetGovernment().GetGovernmentType() == player.GetGovernment().GetGovernmentType();
        }

        public bool isSameRegionType(Player known_player, Player player){
            return known_player.GetCityByIndex(0).GetRegionType() == player.GetCityByIndex(0).GetRegionType();
        }
    }
}