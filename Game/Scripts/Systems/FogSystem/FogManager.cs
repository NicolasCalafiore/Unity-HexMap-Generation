using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Strategy.Assets.Scripts.Objects;
using static Terrain.FogEnums;




namespace Terrain {

    public static class FogManager
    {
        private static int fog_radius = 7;
        public static void InitializePlayerFogOfWar(){

            foreach(Player player in Player.GetPlayerList()){

                List<List<float>> player_fog_of_war_map = TerrainUtils.GenerateMap((float) FogType.Undiscovered);
                List<City> city_list = player.GetCities();

                foreach(City city in city_list){
                    int x = (int) city.GetColRow().x;
                    int y = (int) city.GetColRow().y;

                    player_fog_of_war_map[x][y] = (float) FogType.Discovered;
                    TerrainUtils.CircularSpawn(x, y, player_fog_of_war_map, (float) FogType.Discovered, fog_radius);
                }
                
                player.SetFogOfWarMap(player_fog_of_war_map);
            }

        }

        public static void ShowFogOfWar(){
            Player player = Player.GetPlayerView();
            List<List<float>> fog_of_war_map = player.GetFogOfWarMap();

            for(int i = 0; i < fog_of_war_map.Count; i++){
                for(int j = 0; j < fog_of_war_map[i].Count; j++){

                    if(fog_of_war_map[i][j] == (float) FogType.Undiscovered) DespawnHexTile(i, j);
                    else SpawnHexTile(i, j);
                    
                }
            }
        }

        public static void SpawnHexTile(int i, int j)
        {
            Vector2 coordinates = new Vector2(i, j);

            GameObject hex_go = TerrainManager.hex_go_list[(int) coordinates.x * (int) MapManager.GetMapSize().y + (int) coordinates.y];
            hex_go.SetActive(true);
        }

        public static void DespawnHexTile(int i, int j){
            Vector2 coordinates = new Vector2(i, j);

            GameObject hex_go = TerrainManager.hex_go_list[(int) coordinates.x * (int) MapManager.GetMapSize().y + (int) coordinates.y];
            hex_go.SetActive(false);
        }

        public static void DestroyFog()
        {
            foreach(GameObject hex_go in TerrainManager.hex_go_list){
                hex_go.SetActive(true);
            }
        }




    }
}