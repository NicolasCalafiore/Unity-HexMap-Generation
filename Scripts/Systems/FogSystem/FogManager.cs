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
        public FogManager(Vector2 map_size){

        }

        public void InitializePlayerFogOfWar(List<Player> player_list, Vector2 map_size){
            foreach(Player player in player_list){
                List<List<float>> player_fog_of_war_map = TerrainUtils.GenerateMap(map_size, 0);

                List<City> city_list = player.GetAllCities();
                foreach(City city in city_list){
                    Vector2 city_col_row = city.GetColRow();
                    player_fog_of_war_map[(int) city_col_row.x][(int) city_col_row.y] = 1;
                    TerrainUtils.CircularSpawn((int) city_col_row.x, (int) city_col_row.y, player_fog_of_war_map, 1, 5);
                }
                
                player.SetFogOfWarMap(player_fog_of_war_map);
            }

        }

        public void DestoryAllFog(TerrainHandler terrain_handler){
            terrain_handler.DestroyFog();
        }







    }
}