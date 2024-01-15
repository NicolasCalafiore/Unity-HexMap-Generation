using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using UnityEngine;


public class HexFactory{

    public static Dictionary<Vector2, HexTile> col_row_to_hex = new Dictionary<Vector2, HexTile>();

    public HexFactory(){

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

        // if(owner_id == -1){
        //     hex.SetOwnerPlayer(null);
        // }
        // else{
        //     hex.SetOwnerPlayer(GameManager.player_id_to_player[owner_id]);
        // }

        return hex;

    }


    public void AddHexTileToCityTerritory(List<HexTile> hex_list, City city, Vector2 map_size) // (TO DO: REFACTOR)
    {
        Vector2 city_col_row = city.GetColRow();

        List<HexTile> hex_list_to_add = HexTileUtils.CircularRetrieval((int) city_col_row.x, (int) city_col_row.y, map_size);
            
        foreach(HexTile hex in hex_list_to_add){
            city.hex_territory_list.Add(hex);
            hex.owner_city = city;
        }
    }

    public static void AddHexTileToPlayerTerritory(List<HexTile> hex_list, Player player)   // City object noT available (TO DO: REFACTOR)
    {
        foreach(HexTile hex in hex_list){
            if(hex.owner_player == null){
                hex.SetOwnerPlayer(player);
            }
        }
    }


}



