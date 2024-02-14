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




namespace Terrain {

    public class FogManager
    {
        public FogManager(){

        }

        public void InitializePlayerFogOfWar(MapManager map_generation){
            List<Player> player_list = Player.GetPlayerList();
            foreach(Player player in player_list){

                List<List<float>> player_fog_of_war_map = TerrainUtils.GenerateMap(map_generation.GetMapSize(), 0);

                List<City> city_list = player.cities;
                foreach(City city in city_list){
                    Vector2 city_col_row = city.GetColRow();
                    player_fog_of_war_map[(int) city_col_row.x][(int) city_col_row.y] = 1;
                    TerrainUtils.CircularSpawn((int) city_col_row.x, (int) city_col_row.y, player_fog_of_war_map, 1, 10);
                }
                
                player.SetFogOfWarMap(player_fog_of_war_map);

            }

        }

        public void DestroyFog()
        {
            foreach(GameObject hex_go in TerrainManager.hex_go_list){
                hex_go.SetActive(true);
            }
        }

        public void ShowFogOfWar(MapManager map_generation){
            Player player = Player.GetPlayerView();
            List<List<float>> fog_of_war_map = player.fog_of_war_map;

            for(int i = 0; i < fog_of_war_map.Count; i++){
                for(int j = 0; j < fog_of_war_map[i].Count; j++){

                    if(fog_of_war_map[i][j] == 0) DespawnHexTile(new Vector2(i,j), map_generation.GetMapSize());

                    else SpawnHexTile(new Vector2(i,j), map_generation.GetMapSize());
                    
                }
            }
        }

        public void SpawnHexTile(Vector2 vector2, Vector2 map_size)
        {
            GameObject hex_go = TerrainManager.hex_go_list[(int) vector2.x * (int) map_size.y + (int) vector2.y];
            hex_go.SetActive(true);
        }

        public void DespawnHexTile(Vector2 coordinates, Vector2 map_size){
            GameObject hex_go = TerrainManager.hex_go_list[(int) coordinates.x * (int) map_size.y + (int) coordinates.y];
            hex_go.SetActive(false);
        }






    }
}