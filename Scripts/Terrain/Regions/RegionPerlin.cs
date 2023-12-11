using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;


namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    public class RegionPerlin : RegionStrategy
    {
        float scale = 6.5f;
        public override List<List<float>> GenerateRegionsMap(Vector2 map_size, GameObject perlin_map_object)
        {
            List<List<float>> regions_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(regions_map, map_size, scale);
            FilterRegionMap(regions_map, map_size);
            
            return regions_map;
        }

        public static void FilterRegionMap(List<List<float>> map, Vector2 map_size){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {

                    if(map[i][j] < .325f) map[i][j] = (int) TerrainUtils.HexRegion.Desert;         //DESERT
                    else if(map[i][j] < .4f) map[i][j] = (int) TerrainUtils.HexRegion.Savannah;     //SAVANNAH
                    else if(map[i][j] < .6f) map[i][j] = (int) TerrainUtils.HexRegion.Grassland;     //GRASSLAND
                    else if(map[i][j] < .925f) map[i][j] = (int) TerrainUtils.HexRegion.Forest;    //FOREST
                    else if(map[i][j] < 1f) map[i][j] = (int) TerrainUtils.HexRegion.Jungle;      //JUNGLE
                }
            }   
        }


    }

}