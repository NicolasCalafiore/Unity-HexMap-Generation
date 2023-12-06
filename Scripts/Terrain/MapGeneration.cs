using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terrain {
    public class MapGeneration : MonoBehaviour
    {

        [SerializeField] private Vector2 map_size = new Vector2();
        [SerializeField] private int elevation_strategy;
        [SerializeField] private GameObject generic_hex;
        [SerializeField] private float region_scale;


        void Start()
        {
            List<List<float>> elevation_map = GenerateElevationMap();
            List<List<float>> regions_map = TerrainUtils.GenerateMap(map_size);
            TerrainUtils.GeneratePerlinNoiseMap(regions_map, map_size, region_scale);


            TerrainHandler.SpawnTerrain(map_size, generic_hex, elevation_map, regions_map);

            foreach(Hex i in TerrainHandler.GetHexList()){
                GameObject hex_go = TerrainHandler.hex_to_hex_go[i];
                hex_go.transform.SetParent(this.transform);
            }
        }



        private List<List<float>> GenerateElevationMap(){
            List<List<float>> elevation_map = TerrainUtils.GenerateMap(map_size);

            ElevationStrategy strategy = null;
            switch (elevation_strategy)
            {
                case 0:
                    strategy = new RandomStrategy();
                    break;
                case 1:
                    strategy = new GroupingStrategy();
                    break;
                default:
                    strategy = new RandomStrategy();
                    break;
            }

            strategy.GenerateElevationMap(elevation_map, map_size);
            TerrainUtils.PrintMap(elevation_map);
            return elevation_map;
        }






    }
}



  