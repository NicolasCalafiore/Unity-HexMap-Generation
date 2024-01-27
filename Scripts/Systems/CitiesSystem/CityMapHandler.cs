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
    
        public List<List<float>> GenerateCitiesMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map, int capital_strategy)
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

            structure_map = strategy.GenerateCapitalMap(water_map, player_list, map_size, feature_map, resource_map, structure_map);
            DebugHandler.PrintMapDebug("City Map", structure_map);
            
            this.structure_map = structure_map;
            return structure_map;
        }

    }
}