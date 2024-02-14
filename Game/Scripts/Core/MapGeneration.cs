using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Cities;




namespace Terrain {

    public class MapGeneration
    {
            public TerrainMapHandler terrain_map_handler;
            public CityMapHandler city_map_handler;
            public TerritoryMapHandler territory_map_handler;
            private Vector2 map_size;


            public MapGeneration(Vector2 map_size){
                this.map_size = map_size;
            }

            public void GenerateTerrainMaps(int elevation_strategy, int land_strategy, int region_strategy, int features_strategy){
                this.terrain_map_handler = new TerrainMapHandler(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy);
                terrain_map_handler.GenerateTerrainMap();
            }

            public void GenerateCitiesMaps(List<Player> player_list, int capital_strategy){
                this.city_map_handler = new CityMapHandler();
                city_map_handler.GenerateCitiesMap(terrain_map_handler.water_map, player_list, map_size, terrain_map_handler.features_map, terrain_map_handler.resource_map, capital_strategy);
            }

            public void GenerateTerritoryMaps(List<Player> player_list){
                this.territory_map_handler = new TerritoryMapHandler();
                territory_map_handler.GenerateCapitalTerritory(city_map_handler.structure_map, player_list, map_size);
            }

        
    }
}



  