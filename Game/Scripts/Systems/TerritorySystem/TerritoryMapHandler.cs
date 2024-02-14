using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;




namespace Terrain {

    public class TerritoryMapHandler
    {
        public List<List<float>> territory_map = new List<List<float>>();

        public void GenerateCapitalTerritory(List<List<float>> capital_map, List<Player> player_list, Vector2 map_size)
        {
            List<List<float>> territory_map = TerrainUtils.GenerateMap(new Vector2(capital_map.Count, capital_map[0].Count), -1);

            for (int i = 0; i < capital_map.Count; i++)
            {
                for (int j = 0; j < capital_map[i].Count; j++)
                {
                    if (capital_map[i][j] != (int) EnumHandler.StructureType.Capital) continue;

                    AssignTerritoryToPlayer(i, j, player_list, territory_map, map_size);
                }
            }

            this.territory_map = territory_map;
        }


        private void AssignTerritoryToPlayer(int i, int j, List<Player> player_list, List<List<float>> territory_map, Vector2 map_size)
        {
            Vector2 capitalCoordinate = new Vector2(i, j);

            foreach (Player player in player_list)
            {
                if (player.GetCityByIndex(0).GetColRow() != capitalCoordinate) continue;

                TerrainUtils.CircularSpawn(i, j, territory_map, player.id);
                List<HexTile> hex_list = HexTileUtils.CircularRetrieval(i, j, map_size);

                HexManager.AddHexTileToPlayerTerritory(hex_list, player);
                
            }
        }



    }
}