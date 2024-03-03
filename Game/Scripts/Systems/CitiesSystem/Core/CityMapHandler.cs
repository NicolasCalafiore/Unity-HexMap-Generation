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
    
        public void GenerateCitiesMap(TerrainMapHandler terrain_map_handler, List<Player> player_list, Vector2 map_size, MapManager.CapitalStrat capital_strategy)
        {
            List<List<float>> structure_map = MapUtils.GenerateMap();

            CapitalSpawnStrategy strategy = null;
            switch (capital_strategy)
            {
                case MapManager.CapitalStrat.Random:
                    strategy = new CapitalRandomSpawner();
                    break;
                default:
                    strategy = new CapitalRandomSpawner();
                    break;
            }

            structure_map = strategy.GenerateCapitalMap(terrain_map_handler.GetWaterMap(), player_list, 
                                                        map_size, terrain_map_handler.GetFeaturesMap(), 
                                                        terrain_map_handler.GetResourceMap(), structure_map);
            
            this.structure_map = structure_map;
        }

    }
}