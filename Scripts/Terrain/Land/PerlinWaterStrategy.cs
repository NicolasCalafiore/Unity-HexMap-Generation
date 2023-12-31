using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Water
{
    public class PerlinWaterStrategy : WaterStrategy
    {
        private float river_scale = 4f;
        private float ocean_scale = 5f;
        private Vector2 river_max_min = new Vector2( .5f, .46f);
        private Vector2 ocean_max_min = new Vector2( .7f, .45f);

        public override List<List<float>> GenerateWaterMap(Vector2 map_size, EnumHandler.HexRegion region_type, List<HexTile> hex_list)
        {
            List<List<float>> map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.NormalizePerlinMap(map);
        
            if(region_type == EnumHandler.HexRegion.River){
                TerrainUtils.GeneratePerlinNoiseMap(map, map_size, river_scale);
                FilterPerlinMap(map, map_size, river_max_min, region_type);
            }
            else if(region_type == EnumHandler.HexRegion.Ocean){
                TerrainUtils.GeneratePerlinNoiseMap(map, map_size, ocean_scale);
                FilterPerlinMap(map, map_size, ocean_max_min, region_type);
            }

            SetLand(map, region_type);
            return map;
        }
        public static void SetLand(List<List<float>> map, EnumHandler.HexRegion region_type)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if(map[i][j] != (int) region_type){
                        map[i][j] = (int) EnumHandler.LandType.Land;}
                    else{
                        map[i][j] = (int) EnumHandler.LandType.Water;
                    }
                }
            }
        }
        
        public static void FilterPerlinMap(List<List<float>> map, Vector2 map_size, Vector2 max_min, EnumHandler.HexRegion region_type){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    if(map[i][j] <= max_min.x && map[i][j] >= max_min.y){
                        map[i][j] = (int) region_type;         //Ocean 0, River 1
                    }
                }
            }   
        }

    }
}
