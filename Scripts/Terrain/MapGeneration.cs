using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terrain {
    public class MapGeneration : MonoBehaviour
    {

        [SerializeField] private Vector2 map_size = new Vector2();
        [SerializeField] private int elevation_strategy;
        [SerializeField] private GameObject generic_hex;
        void Start()
        {
            List<List<float>> elevation_map = GenerateElevationMap();
            TerrainHandler.SpawnTerrain(map_size, generic_hex, elevation_map);
            DebugHandler.ShowTerrainTypes(TerrainHandler.GetHexList());

            List<List<float>> regions_map = TerrainUtils.GenerateMap(map_size);


        }



        private List<List<float>> GenerateElevationMap(){
            Debug.Log("MapGeneration");
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



                // Set the parent of the hex game object this empty gameobject
                //hex_go.transform.SetParent(this.transform);