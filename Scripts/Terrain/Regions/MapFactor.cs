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

        public override List<List<float>> GenerateRegionsMap(Vector2 map_size, GameObject perlin_map_object)
        {
            return GenerateMapFactors(map_size, perlin_map_object);
        }

        private List<List<float>> GenerateMapFactors(Vector2 map_size, GameObject perlin_map_object){
            List<List<float>> rain_map = GenerateRainMap(map_size, perlin_map_object);
            List<List<float>> temperature_map = GenerateTemperatureMap(map_size, perlin_map_object);

            // Normalize rain_map
            float rainMax = rain_map.SelectMany(x => x).Max();
            float rainMin = rain_map.SelectMany(x => x).Min();
            float rainRange = rainMax - rainMin;

            for(int i = 0; i < rain_map.Count; i++){
                for(int j = 0; j < rain_map[i].Count; j++){
                    rain_map[i][j] = (rain_map[i][j] - rainMin) / rainRange;
                }
            }

            // Normalize temperature_map
            float tempMax = temperature_map.SelectMany(x => x).Max();
            float tempMin = temperature_map.SelectMany(x => x).Min();
            float tempRange = tempMax - tempMin;

            for(int i = 0; i < temperature_map.Count; i++){
                for(int j = 0; j < temperature_map[i].Count; j++){
                    temperature_map[i][j] = (temperature_map[i][j] - tempMin) / tempRange;
                }
            }

            List<List<float>> map_factors = CombineFactorMaps(rain_map, temperature_map, map_size);

            DebugHandler.PrintMapDebug("rain_map", rain_map);
            DebugHandler.PrintMapDebug("temperature_map", temperature_map);
            DebugHandler.PrintMapDebug("map_factors", map_factors);


 

            return map_factors;
        }

        private List<List<float>> CombineFactorMaps(List<List<float>> rain_map, List<List<float>> temperature_map, Vector2 map_size){
            List<List<float>> map_factors = TerrainUtils.GenerateMap(map_size);
            for(int i = 0; i < rain_map.Count; i++){
                for(int j = 0; j < rain_map[i].Count; j++){
                    map_factors[i][j] = (int) TerrainUtils.HexRegion.Grassland;

                    if(rain_map[i][j] <= .34f && temperature_map[i][j] <= 1f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Desert; 
                    if(rain_map[i][j] <= .34f && temperature_map[i][j] <= .56f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Savannah; 
                    if(rain_map[i][j] <= .34f && temperature_map[i][j] <= .32f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Tundra;

                    if(rain_map[i][j] <= .76f && rain_map[i][j] > .34f && temperature_map[i][j] <= .4f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Forest; 
                    if(rain_map[i][j] <= .58f && rain_map[i][j] > .34f && temperature_map[i][j] >= .74f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Swamp; 
                    if(rain_map[i][j] > .58f && rain_map[i][j] > .34f && temperature_map[i][j] >= .74f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Jungle;

                    if(rain_map[i][j] > .76f && temperature_map[i][j] <= .28f) map_factors[i][j] = (int) TerrainUtils.HexRegion.Tundra;
                    if(rain_map[i][j] > .76f && temperature_map[i][j] <= .74f && temperature_map[i][j] > .28 ) map_factors[i][j] = (int) TerrainUtils.HexRegion.Forest;

                    
                }
            }
            
            return map_factors;
        }

        private List<List<float>> GenerateRainMap(Vector2 map_size, GameObject perlin_map_object){
            List<List<float>> rain_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(rain_map, map_size, perlin_scale);

            DebugHandler.SpawnPerlinViewers(map_size, rain_map, "rain_map", perlin_map_object);

             return rain_map;
        }

        private List<List<float>> GenerateTemperatureMap(Vector2 map_size, GameObject perlin_map_object){
            List<List<float>> temperature_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(temperature_map, map_size, perlin_scale);

            DebugHandler.SpawnPerlinViewers(map_size, temperature_map, "temperature_map", perlin_map_object);

            return temperature_map;

    }
}
}