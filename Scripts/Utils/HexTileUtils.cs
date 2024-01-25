using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Terrain
{
    public static class HexTileUtils
    {

        public static List<HexTile> GenerateHexList(Vector2 map_size, MapGeneration map_generation, CityManager city_manager, TerritoryManager territory_manager, HexManager hex_factory){
            List<HexTile> hex_list = new List<HexTile>();
            for(int column = 0; column < map_size.x; column++){
                for(int row = 0; row < map_size.y; row++){
                    HexTile hex = hex_factory.GenerateHex(
                        map_generation.terrain_map_handler.elevation_map[column][row], 
                        city_manager.structure_map[column][row], 
                        map_generation.terrain_map_handler.features_map[column][row], 
                        map_generation.terrain_map_handler.water_map[column][row],
                        map_generation.terrain_map_handler.regions_map[column][row], 
                        map_generation.terrain_map_handler.resource_map[column][row], 
                        // territory_manager.territory_map[column][row], 
                        column, row);

                    hex_list.Add(hex);
                }
                
            }

            return hex_list;
            
        }


        public static List<HexTile> CircularRetrieval(int i, int j, Vector2 map_size, int iterations = 1)
        {
            List<HexTile> hex_list = new List<HexTile>();

            // Check immediate neighbors
            if (j - 1 >= 0) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i, j - 1)]);
            if (j + 1 < map_size.y) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i, j + 1)]);
            if (i - 1 >= 0) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i - 1, j)]);
            if (i + 1 < map_size.x) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i + 1, j)]);
            if (i + 1 < map_size.x && j - 1 >= 0) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i + 1, j - 1)]);
            if (i - 1 >= 0 && j + 1 < map_size.y) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i - 1, j + 1)]);

            return hex_list;
        }

    }
}