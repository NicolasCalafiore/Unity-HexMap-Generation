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
using static Character.CharacterEnums;



namespace Character {

    public static class CharacterManager
    {

        public static void GenerateGovernmentsCharacters(){
            List<List<float>> regions_map = MapManager.terrain_map_handler.GetRegionsMap();
            List<Player> player_list = Player.GetPlayerList();
            foreach(Player i in player_list){
                City city = i.GetCityByIndex(0);

                Leader leader = (Leader) CharacterFactory.CreateCharacter(RoleType.Leader, regions_map, city);
                i.GetGovernment().AddCharacter(leader);
                leader.InitializeCharacteristics();


                Domestic domestic_advisor = (Domestic) CharacterFactory.CreateCharacter(RoleType.Domestic, regions_map, city);
                i.GetGovernment().AddCharacter(domestic_advisor);
                domestic_advisor.InitializeCharacteristics();

                Foreign foreign_advisor = (Foreign) CharacterFactory.CreateCharacter(RoleType.Foreign, regions_map, city);
                foreign_advisor.SetForeignStrategy(1);
                i.GetGovernment().AddCharacter(foreign_advisor);
                foreign_advisor.InitializeCharacteristics();

            }
        }



    }
}