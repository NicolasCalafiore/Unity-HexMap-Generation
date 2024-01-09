using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Terrain
{
    public static class HexTileUtils
    {

        public static List<HexTile> GenerateHexList(Vector2 map_size, MapGeneration map_generation, CityManager city_manager, TerritoryManager territory_manager, HexFactory hex_factory){
            List<HexTile> hex_list = new List<HexTile>();
            for(int column = 0; column < map_size.x; column++){
                for(int row = 0; row < map_size.y; row++){
                    HexTile hex = hex_factory.GenerateHex(
                        map_generation.elevation_map[column][row], 
                        city_manager.structure_map[column][row], 
                        map_generation.features_map[column][row], 
                        map_generation.water_map[column][row],
                        map_generation.regions_map[column][row], 
                        map_generation.resource_map[column][row], 
                        territory_manager.territory_map[column][row], 
                        column, row);

                    hex_list.Add(hex);
                }
                
            }

            return hex_list;
            
        }

    }
}