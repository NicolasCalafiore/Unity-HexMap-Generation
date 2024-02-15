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

    public static class CharacterManager
    {
        private static CharacterNameStrategy character_names_strategy = new NameByRegion();
        private static int gender_male_chance = 75;

        public static void GenerateGovernmentsCharacters(){
            List<List<float>> regions_map = MapManager.terrain_map_handler.GetRegionsMap();
            List<Player> player_list = Player.GetPlayerList();
            foreach(Player i in player_list){

                CharacterEnums.CharacterGender gender = UnityEngine.Random.Range(0, 100) < gender_male_chance ? CharacterEnums.CharacterGender.Male : CharacterEnums.CharacterGender.Female;
                City city = i.GetCityByIndex(0);
                
                List<string> names = character_names_strategy.GenerateNames(city.GetColRow(), regions_map, gender);
                Leader leader = new Leader(names, gender);

                i.GetGovernment().SetLeader(leader);
                leader.InitializeCharacteristics();

                gender = UnityEngine.Random.Range(0, 100) < gender_male_chance ? CharacterEnums.CharacterGender.Male : CharacterEnums.CharacterGender.Female;
                names = character_names_strategy.GenerateNames(city.GetColRow(), regions_map, gender);
                Domestic domestic_advisor = new Domestic(names, gender);

                i.GetGovernment().AddDomestic(domestic_advisor);
                domestic_advisor.InitializeCharacteristics();

                gender = UnityEngine.Random.Range(0, 100) < gender_male_chance ? CharacterEnums.CharacterGender.Male : CharacterEnums.CharacterGender.Female;
                names = character_names_strategy.GenerateNames(city.GetColRow(), regions_map, gender);
                Foreign foreign_advisor = new Foreign(names, gender);
                foreign_advisor.SetForeignStrategy(1);
                
                i.GetGovernment().AddForeign(foreign_advisor);
                foreign_advisor.InitializeCharacteristics();
            }
        }

    }
}