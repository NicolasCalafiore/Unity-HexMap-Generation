using UnityEngine;
using Terrain;
using System;
using Players;
using System.Collections.Generic;
using Character;
using Cities;

namespace Terrain {
    public static class HexManager{
        public static Dictionary<Vector2, HexTile> col_row_to_hex = new Dictionary<Vector2, HexTile>();
        public static List<HexTile> hex_list = new List<HexTile>();   // All HexTile objects
        private static DecoratorHandler hex_decorator = new DecoratorHandler();


        // Generates a list of hex tiles based on the map size
        // Called from MapGeneration
        // Generates a hex tile for each column and row
        public static void GenerateHexList(){
            List<HexTile> hex_list = new List<HexTile>();

            for(int column = 0; column < MapManager.GetMapSize().x; column++)
                for(int row = 0; row < MapManager.GetMapSize().y; row++){
                    
                    HexTile hex = GenerateHex(
                        MapManager.terrain_map_handler.GetElevationMap()[column][row], 
                        MapManager.city_map_handler.structure_map[column][row], 
                        MapManager.terrain_map_handler.GetFeaturesMap()[column][row], 
                        MapManager.terrain_map_handler.GetWaterMap()[column][row],
                        MapManager.terrain_map_handler.GetRegionsMap()[column][row], 
                        MapManager.terrain_map_handler.GetResourceMap()[column][row], 
                        MapManager.terrain_map_handler.GetContinentsMap()[column][row],
                        column, row, MapManager.terrain_map_handler.GetRegionIdMap()[column][row]);

                    hex_list.Add(hex);

                }
                
            

            HexManager.hex_list = hex_list;
        }

        // Generates a hex tile based on the parameters
        // One Hex At a Time
        // Called from above loop
        private static HexTile GenerateHex(float elevation_type, float structure_type, float feature_type, float land_type, float region_type, float resource_type, /* float owner_id, */ float continent, float col, float row, float region_id){ 
            HexTile hex = new((int) col, (int) row);
            col_row_to_hex.Add(new Vector2(col, row), hex);

            ElevationEnums.HexElevation elevation_type_enum = LandEnums.GetLandType(land_type) == LandEnums.LandType.Water ? ElevationEnums.HexElevation.Flatland : ElevationEnums.GetElevationType(elevation_type);
            
            hex.elevation_type = elevation_type_enum;
            hex.structure_type = StructureEnums.GetStructureType(structure_type);
            hex.feature_type = FeaturesEnums.GetNaturalFeatureType(feature_type);
            hex.land_type = LandEnums.GetLandType(land_type);
            hex.region_type = RegionsEnums.GetRegionType(region_type);
            hex.resource_type = ResourceEnums.GetResourceType(resource_type);
            hex.continent_id = continent;
            hex.culture_id = region_id;
            return hex;
        }


        public static void AddHexTileToCityTerritory(City city) 
        {
            List<HexTile> hex_list_to_add = HexTileUtils.CircularRetrieval((int) city.col_row.x, (int) city.col_row.y);
                
            foreach(HexTile hex in hex_list_to_add){
                city.GetHexTerritoryList().Add(hex);
                hex.owner_city = city;
            }
        }

        // Adds hexes to the player's territory
        // If the hex is not owned by a player, it is added to the player's territory
        // territory_hex_list is the list of hexes to add to the player's territory
        public static void AddHexTileToPlayerTerritory(List<HexTile> territory_hex_list, Player player)   
        {
            foreach(HexTile hex in territory_hex_list)
                if(hex.owner_player == null)
                    hex.owner_player = player;
        }

        internal static void SetHexDecorators() => hex_decorator.SetHexDecorators(hex_list);
        public static List<HexTile> GetHexList => hex_list;
    }
}