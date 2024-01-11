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

    public class TerritoryManager
    {
        public List<List<float>> territory_map = new List<List<float>>();
        public TerritoryManager()
        {

        }


        public void GenerateCapitalTerritory(List<List<float>> capital_map, List<Player> player_list)
        {
            List<List<float>> territory_map = TerrainUtils.GenerateMap(new Vector2(capital_map.Count, capital_map[0].Count), -1);

            for(int i = 0; i < capital_map.Count; i++)
            {
                for(int j = 0; j < capital_map[i].Count; j++)
                {
                    if(capital_map[i][j] == (int) EnumHandler.StructureType.Capital)
                    {
                        foreach(Player player in player_list){
                            try{
                                if(player.GetCityByIndex(0).GetColRow() == new Vector2(i,j)){
                                    TerrainUtils.CircularSpawn(i, j, territory_map, player.id);
                                }
                            }catch(Exception e){
                                Debug.Log(e);
                            }
                        }
                        
                    }
                }
            }

            this.territory_map = territory_map;
        }

        public void AddHexTileTerritoryToCities(List<HexTile> hex_list, City city){
            
        }

        public void CalculateCityNourishment(City city){




        }
















    }
}