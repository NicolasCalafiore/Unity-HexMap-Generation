using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace Terrain
{
    public static class MapUtils
    {
       /*
            Contains all functions used for general terrain manipulation
            Used to generate all List<List<float>> maps
            Used to normalize all List<List<float>> maps
            Used to generate all Vector2 spawn locations
        */
        private static int temp_one = 0;

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

        public static void RatioPerlinMap(float range, List<List<float>> map){
            for(int i = 0; i < map.Count; i++){
                for(int j = 0; j < map[i].Count; j++){
                    map[i][j] = map[i][j] * range;
                    map[i][j] -= 0.5f * range;
                }
            }
        }
        public static List<List<float>> GenerateMap(float value = 0){  // Generates List<List<float>> map with 0 values
            Vector2 map_size = MapManager.GetMapSize();
            return Enumerable.Range(0, (int)map_size.x)
                            .Select(_ => Enumerable.Repeat(value, (int)map_size.y).ToList())
                            .ToList();
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

        public static bool CircularSearch(int i, int j, List<List<float>> map, int target, int iterations = 1)
        {
            int mapSizeX = map.Count;
            int mapSizeY = map[0].Count;


            // Check immediate neighbors
            if ((j - 1 >= 0 && map[i][j - 1] == target) ||
                (j + 1 < mapSizeY && map[i][j + 1] == target) ||
                (i - 1 >= 0 && map[i - 1][j] == target) ||
                (i + 1 < mapSizeX && map[i + 1][j] == target) ||
                (i + 1 < mapSizeX && j - 1 >= 0 && map[i + 1][j - 1] == target) ||
                (i - 1 >= 0 && j + 1 < mapSizeY && map[i - 1][j + 1] == target))
            {
                return true;
            }

            // Base case to stop recursion
            if (iterations <= 1) return false;

            // Recursive calls
            return (j - 1 >= 0 && CircularSearch(i, j - 1, map, target, iterations - 1)) ||
                (j + 1 < mapSizeY && CircularSearch(i, j + 1, map, target, iterations - 1)) ||
                (i - 1 >= 0 && CircularSearch(i - 1, j, map, target, iterations - 1)) ||
                (i + 1 < mapSizeX && CircularSearch(i + 1, j, map, target, iterations - 1)) ||
                (i + 1 < mapSizeX && j - 1 >= 0 && CircularSearch(i + 1, j - 1, map, target, iterations - 1)) ||
                (i - 1 >= 0 && j + 1 < mapSizeY && CircularSearch(i - 1, j + 1, map, target, iterations - 1));
        }

        public static void CircularSpawn(int i, int j, List<List<float>> map, float value, int iterations = 1)
        {
            int mapSizeX = map.Count;
            int mapSizeY = map[0].Count;

            if (j - 1 >= 0 && j - 1 < mapSizeY)
                map[i][j - 1] = value;
            if (j + 1 >= 0 && j + 1 < mapSizeY)
                map[i][j + 1] = value;
            if (i - 1 >= 0 && i - 1 < mapSizeX)
                map[i - 1][j] = value;
            if (i + 1 >= 0 && i + 1 < mapSizeX)
                map[i + 1][j] = value;
            if (i + 1 >= 0 && i + 1 < mapSizeX && j - 1 >= 0 && j - 1 < mapSizeY)
                map[i + 1][j - 1] = value;
            if (i - 1 >= 0 && i - 1 < mapSizeX && j + 1 >= 0 && j + 1 < mapSizeY)
                map[i - 1][j + 1] = value;

            if (iterations > 1)
            {
                if (j - 1 >= 0 && j - 1 < mapSizeY)
                    CircularSpawn(i, j - 1, map, value, iterations - 1);
                if (j + 1 >= 0 && j + 1 < mapSizeY)
                    CircularSpawn(i, j + 1, map, value, iterations - 1);
                if (i - 1 >= 0 && i - 1 < mapSizeX)
                    CircularSpawn(i - 1, j, map, value, iterations - 1);
                if (i + 1 >= 0 && i + 1 < mapSizeX)
                    CircularSpawn(i + 1, j, map, value, iterations - 1);
                if (i + 1 >= 0 && i + 1 < mapSizeX && j - 1 >= 0 && j - 1 < mapSizeY)
                    CircularSpawn(i + 1, j - 1, map, value, iterations - 1);
                if (i - 1 >= 0 && i - 1 < mapSizeX && j + 1 >= 0 && j + 1 < mapSizeY)
                    CircularSpawn(i - 1, j + 1, map, value, iterations - 1);
            }
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



        public static List<Tuple<int, int>> CompareValueBorder(List<List<float>> matrix, int target_value, int border_value)
        {
            var border_list = new List<Tuple<int, int>>();
            int rows = matrix.Count;
            int cols = matrix[0].Count;


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    if (matrix[i][j] == target_value && IsBordering(matrix, i, j, border_value))
                    {
                        border_list.Add(new Tuple<int, int>(i, j));
                    }
                    
                }
            }

            return border_list;
        }

    public static int GetElementCount(int target, List<List<float>> map){
            int count = 0;
            foreach(List<float> list in map){
                foreach(float element in list){
                    if(element == target){
                        count++;
                    }
                }
            }
            return count;
        }

        static bool IsBordering(List<List<float>> matrix, int row, int col, int border_value)
        {
            int rows = matrix.Count;
            int cols = matrix[0].Count;

            // Check above
            if (row > 0 && matrix[row - 1][col] == border_value) return true;
            // Check below
            if (row < rows - 1 && matrix[row + 1][col] == border_value) return true;
            // Check left
            if (col > 0 && matrix[row][col - 1] == border_value) return true;
            // Check right
            if (col < cols - 1 && matrix[row][col + 1] == border_value) return true;

            if(row > 0 && col < cols - 1 && matrix[row - 1][col + 1] == border_value) return true;

            if(row < rows - 1 && col > 0 && matrix[row + 1][col - 1] == border_value) return true;      
        

            return false;
        }

        public static void RegionsFloodFill(List<List<float>> regions_map, List<List<float>> regions_id_map, int x, int y, int regionId, float targetRegion)
        {
            if (x < 0 || x >= regions_map.Count || y < 0 || y >= regions_map[0].Count) return; // Out of bounds
            if (regions_id_map[x][y] != 0 || regions_map[x][y] != targetRegion) return; // Already visited or not part of the target region

            // Mark the current cell with the regionId
            regions_id_map[x][y] = regionId;

            // Recursively apply to all neighboring cells
            RegionsFloodFill(regions_map, regions_id_map, x + 1, y, regionId, targetRegion);
            RegionsFloodFill(regions_map, regions_id_map, x - 1, y, regionId, targetRegion);
            RegionsFloodFill(regions_map, regions_id_map, x, y + 1, regionId, targetRegion);
            RegionsFloodFill(regions_map, regions_id_map, x, y - 1, regionId, targetRegion);
        }

        public static void ContinentsFloodFill(List<List<float>> water_map, List<List<float>> continent_map, int row, int col, int continentId)
        {
            if (row < 0 || col < 0 || row >= water_map.Count || col >= water_map[0].Count) return; // Out of bounds
            if (water_map[row][col] != 1 || continent_map[row][col] != 0) return; // Not land or already visited

            continent_map[row][col] = continentId;

            ContinentsFloodFill(water_map, continent_map, row - 1, col, continentId); // Up
            ContinentsFloodFill(water_map, continent_map, row + 1, col, continentId); // Down
            ContinentsFloodFill(water_map, continent_map, row, col - 1, continentId); // Left
            ContinentsFloodFill(water_map, continent_map, row, col + 1, continentId); // Right
            ContinentsFloodFill(water_map, continent_map, row - 1, col + 1, continentId); // 
            ContinentsFloodFill(water_map, continent_map, row + 1, col - 1, continentId); // 
        }

        public static void MakeCopyOfList(List<List<float>> map, List<List<float>> copy_map){
            for(int i = 0; i < map.Count; i++){
                for(int j = 0; j < map[i].Count; j++){
                    copy_map[i][j] = map[i][j];
                }
            }
        }

    }
}