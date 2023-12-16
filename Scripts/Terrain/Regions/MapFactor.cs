using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Regions
{
    
    public class MapFactor : RegionStrategy
    {
        public float perlin_scale = 3.5f;

        public override List<List<float>> GenerateRegionsMap(Vector2 map_size, List<List<float>> ocean_map)
        {
            List<List<float>> rain_map = GenerateRainMap(map_size);
            List<List<float>> temperature_map = GenerateTemperatureMap(map_size);

            TerrainUtils.NormalizePerlinMap(rain_map);

            TerrainUtils.NormalizePerlinMap(temperature_map);

            DebugHandler.SpawnPerlinViewers(map_size, temperature_map, "temperature_map");
            DebugHandler.SpawnPerlinViewers(map_size, rain_map, "rain_map");

            List<List<float>> map_factors = CombineRegionFactors(rain_map, temperature_map, map_size, ocean_map);

            DebugHandler.PrintMapDebug("map_factors", map_factors);


 

            return map_factors;
        }

        private List<List<float>> CombineRegionFactors(List<List<float>> rain_map, List<List<float>> temperature_map, Vector2 map_size, List<List<float>> ocean_map){
            List<List<float>> map_factors = TerrainUtils.GenerateMap(map_size);

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){

                    if(ocean_map[i][j] == (int) EnumHandler.LandType.Water){
                        map_factors[i][j] = (float) EnumHandler.HexRegion.Ocean;
                        continue;
                    }

                    if(rain_map[i][j] <= 1f){
                        if(temperature_map[i][j] < 0.3f){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Tundra;
                        }
                        else{
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Grassland;
                        }
                    }

                    if(rain_map[i][j] < 0.6f){
                        if(temperature_map[i][j] < 0.6f){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Grassland;
                        }
                        else{
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Desert;
                        }
                    }
                    
                    if(rain_map[i][j] < 0.3f){
                        if(temperature_map[i][j] < 0.3f){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Tundra;
                        }else if(temperature_map[i][j] < 0.6f){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Plains;
                        }
                        else{
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Desert;
                        }
                    }
                }
            }
        
            return map_factors;
        }

        private List<List<float>> GenerateRainMap(Vector2 map_size){
            List<List<float>> rain_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(rain_map, map_size, perlin_scale);
             return rain_map;
        }

        private List<List<float>> GenerateTemperatureMap(Vector2 map_size ){
            List<List<float>> temperature_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(temperature_map, map_size, perlin_scale);
            return temperature_map;

    }
}
}