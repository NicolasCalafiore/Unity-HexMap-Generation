using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace TerrainGeneration
{
    public class GroupingsElevations : ElevationStrategy
    {

        private readonly int max_mountains = 100;
        private readonly int min_mountains = 50;
        private readonly int max_mountain_range = 25;
        private readonly int min_mountain_range = 10;
        private readonly int max_large_hills = 500;
        private readonly int min_large_hills = 250;
        private readonly int max_small_hills = 400;
        private readonly int min_small_hills = 300;
        private readonly int max_valley_range = 25;
        private readonly int min_valley_range = 15;
        private readonly int max_valley_num = 75;
        private readonly int min_valley_num = 30;
    




        public override void ElevateHexTerrain(List<Hex> hexList, Vector2 mapSize)
        {
            List<List<float>> elevationMap = ElevationUtils.GenerateElevationMap(mapSize);
            GenerateMonuments(elevationMap, mapSize);
            ElevationUtils.UpdateHexElevation(hexList, mapSize, elevationMap);
            //GenerateRegionsMap(mapSize, 15);

        }

 
        private void CreateSmallHill(List<List<float>> elevationMap, Vector2 mapSize){
            int i = UnityEngine.Random.Range(0, (int) mapSize.x);
            int j = UnityEngine.Random.Range(0, (int) mapSize.y);

            elevationMap[i][j] = (int) ElevationUtils.HexElevationTypes.HILL / 100;
        }

        private void CreateLargeHill(List<List<float>> elevationMap, Vector2 mapSize){
            int i = UnityEngine.Random.Range(1, (int) mapSize.x - 1);
            int j = UnityEngine.Random.Range(1, (int) mapSize.y - 1);

            elevationMap[i][j] = (int) ElevationUtils.HexElevationTypes.LARGE_HILL / 100;
            ElevationUtils.CircularSpawn(i, j, elevationMap, (int) ElevationUtils.HexElevationTypes.HILL / 100);
        }

        private void CreateMountain(List<List<float>> elevationMap, Vector2 mapSize){
            int size = UnityEngine.Random.Range(min_mountain_range, max_mountain_range);
            int i = UnityEngine.Random.Range(size, (int) mapSize.x - size);
            int j = UnityEngine.Random.Range(size, (int) mapSize.y - size);

            elevationMap[i][j] = (int) ElevationUtils.HexElevationTypes.MOUNTAIN / 100;
            for (int k = 0; k < size; k++)
            {
                UnityEngine.Vector2 coord = ElevationUtils.LinearSpawn(i, j);
                i = (int) coord.x;
                j = (int) coord.y;

                elevationMap[i][j] = (int) ElevationUtils.HexElevationTypes.LARGE_HILL / 100;

                if(UnityEngine.Random.Range(0, 101) < 30){
                    elevationMap[i][j] = 0f;                    // 30% chance to stop mountain range/gap
                }

            }
        }

        private void CreateValley(List<List<float>> elevationMap, Vector2 mapSize){
            int size = UnityEngine.Random.Range(min_valley_range, max_valley_range);
            int i = UnityEngine.Random.Range(size, (int) mapSize.x - size);
            int j = UnityEngine.Random.Range(size, (int) mapSize.y - size);
            elevationMap[i][j] = -(int) ElevationUtils.HexElevationTypes.CANYON / 100;
            for (int k = 0; k < size; k++)
            {
                UnityEngine.Vector2 coord = ElevationUtils.LinearSpawn(i, j);
                i = (int) coord.x;
                j = (int) coord.y;
                float elevation = UnityEngine.Random.Range(0, 101) < 50 ? (int) ElevationUtils.HexElevationTypes.CANYON / 100 : (int) ElevationUtils.HexElevationTypes.VALLEY / 100;;
                elevationMap[i][j] = elevation;
            }

        }

        private void GenerateMonuments(List<List<float>> elevationMap, Vector2 mapSize){
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
                    CreateMountain(elevationMap, mapSize);
                }
                for (int i = 0; i < valley_num; i++)
                {
                    CreateValley(elevationMap, mapSize);
                }
        }








    }












}