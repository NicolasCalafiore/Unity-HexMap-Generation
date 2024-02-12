using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Terrain;




namespace Cities {

    public class CityMapHandler
    {
        public List<List<float>> structure_map = new List<List<float>>();

        public CityMapHandler(){

        }
    
        public void GenerateCitiesMap(TerrainMapHandler terrain_map_handler, List<Player> player_list, Vector2 map_size, int capital_strategy)
        {
            List<List<float>> structure_map = TerrainUtils.GenerateMap(map_size);

            CapitalSpawnStrategy strategy = null;
            switch (capital_strategy)
            {
                case 0:
                    strategy = new CapitalRandomSpawner();
                    break;
                case 1:
                    strategy = new CapitalRandomSpawner();
                    break;
                default:
                    strategy = new CapitalRandomSpawner();
                    break;
            }

            structure_map = strategy.GenerateCapitalMap(terrain_map_handler.water_map, player_list, 
                                                        map_size, terrain_map_handler.features_map, 
                                                        terrain_map_handler.resource_map, structure_map);
            
            this.structure_map = structure_map;
        }

    }
}