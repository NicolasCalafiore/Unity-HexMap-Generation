using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Terrain
{
    public static class HexTileUtils
    {
        public static List<HexTile> CreateHexObjects(Vector2 map_size){
            List<HexTile> hex_list = new List<HexTile>();

            // Create Hex objects for each column and row in the map
            for(int column = 0; column < map_size.x; column++)
            {
                for(int row = 0; row < map_size.y; row++)
                {
                    HexTile hex = new HexTile(column, row);
                    hex_list.Add(hex);
                }
            }

            return hex_list;
        }
        public static void SetHexFeatures(List<List<float>> features_map, List<HexTile> hex_list)
        {
            for(int i = 0; i < hex_list.Count; i++){
                Vector2 coordinates = hex_list[i].GetColRow();
                hex_list[i].SetFeatureType(EnumHandler.GetFeatureType(features_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public static void SetHexLand(List<List<float>> land_map, List<HexTile> hex_list)
        {
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetLandType(EnumHandler.GetLandType(land_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public static void SetHexElevation(List<List<float>> elevation_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetElevation(EnumHandler.GetElevationType(elevation_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }
        public static void SetHexRegion(List<List<float>> regions_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                if(hex.GetRegionType() == EnumHandler.HexRegion.River){
                    //hex.SetRegionType(EnumHandler.HexRegion.River);
                }
                hex.SetRegionType(EnumHandler.GetRegionType(regions_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

    }
}