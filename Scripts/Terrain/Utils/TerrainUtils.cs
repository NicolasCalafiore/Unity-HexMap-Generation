using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace Terrain
{
    public static class TerrainUtils
    {
        /*
            Contains all functions used for general terrain manipulation
            Used to generate all List<List<float>> maps
            Used to normalize all List<List<float>> maps
            Used to generate all Vector2 spawn locations
        */

        public static void NormalizePerlinMap(List<List<float>> map){   // Normalizes Perlin Noise Map
            float max = map.SelectMany(x => x).Max();
            float min = map.SelectMany(x => x).Min();
            float range = max - min;

            for(int i = 0; i < map.Count; i++){
                for(int j = 0; j < map[i].Count; j++){
                    map[i][j] = (map[i][j] - min) / range;
                }
            }
        }
        public static List<List<float>> GenerateMap(Vector2 map_size){  // Generates List<List<float>> map with 0 values
            List<List<float>> map = new List<List<float>>();
            for (int i = 0; i < map_size.x; i++)
            {
                List<float> row = new List<float>();
                for (int j = 0; j < map_size.y; j++)
                {
                    row.Add(0);
                }
                map.Add(row);
            }
            return map;

        }

        public static Vector2 RandomSpawn(Vector2 map_size, List<List<float>> elevation_map, float value, int min = 0, int limiter = 0){    // Coordinate value at random location on map
            int i, j = 0;
            i = UnityEngine.Random.Range(min, (int) map_size.x - limiter);
            j = UnityEngine.Random.Range(min, (int) map_size.y - limiter);
            elevation_map[i][j] = value;
            return new Vector2(i, j);
        }

        public static Vector3 RandomVector3(Vector2 map_size){  // Coordinate value at random location on map
            int i, j = 0;
            i = UnityEngine.Random.Range(0, (int) map_size.x);
            j = UnityEngine.Random.Range(0, (int) map_size.y);
            return new Vector3(i, 0, j);
        }

        public static void CircularSpawn(int i, int j, List<List<float>> map, float value){ // Sets map value at coordinate and surrounding coordinates
            map[i][j - 1] = value;
            map[i][j + 1] = value;
            map[i - 1][j] = value;
            map[i + 1][j] = value;
            map[i + 1][j - 1] = value;
            map[i - 1][j + 1] = value;  
        
        }

        public static void GeneratePerlinNoiseMap(List<List<float>> map, Vector2 map_size, float scale){    // Generates Perlin Noise Map
            int offsetX = UnityEngine.Random.Range(0, 100000);
            int offsetY = UnityEngine.Random.Range(0, 100000);

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    float xCoord = (float) i / map_size.x * scale + offsetX;
                    float yCoord = (float) j / map_size.y * scale + offsetY;

                    map[i][j] = Mathf.PerlinNoise(xCoord, yCoord);
                }
            }
        }

        public static Vector2 LinearSpawn(int i, int j, List<List<float>> map, float value){    // Sets map value at coordinate and successive randomly-linear coordinates

            if(UnityEngine.Random.Range(0, 101) < 50){
                i = i;
            } else {
                i -= 1;
            }
            if(UnityEngine.Random.Range(0, 101) < 50){
                i = i;
            } else {
                i += 1;
            }

            if(UnityEngine.Random.Range(0, 101) < 50){
                j = j;
            } else {
                j -= 1;
            }
            if(UnityEngine.Random.Range(0, 101) < 50){
                j = j;
            } else {
                j += 1;
            }

            if(i < 0){
                i = 0;
            }
            if(i > map.Count - 1){
                i = map.Count - 1;
            }
            if(j < 0){
                j = 0;
            }
            if(j > map[i].Count - 1){
                j = map[i].Count - 1;
            }

            map[i][j] = value;
            return new Vector2(i, j);
        }


        public static List<List<float>> CombineMapValue(List<List<float>> map1, List<List<float>> map2, Vector2 map_size, int value){   // Combines two List<List<float>> maps into one List<List<float>> map
            List<List<float>> map = new List<List<float>>();

                for (int i = 0; i < map_size.x; i++)
                {
                    List<float> row = new List<float>();
                    for (int j = 0; j < map_size.y; j++)
                    {
                        // Initialize with 0 or the specified value based on the condition
                        float cellValue = (map1[i][j] == value || map2[i][j] == value) ? value : 1;
                        row.Add(cellValue);
                    }
                    map.Add(row); // Add the row to the map
                }

                return map;
        }
    }
}