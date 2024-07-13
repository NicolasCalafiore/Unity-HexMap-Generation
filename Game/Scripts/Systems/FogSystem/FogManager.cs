using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Cities;
using static Terrain.FogEnums;
using System.Linq;
using Graphics;




namespace Terrain {

    public static class FogManager
    {
        private const int fog_radius = 7;

        // Initialize the fog of war for all players
        // This method is called by the MapManager
        public static void InitializePlayerFogOfWar(){
            PlayerManager.player_list.ForEach(player =>
            {
                List<List<float>> player_fog_of_war_map = MapUtils.GenerateMap((float)FogType.Undiscovered);

                player.GetCities().ForEach(city =>
                {
                    int x = (int)city.col_row.x;
                    int y = (int)city.col_row.y;

                    player_fog_of_war_map[x][y] = (float)FogType.Discovered;
                    MapUtils.CircularSpawn(x, y, player_fog_of_war_map, (float)FogType.Discovered, fog_radius);
                });

                player.SetFogOfWarMap(player_fog_of_war_map);
            });
        }
        // Update the fog of war for a player
        // This method is called by the Player class
        public static void ShowFogOfWar(){
            PlayerManager.player_view.GetFogOfWarMap()
                .SelectMany((row, i) => row.Select((value, j) => new { i, j, value }))
                .ToList()
                .ForEach(cell => 
                {
                    if (cell.value == (float)FogType.Undiscovered) 
                        DespawnHexTile(cell.i, cell.j);
                    else 
                        SpawnHexTile(cell.i, cell.j);
                });
        }

        public static void SpawnHexTile(int i, int j)
        {
            GameObject hex_go = HexGraphicManager.GetHexGoByColRow(new Vector2(i, j));
            hex_go.SetActive(true);
        }

        public static void DespawnHexTile(int i, int j){
            GameObject hex_go = HexGraphicManager.GetHexGoByColRow(new Vector2(i, j));
            hex_go.SetActive(false);
        }

        public static void ClearFog()
        {
            foreach(GameObject hex_go in HexGraphicManager.hex_go_list)
                hex_go.SetActive(true);
        }

        // Get the list of players that are currently visible
        public static List<Player> GetVisiblePlayers(){
            return PlayerManager.player_list
                .Where(player => player.GetCities().Any(city => CityManager.city_to_city_go[city].activeSelf))
                .ToList();
        }
    }
}