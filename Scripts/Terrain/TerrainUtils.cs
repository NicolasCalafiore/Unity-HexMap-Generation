using System;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public static class TerrainUtils
    {
        private static int utility_counter = 0;

        public enum HexElevation{
            Canyon = -50,
            Valley = -25,
            Flatland = 0,
            Hill = 25,
            Large_Hill = 50,
            Mountain = 150,
        }

        public enum HexRegion{
            Desert = 0,
            Savannah = 1,
            Grassland = 2,
            Forest = 3,
            Jungle = 4,

        }

        public static HexElevation GetElevationType(float elevationValue)
        {
            Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>(){
                { -50, HexElevation.Canyon},
                { -25, HexElevation.Valley},
                { 0, HexElevation.Flatland},
                { 25, HexElevation.Hill},
                { 50, HexElevation.Large_Hill},
                { 150, HexElevation.Mountain},
            };

            return elevationDict[elevationValue];
            
        }

        public static HexRegion GetRegionType(float regionValue)
        {
            Dictionary<float, HexRegion> regionDict = new Dictionary<float, HexRegion>(){
                { 0, HexRegion.Desert},
                { 1, HexRegion.Savannah},
                { 2, HexRegion.Grassland},
                { 3, HexRegion.Forest},
                { 4, HexRegion.Jungle},
            };

            return regionDict[regionValue];
            
        }
        
        public static float[] GetElevationValues(){
            Array enumValues = Enum.GetValues(typeof(HexElevation));

            // Create a float array to store the float values
            float[] floatValues = new float[enumValues.Length];

            // Convert each enum value to float and store in the floatValues array
            int index = 0;
            foreach (var value in enumValues) {
                floatValues[index++] = (int) value;
            }

            return floatValues;
        }

        public static List<List<float>> GenerateMap(Vector2 map_size){
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

        public static void PrintMap(List<List<float>> map){


            for (int i = 0; i < map.Count; i++)
            {
                string row = "";
                for (int j = 0; j < map[i].Count; j++)
                {
                    row += map[i][j] + " ";
                }
                Debug.Log(row);
            }
        }


        public static Vector2 RandomSpawn(Vector2 map_size, List<List<float>> elevation_map, float value, int min = 0, int limiter = 0){
            int i, j = 0;
            i = UnityEngine.Random.Range(min, (int) map_size.x - limiter);
            j = UnityEngine.Random.Range(min, (int) map_size.y - limiter);
            elevation_map[i][j] = value;
            return new Vector2(i, j);
        }

        public static void CircularSpawn(int i, int j, List<List<float>> map, float value){
            map[i][j - 1] = value;
            map[i][j + 1] = value;
            map[i - 1][j] = value;
            map[i + 1][j] = value;
            map[i + 1][j - 1] = value;
            map[i - 1][j + 1] = value;
        }

        public static Vector2 LinearSpawn(int i, int j, List<List<float>> map, float value){

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

        public static void GeneratePerlinNoiseMap(List<List<float>> map, Vector2 map_size, float scale){
            int offset = UnityEngine.Random.Range(0, 100000);

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    map[i][j] = Mathf.PerlinNoise((i + offset) / map_size.x * scale, (j + offset) / map_size.y * scale);
                }
            }

            Debug.Log("!!!");
            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {

                    if(map[i][j] < .2f) map[i][j] = 0;         //DESERT
                    else if(map[i][j] < .4f) map[i][j] = 1;     //SAVANNAH
                    else if(map[i][j] < .6f) map[i][j] = 2;     //GRASSLAND
                    else if(map[i][j] < .8f) map[i][j] = 3;    //FOREST
                    else if(map[i][j] < 1f) map[i][j] = 4;      //JUNGLE
                }
            }



        }

    }
}