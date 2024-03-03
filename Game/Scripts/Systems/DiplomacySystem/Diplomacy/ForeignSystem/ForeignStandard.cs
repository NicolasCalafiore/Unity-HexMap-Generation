using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;


namespace Diplomacy
{
    public class ForeignStandard : ForeignStrategy
    {
        private float leader_multiplier = 1.5f;
        public ForeignStandard(){
        }

        public override float GenerateStartingRelationship(Player known_player, Player player){

            float current_relationship_level = 0;

            foreach(ForeignTraitBase i in player.GetGovernment().GetForeign(0).traits){
                current_relationship_level += i.GetTraitAlgorithmValue(known_player, player);
            }

            foreach(TraitBase i in player.GetGovernment().GetLeader().traits){
                if(i is ForeignTraitBase){
                    current_relationship_level += ((ForeignTraitBase)i).GetTraitAlgorithmValue(known_player, player) * leader_multiplier;
                }
            }

            List<ICharacter> known_characters = known_player.GetAllCharacters();
            List<ICharacter> player_characters = player.GetAllCharacters();

            int similiar_views_counter = 0;
            int disimiliar_views_counter = 0;

            foreach(ICharacter i in known_characters){
                foreach(TraitBase iTrait in i.traits){
                    foreach(ICharacter j in player_characters){
                        foreach(TraitBase jTrait in j.traits){
                            if(jTrait.name == iTrait.name){
                                similiar_views_counter += 1;
                            }
                            else{
                                disimiliar_views_counter += 1;
                            }
                        }

                    }
                }
            }

            current_relationship_level += similiar_views_counter;
            current_relationship_level -= disimiliar_views_counter * .1f;

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
            values.Add("");

            foreach(TraitBase i in player.GetGovernment().GetLeader().traits){
                if(i is ForeignTraitBase){
                    values.Add("Leader Trait Name: " + i.GetName());
                    values.Add("Leader Trait Value: " + ((ForeignTraitBase)i).GetTraitAlgorithmValue(known_player, player) * leader_multiplier);
                    values.Add("Leader Trait Activated: " + ((ForeignTraitBase)i).isActivated(known_player, player));
                    current_relationship_level += ((ForeignTraitBase)i).GetTraitAlgorithmValue(known_player, player) * leader_multiplier;
                }
            }
            values.Add("");

            List<ICharacter> known_characters = known_player.GetAllCharacters();
            List<ICharacter> player_characters = player.GetAllCharacters();

            int similiar_views_counter = 0;
            int disimiliar_views_counter = 0;
            List<TraitBase> similiar_traits = new List<TraitBase>();
            foreach(ICharacter i in known_characters){
                foreach(TraitBase iTrait in i.traits){
                    foreach(ICharacter j in player_characters){
                        foreach(TraitBase jTrait in j.traits){
                            if(jTrait.name == iTrait.name){
                                similiar_views_counter += 1;
                                similiar_traits.Add(jTrait);
                            }
                            else{
                                disimiliar_views_counter += 1;
                            }
                        }

                    }
                }
            }

            current_relationship_level += similiar_views_counter;
            current_relationship_level -= disimiliar_views_counter * .1f;
            values.Add("similiar_views_counter: " + similiar_views_counter);
            values.Add("similiar_views value: " + similiar_views_counter);
            values.Add("disimiliar_views_counter: " + disimiliar_views_counter);
            values.Add("disimiliar_views value: " + disimiliar_views_counter * .1f);
            values.Add("");

            foreach(TraitBase i in similiar_traits){
                values.Add("Similiar Trait Name: " + i.GetName());
            }
            values.Add("");

            values.Add("Total Relationship Level: " + current_relationship_level);

            return values;

        }
    }
}
