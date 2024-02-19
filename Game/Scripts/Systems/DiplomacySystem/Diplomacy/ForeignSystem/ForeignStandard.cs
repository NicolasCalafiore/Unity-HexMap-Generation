using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;


namespace Diplomacy
{
    public class ForeignStandard : ForeignStrategy
    {
        public ForeignStandard(){
        }

        // public override void GenerateStartingValues(Player player){
        //     Foreign foreign = player.GetGovernment().GetForeign(0);

        //     foreach(Trait i in foreign.traits){
        //         this.base_value += i.GetTraitAlgorithmValue();
        //         this.government_type_value += i.GetGovernmentTypeValue();
        //         this.region_type_value += i.GetRegionTypeValue();
        //     }
        // }

        public override float GenerateStartingRelationship(Player known_player, Player player){

            float current_relationship_level = 0;

            foreach(ForeignTraitBase i in player.GetGovernment().GetForeign(0).traits){
                current_relationship_level += i.GetTraitAlgorithmValue(known_player, player);
            }

            return current_relationship_level;

        }
        public override List<string> CalculationValues(Player known_player, Player player){
            float current_relationship_level = 0;
            List<string> values = new List<string>();

            foreach(ForeignTraitBase i in player.GetGovernment().GetForeign(0).traits){
                values.Add("Trait Name: " + i.GetName());
                values.Add("Trait Value: " + i.GetTraitAlgorithmValue(known_player, player));
                values.Add("Trait Activated: " + i.isActivated(known_player, player));
                current_relationship_level += i.GetTraitAlgorithmValue(known_player, player);
            }

            values.Add("Total Relationship Level: " + current_relationship_level);

            return values;

        }
    }
}
