using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


namespace TerrainGeneration
{
    public class GroupingsElevations : ElevationStrategy
    {

        private readonly int max_mountains = 150;
        private readonly int min_mountains = 75;
        private readonly int max_mountain_range = 25;
        private readonly int min_mountain_range = 5;
        private readonly int max_large_hills = 500;
        private readonly int min_large_hills = 250;
        private readonly int max_small_hills = 400;
        private readonly int min_small_hills = 300;
        private readonly int max_valley_range = 50;
        private readonly int min_valley_range = 15;
        private readonly int max_valley_num = 100;
        private readonly int min_valley_num = 25;




        public override void ElevateHexTerrain(List<Hex> hexList, Vector2 mapSize)
        {
            List<List<float>> elevationMap = GenerateElevationMap(mapSize);
            int large_hill_num = UnityEngine.Random.Range(min_large_hills, max_large_hills);
            int small_hill_num = UnityEngine.Random.Range(min_small_hills, max_small_hills);
            int valley_num = UnityEngine.Random.Range(min_valley_num, max_valley_num);
            int mountain_num = UnityEngine.Random.Range(min_mountains, max_mountains);

                for (int i = 0; i < small_hill_num; i++)
                {
                    CreateSmallHill(elevationMap, mapSize);
                }
                for (int i = 0; i < large_hill_num; i++)
                {
                    CreateLargeHill(elevationMap, mapSize);
                }
                for (int i = 0; i < mountain_num; i++)
                {
                    CreateMountain(elevationMap, mapSize, max_mountain_range);
                }
                for (int i = 0; i < valley_num; i++)
                {
                    CreateValley(elevationMap, mapSize, max_valley_range);
                }
            
            int index = 0;
            int row = 0;
            foreach (var hex in hexList)
            {
                if (index == mapSize.x)
                {
                    index = 0;
                    row++;
                }

                hex.SetElevation(elevationMap[row][index]);
                index++;
            }
        }

        private List<List<float>> GenerateElevationMap(Vector2 mapSize)
        {
            List<List<float>> elevationMap = new List<List<float>>();

            for (int i = 0; i < mapSize.x; i++)
            {
                List<float> row = Enumerable.Repeat(0f, (int)mapSize.y).ToList();
                elevationMap.Add(row);
            }

            return elevationMap;
        }

        private void PrintElevationMap(List<List<float>> elevationMap)
        {
            foreach (var row in elevationMap)
            {
                Debug.Log(string.Join(" ", row));
            }
        }


        private void CreateSmallHill(List<List<float>> elevationMap, Vector2 mapSize){
            int i = UnityEngine.Random.Range(0, (int) mapSize.x);
            int j = UnityEngine.Random.Range(0, (int) mapSize.y);

            elevationMap[i][j] = .25f;
        }

        private void CreateLargeHill(List<List<float>> elevationMap, Vector2 mapSize){
            int i = UnityEngine.Random.Range(1, (int) mapSize.x - 1);
            int j = UnityEngine.Random.Range(1, (int) mapSize.y - 1);

            elevationMap[i][j] = .5f;

            elevationMap[i][j - 1] = .25f;
            elevationMap[i][j + 1] = .25f;
            elevationMap[i + 1][j - 1] = .25f;
            elevationMap[i - 1][j] = .25f;
            elevationMap[i - 1][j + 1] = .25f;
            elevationMap[i + 1][j] = .25f;
        }

        private void CreateMountain(List<List<float>> elevationMap, Vector2 mapSize, int size){
            size = UnityEngine.Random.Range(min_mountain_range, max_mountain_range);
            int i = UnityEngine.Random.Range(size, (int) mapSize.x - size);
            int j = UnityEngine.Random.Range(size, (int) mapSize.y - size);

            elevationMap[i][j] = 1.5f;
            for (int k = 0; k < size; k++)
            {
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

                elevationMap[i][j] = 1.5f;

                if(UnityEngine.Random.Range(0, 101) < 30){
                    elevationMap[i][j] = 0f;                    // 30% chance to stop mountain range/gap
                }

            }

        }

        private void CreateValley(List<List<float>> elevationMap, Vector2 mapSize, int size){
            size = UnityEngine.Random.Range(min_valley_range, size);
            int i = UnityEngine.Random.Range(size, (int) mapSize.x - size);
            int j = UnityEngine.Random.Range(size, (int) mapSize.y - size);
            elevationMap[i][j] = -.5f;
            for (int k = 0; k < size; k++)
            {
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

                float elevation = UnityEngine.Random.Range(0, 101) < 50 ? -.25f : -.5f;
                elevationMap[i][j] = elevation;
            }

        }


    }












}