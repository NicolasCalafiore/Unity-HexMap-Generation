using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace TerrainGeneration
{
    public class ElevationUtils
    {        
        public enum HexElevationTypes{
            FLATLAND = 0,
            HILL = 25,
            LARGE_HILL = 50,
            MOUNTAIN = 150,
            CANYON = -50,
            VALLEY = -25
        }
        public enum RegionTypes{
            OCEAN = 0,
        }


        public static void CircularSpawn(int i, int j, List<List<float>> map, float value){
            map[i][j - 1] = value;
            map[i][j + 1] = value;
            map[i - 1][j] = value;
            map[i + 1][j] = value;
            map[i + 1][j - 1] = value;
            map[i - 1][j + 1] = value;
        }



        public static Vector2 LinearSpawn(int i, int j){
            
            bool check = true;
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


            return new Vector2(i, j);
        }


        public static List<List<float>> GenerateElevationMap(Vector2 mapSize, float default_elevation = 0f)
        {
            List<List<float>> elevationMap = new List<List<float>>();

            for (int i = 0; i < mapSize.x; i++)
            {
                List<float> row = Enumerable.Repeat(default_elevation, (int)mapSize.y).ToList();
                elevationMap.Add(row);
            }

            return elevationMap;
        }

        public static void UpdateHexElevation(List<Hex> hexList,  Vector2 mapSize, List<List<float>> elevationMap){
            int index = 0;
            int row = 0;
            foreach (var hex in hexList)
            {
                if (index == mapSize.x)
                {
                    index = 0;
                    row++;
                }

                hex.SetElevation(elevationMap[row][index], GetElevationType(elevationMap[row][index]));
                index++;
            }
        }


        public static List<List<float>> GenerateRegionsMap(Vector2 mapSize, int numSegments)
        {
            List<List<float>> regionMap = new List<List<float>>();

            // Initialize the map
            for (int i = 0; i < mapSize.x; i++)
            {
                List<float> row = new List<float>();
                for (int j = 0; j < mapSize.y; j++)
                {
                    row.Add(-1f); // Initialize with -1f
                }
                regionMap.Add(row);
            }

            int segmentWidth = (int)mapSize.x / numSegments;
            int segmentHeight = (int)mapSize.y / numSegments;

            // Fill each region
            for (int segment = 0; segment < numSegments; segment++)
            {
                // Define the start and end points for this region
                int startX = segment * segmentWidth;
                int endX = (segment + 1) * segmentWidth;
                int startY = segment * segmentHeight;
                int endY = (segment + 1) * segmentHeight;

                // Adjust the last segment to cover the entire grid if necessary
                if (segment == numSegments - 1) {
                    endX = (int)mapSize.x;
                    endY = (int)mapSize.y;
                }

                // Fill the region with the segment number
                for (int i = startX; i < endX; i++)
                {
                    for (int j = startY; j < endY; j++)
                    {
                        regionMap[i][j] = segment;
                    }
                }
            }

            PrintMap(regionMap);
            return regionMap;
        }

        public static void PrintMap(List<List<float>> map)
        {
            foreach (var row in map)
            {
                Debug.Log(string.Join(" ", row));
            }
        }

        private static HexElevationTypes GetElevationType(float elevationValue)
        {
            switch (elevationValue)
            {
                case (float) HexElevationTypes.FLATLAND / 100f: return HexElevationTypes.FLATLAND;
                case (float) HexElevationTypes.HILL / 100f: return HexElevationTypes.HILL;
                case (float) HexElevationTypes.LARGE_HILL / 100f: return HexElevationTypes.LARGE_HILL;
                case (float) HexElevationTypes.MOUNTAIN / 100f: return HexElevationTypes.MOUNTAIN;
                case (float) HexElevationTypes.VALLEY / 100f: return HexElevationTypes.VALLEY;
                case (float) HexElevationTypes.CANYON / 100f: return HexElevationTypes.CANYON;
                default: return HexElevationTypes.FLATLAND; 
            }
        }




    }
}