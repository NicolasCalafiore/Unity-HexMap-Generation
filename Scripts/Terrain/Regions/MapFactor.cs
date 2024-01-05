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
        /*
            MapFactor is used to generate regions on the map, respective to a rain and temperature list.
        */
        public float perlin_scale = 4.5f;   // Higher --> more random
        private static int counter = 0;     // Used to force map generation if requirements are not met after X iterations
        private static int max_iterations = 500; // Max iterations before forced map generation

        public override List<List<float>> GenerateRegionsMap(Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map)    //Called from MapGeneration.GenerateRegions
        {
            List<List<float>> rain_map = GenerateRainMap(map_size);
            List<List<float>> temperature_map = GenerateTemperatureMap(map_size);

            TerrainUtils.NormalizePerlinMap(rain_map);

            TerrainUtils.NormalizePerlinMap(temperature_map);

            List<List<float>> map_factors = CombineRegionFactors(rain_map, temperature_map, map_size, ocean_map, river_map);    // Combine rain_map and temperature_map into unique map

            if(MeetsRegionRequirements(map_factors, map_size) || counter > 500)  return map_factors; // If map meets requirements, return map
            
            else return GenerateRegionsMap(map_size, ocean_map, river_map);  // Else, generate new map
            
        }

        private bool MeetsRegionRequirements(List<List<float>> map_factors, Vector2 map_size){  // Checks if map meets region requirements

            float desert_count = 0;
            float tundra_count = 0;
            float grassland_count = 0;
            float plains_count = 0;
            float ocean_count = 0;
            float highlands_count = 0;
            float jungle_count = 0;
            float swamp_count = 0;


            foreach(List<float> row in map_factors){
                foreach(float factor in row){
                    if(factor == (float) EnumHandler.HexRegion.Desert) desert_count++;
                    if(factor == (float) EnumHandler.HexRegion.Tundra) tundra_count++;
                    if(factor == (float) EnumHandler.HexRegion.Grassland) grassland_count++;
                    if(factor == (float) EnumHandler.HexRegion.Plains) plains_count++;
                    if(factor == (float) EnumHandler.HexRegion.Ocean) ocean_count++;
                    if(factor == (float) EnumHandler.HexRegion.Highlands) highlands_count++;
                    if(factor == (float) EnumHandler.HexRegion.Jungle) jungle_count++;
                    if(factor == (float) EnumHandler.HexRegion.Swamp) swamp_count++;
                }
            }

            desert_count =  desert_count / (map_size.x * map_size.y) * 100;  // Calculate percentage of each region
            tundra_count = tundra_count / (map_size.x * map_size.y) * 100;
            grassland_count = grassland_count / (map_size.x * map_size.y) * 100;
            plains_count = plains_count / (map_size.x * map_size.y) * 100;
            highlands_count = highlands_count / (map_size.x * map_size.y) * 100;
            jungle_count = jungle_count / (map_size.x * map_size.y) * 100;
            swamp_count = swamp_count / (map_size.x * map_size.y) * 100;
            
            if(desert_count < 0.5f || tundra_count < 0.5f || plains_count < 0.5f || grassland_count < 0.5f || highlands_count < 0.5f || jungle_count < 0.5f || swamp_count < 0.5f ) return false;     //TO:DO

            return true;
        }

        private List<List<float>> CombineRegionFactors(List<List<float>> rain_map, List<List<float>> temperature_map, Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map){
            List<List<float>> map_factors = TerrainUtils.GenerateMap(map_size);

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){

                    if(ocean_map[i][j] == (int) EnumHandler.LandType.Water || river_map[i][j] == (int) EnumHandler.LandType.Water){
                        if(ocean_map[i][j] == (int) EnumHandler.LandType.Water){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Ocean;
                        }
                        else{
                            map_factors[i][j] = (float) EnumHandler.HexRegion.River;
                        }
                        continue;
                    }


                
                    if(rain_map[i][j] <= .35){
                        if(temperature_map[i][j] <= .2){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Tundra;
                        }
                        else if(temperature_map[i][j] <= .35){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Highlands;
                        }
                        else if(temperature_map[i][j] <= .4){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Grassland;
                        }
                        else if(temperature_map[i][j] <= .6){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Plains;
                        }
                        else if(temperature_map[i][j] <= 1){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Desert;
                        }
                    }


                    else if(rain_map[i][j] <= .75){
                        if(temperature_map[i][j] <= .3 && rain_map[i][j] <= .45){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Highlands;
                        }
                        else if(temperature_map[i][j] >= .7 && rain_map[i][j] <= .55){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Desert;
                        }
                        else if(temperature_map[i][j] <= .6){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Grassland;
                        }
                        else if(temperature_map[i][j] <= 1 && rain_map[i][j] <= .65){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Plains;
                        }
                        else if(temperature_map[i][j] <= 1 && rain_map[i][j] <= .75){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Jungle;
                        }
                    }

                    else if(rain_map[i][j] <= 1){
                        if(temperature_map[i][j] <= .6){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Swamp;
                        }
                        else if(temperature_map[i][j] <= 1f){
                            map_factors[i][j] = (float) EnumHandler.HexRegion.Jungle;
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