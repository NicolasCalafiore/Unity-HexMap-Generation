using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Cities;
using Terrain;
using Cabinet;
using static Character.CharacterEnums;
using System;



namespace Character {

    public static class CharacterManager
    {

        // Generates all characters for all players
        public static void GenerateGovernmentsCharacters(){

            List<List<float>> regions_map = MapManager.terrain_map_handler.GetRegionsMap();
            List<Player> player_list = PlayerManager.player_list;

            foreach(Player player in player_list)
            {
                City city = player.GetCapital();

                foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
                {
                    AbstractCharacter character = CharacterFactory.CreateCharacterNullable(role, regions_map, city, player);
                    if (character != null)
                    {
                        player.government.AddCharacter(character);
                        character.InitializeCharacteristics();
                    }
                }
            }
        }

    }
}