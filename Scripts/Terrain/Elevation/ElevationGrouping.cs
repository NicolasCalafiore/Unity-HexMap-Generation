using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class ElevationGrouping : ElevationStrategy
    {
        private float hill_modifier = 0.15f;
        private float large_hill_modifier = 0.10f;
        private float mountain_modifier = 0.02f;
        private float canyon_modifier = 0.02f;

        private int hill_count = 0;
        private int large_hill_count = 0;
        private int mountain_count = 0;
        private int canyon_count = 0;

        private int mountain_range_max, mountain_range_min = 0;
        private int canyon_range_max, canyon_range_min = 0;
        public override List<List<float>> GenerateElevationMap(List<List<float>> elevation_map, Vector2 map_size)
        {
            InitializeMonumentParameters(map_size);
            CreateLargeHills(elevation_map, map_size);
            CreateHills(elevation_map, map_size);
            CreateMountainRanges(elevation_map, map_size);
            CreateCanyons(elevation_map, map_size);
            return elevation_map;
        }

        private void InitializeMonumentParameters(Vector2 map_size){
            hill_count = (int) (map_size.x * map_size.y * hill_modifier);
            large_hill_count = (int) (map_size.x * map_size.y * large_hill_modifier);
            mountain_count = (int) (map_size.x * map_size.y * mountain_modifier);
            canyon_count = (int) (map_size.x * map_size.y * canyon_modifier);

            mountain_range_max = 10; // TO DO: Make this a percentage of the map size
            mountain_range_min = 3; // TO DO: Make this a percentage of the map size
            canyon_range_max = 25; // TO DO: Make this a percentage of the map size
            canyon_range_min = 10; // TO DO: Make this a percentage of the map size



        }

        private void CreateHills(List<List<float>> elevation_map, Vector2 map_size){
            for(int i = 0; i < hill_count; i++){
                TerrainUtils.RandomSpawn(map_size, elevation_map, (float) EnumHandler.HexElevation.Hill);
            }
        }

        private void CreateLargeHills(List<List<float>> elevation_map, Vector2 map_size){
            for(int i = 0; i < large_hill_count; i++){
                Vector2 coord = TerrainUtils.RandomSpawn(map_size, elevation_map, (float) EnumHandler.HexElevation.Large_Hill, 1, 1);
                TerrainUtils.CircularSpawn((int) coord.x, (int) coord.y, elevation_map, (float) EnumHandler.HexElevation.Hill);
            }
        }

        private void CreateMountainRanges(List<List<float>> elevation_map, Vector2 map_size){
            for(int i = 0; i < mountain_count; i++){

                Vector2 coord = TerrainUtils.RandomSpawn(map_size, elevation_map, (float) EnumHandler.HexElevation.Mountain);
                for(int j = 0; j < UnityEngine.Random.Range(mountain_range_min, mountain_range_max); j++){

                    float elevation = Random.Range(0, 101) < 25 ? (int) EnumHandler.HexElevation.Mountain : 0;

                    coord = TerrainUtils.LinearSpawn((int) coord.x, (int) coord.y, elevation_map, (float) EnumHandler.HexElevation.Mountain);
                }

            }
        }
        
        private void CreateCanyons(List<List<float>> elevation_map, Vector2 map_size){
            for(int i = 0; i < canyon_count; i++){
                Vector2 coord = TerrainUtils.RandomSpawn(map_size, elevation_map, (float) EnumHandler.HexElevation.Canyon);
                for(int j = 0; j < Random.Range(canyon_range_min, canyon_range_max); j++){

                    float elevation = Random.Range(0, 101) < 25 ? (int) EnumHandler.HexElevation.Canyon : (int) EnumHandler.HexElevation.Valley;

                    coord = TerrainUtils.LinearSpawn((int) coord.x, (int) coord.y, elevation_map, (float) elevation);
                }
            }
        }











    }
}