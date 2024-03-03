using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class ElevationGrouping : ElevationStrategy
    {
        /*
            ElevationGrouping is used to generate elevation on the map - concrete class
            ElevationGrouping is used to group together the different elevation types
        */

        // private float hill_modifier = 0.15f;         DEFAULT VALUES
        // private float large_hill_modifier = 0.10f;
        // private float mountain_modifier = 0.02f;
        // private float canyon_modifier = 0.02f;

        private float hill_modifier = 0.03f;       // TO DO: FINISH
        private float large_hill_modifier = 0.03f;
        private float mountain_modifier = 0.03f;
        private float canyon_modifier = 0.03f;

        private int hill_count = 0;
        private int large_hill_count = 0;
        private int mountain_count = 0;
        private int canyon_count = 0;

        private int mountain_range_max, mountain_range_min = 0;
        private int canyon_range_max, canyon_range_min = 0;
        public override List<List<float>> GenerateElevationMap(List<List<float>> elevation_map, Vector2 map_size, List<List<float>> regions_map) //Called from MapGeneration.cs
        {
            InitializeMonumentParameters(map_size); // Sets the parameters for the monuments
            CreateLargeHills(elevation_map, map_size, regions_map); // All functions apply to List<List<float>> map
            CreateHills(elevation_map, map_size, regions_map);
            CreateMountainRanges(elevation_map, map_size, regions_map);
            CreateCanyons(elevation_map, map_size, regions_map);
            return elevation_map;
        }

        private void InitializeMonumentParameters(Vector2 map_size){
            hill_count = (int) (map_size.x * map_size.y * hill_modifier);
            large_hill_count = (int) (map_size.x * map_size.y * large_hill_modifier);
            mountain_count = (int) (map_size.x * map_size.y * mountain_modifier);
            canyon_count = (int) (map_size.x * map_size.y * canyon_modifier);

            mountain_range_max = 4; // TO DO: Make this a percentage of the map size
            mountain_range_min = 1; // TO DO: Make this a percentage of the map size
            canyon_range_max = 10; // TO DO: Make this a percentage of the map size
            canyon_range_min = 3; // TO DO: Make this a percentage of the map size
        }

        private void CreateHills(List<List<float>> elevation_map, Vector2 map_size, List<List<float>> regions_map){
            for(int i = 0; i < hill_count; i++){
                MapUtils.RandomSpawn(map_size, elevation_map, (float) ElevationEnums.HexElevation.Small_Hill);
            }
        }

        private void CreateLargeHills(List<List<float>> elevation_map, Vector2 map_size, List<List<float>> regions_map){
            for(int i = 0; i < large_hill_count; i++){
                Vector2 coord = MapUtils.RandomSpawn(map_size, elevation_map, (float) ElevationEnums.HexElevation.Large_Hill, 1, 1);
                MapUtils.CircularSpawn((int) coord.x, (int) coord.y, elevation_map, (float) ElevationEnums.HexElevation.Small_Hill);
            }
        }

        private void CreateMountainRanges(List<List<float>> elevation_map, Vector2 map_size, List<List<float>> regions_map){
            for(int i = 0; i < mountain_count; i++){

                Vector2 coord = MapUtils.RandomSpawn(map_size, elevation_map, (float) ElevationEnums.HexElevation.Mountain);

                for(int j = 0; j < UnityEngine.Random.Range(mountain_range_min, mountain_range_max); j++){

                    float elevation = Random.Range(0, 101) < 25 ? (int) ElevationEnums.HexElevation.Mountain : 0;

                    coord = MapUtils.LinearSpawn((int) coord.x, (int) coord.y, elevation_map, (float) ElevationEnums.HexElevation.Mountain);
                }

            }
        }
        
        private void CreateCanyons(List<List<float>> elevation_map, Vector2 map_size, List<List<float>> regions_map){
            for(int i = 0; i < canyon_count; i++){
                Vector2 coord = MapUtils.RandomSpawn(map_size, elevation_map, (float) ElevationEnums.HexElevation.Canyon);
                for(int j = 0; j < Random.Range(canyon_range_min, canyon_range_max); j++){

                    float elevation = Random.Range(0, 101) < 25 ? (int) ElevationEnums.HexElevation.Canyon : (int) ElevationEnums.HexElevation.Valley;

                    coord = MapUtils.LinearSpawn((int) coord.x, (int) coord.y, elevation_map, (float) elevation);
                }
            }
        }
    }
}