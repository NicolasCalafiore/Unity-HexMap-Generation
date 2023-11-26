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

        private readonly int max_mountains = 100;
        private readonly int max_large_hills = 300;
        private readonly int max_small_hills = 200;


        public override void ElevateHexTerrain(List<Hex> hexList, Vector2 mapSize)
        {
            List<List<float>> elevationMap = GenerateElevationMap(mapSize);
            int mountain_num = UnityEngine.Random.Range(1, max_mountains);
            int large_hill_num = UnityEngine.Random.Range(1, max_large_hills);
            int small_hill_num = UnityEngine.Random.Range(1, max_small_hills);

            for (int i = 0; i < mountain_num; i++)
            {
                CreateMountain(elevationMap, mapSize);
            }

            for (int i = 0; i < large_hill_num; i++)
            {
                CreateLargeHill(elevationMap, mapSize);
            }

            for (int i = 0; i < small_hill_num; i++)
            {
                CreateSmallHill(elevationMap, mapSize);
            }


            PrintElevationMap(elevationMap);
            
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

        private void CreateMountain(List<List<float>> elevationMap, Vector2 mapSize){
            int i = UnityEngine.Random.Range(0, (int) mapSize.x);
            int j = UnityEngine.Random.Range(0, (int) mapSize.y);

            elevationMap[i][j] = 1f;

        }


    }












}