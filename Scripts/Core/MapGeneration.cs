using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;




namespace Terrain {

    public class MapGeneration
    {
            public TerrainMapHandler terrain_map_handler;

            public MapGeneration(){

            }

            public void GenerateTerrainMaps(Vector2 map_size, int elevation_strategy, int land_strategy, int region_strategy, int features_strategy){
                this.terrain_map_handler = new TerrainMapHandler(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy);
                terrain_map_handler.GenerateTerrainMap();
            }





        
    }
}



  