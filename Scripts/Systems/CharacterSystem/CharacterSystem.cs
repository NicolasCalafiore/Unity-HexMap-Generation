using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using Cabinet;



namespace Character {

    public class CharacterManager
    {
        CharacterNameStrategy character_names_strategy;
        private int gender_male_chance = 75;

        public CharacterManager(){


        }

        public void SetCharacterGenerationStrategy(int name_mode){
            switch(name_mode){
                case 0:
                    character_names_strategy = new NameByRegion();
                    break;
                default:
                    character_names_strategy = new NameByRegion();
                    break;
            }
        }


        public void GenerateGovernmentsLeaders(List<List<float>> regions_map, List<Player> player_list){
            foreach(Player i in player_list){
                EnumHandler.CharacterGender gender = UnityEngine.Random.Range(0, 100) < gender_male_chance ? EnumHandler.CharacterGender.Male : EnumHandler.CharacterGender.Female;
                List<string> names = character_names_strategy.GenerateNames(i.GetCityByIndex(0).GetColRow(), regions_map, gender);
                Leader leader = new Leader(names, gender);
                
                i.GetGovernment().SetLeader(leader);
            }
        }

    }
}