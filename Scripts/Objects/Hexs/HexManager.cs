using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using UnityEngine;


public class HexManager{

    public static Dictionary<Vector2, HexTile> col_row_to_hex = new Dictionary<Vector2, HexTile>();

    public HexManager(){

    }

    public List<HexTile> GenerateHexList(Vector2 map_size, MapGeneration map_generation, CityManager city_manager, TerritoryManager territory_manager, HexManager hex_factory){
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

    public HexTile GenerateHex(float elevation_type, float structure_type, float feature_type, float land_type, float region_type, float resource_type, /* float owner_id, */ float col, float row){ 
        HexTile hex = new((int) col, (int) row);
        col_row_to_hex.Add(new Vector2(col, row), hex);
        EnumHandler.HexElevation elevation_type_enum = EnumHandler.GetLandType(land_type) == EnumHandler.LandType.Water ? EnumHandler.HexElevation.Flatland : EnumHandler.GetElevationType(elevation_type);
        hex.SetElevation(elevation_type_enum)
            .SetStructureType(EnumHandler.GetStructureType(structure_type))
            .SetFeatureType(EnumHandler.GetNaturalFeatureType(feature_type))
            .SetLandType(EnumHandler.GetLandType(land_type))
            .SetRegionType(EnumHandler.GetRegionType(region_type))
            .SetResourceType(EnumHandler.GetResourceType(resource_type));

        return hex;
    }


    public void AddHexTileToCityTerritory(City city, Vector2 map_size) 
    {
        Vector2 city_col_row = city.GetColRow();

        List<HexTile> hex_list_to_add = HexTileUtils.CircularRetrieval((int) city_col_row.x, (int) city_col_row.y, map_size);
            
        foreach(HexTile hex in hex_list_to_add){
            city.hex_territory_list.Add(hex);
            hex.SetOwnerCity(city);
        }
    }

    public static void AddHexTileToPlayerTerritory(List<HexTile> hex_list, Player player)   
    {
        foreach(HexTile hex in hex_list){
            if(hex.owner_player == null){
                hex.SetOwnerPlayer(player);
            }
        }
    }




}



