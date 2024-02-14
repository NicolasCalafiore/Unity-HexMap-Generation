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

    public class  MapManager
    {

        private const ElevationStrat elevation_strategy = ElevationStrat.Groupings;                  
        private const LandStrat land_strategy = LandStrat.Perlin;
        private const RegionStrat region_strategy = RegionStrat.MapFactor;
        private const FeaturesStrat features_strategy = FeaturesStrat.RegionSpecific;
        private const ResourceStrat resource_strategy = ResourceStrat.RegionRandom;
        private const CapitalStrat capital_strategy =  CapitalStrat.Random;  //TO DO: Convert to enums
        private Vector2 map_size = new Vector2(128, 128);

        public TerrainMapHandler terrain_map_handler;
        public CityMapHandler city_map_handler;
        public TerritoryMapHandler territory_map_handler;

        public MapManager(){}

        public void GenerateTerrainMaps(){
            this.terrain_map_handler = new TerrainMapHandler();
            terrain_map_handler.GenerateTerrainMap(elevation_strategy, land_strategy, region_strategy, features_strategy, resource_strategy, map_size);
        }

        public void GenerateCitiesMaps(){
            List<Player> player_list = Player.GetPlayerList();
            this.city_map_handler = new CityMapHandler();
            city_map_handler.GenerateCitiesMap(terrain_map_handler, player_list, map_size, capital_strategy);
        }

        public void GenerateTerritoryMaps(){
            List<Player> player_list = Player.GetPlayerList();
            this.territory_map_handler = new TerritoryMapHandler();
            territory_map_handler.GenerateCapitalTerritory(city_map_handler, player_list, map_size);
        }

        public void GenerateShores(){
            terrain_map_handler.GenerateShores();
        }

        public Vector2 GetMapSize(){
            return map_size;
        }

        public enum ElevationStrat{
            Groupings,
        }

        public enum LandStrat{
            Perlin,
            
        }

        public enum RegionStrat{
            MapFactor,
        }

        public enum FeaturesStrat{
            RegionSpecific,
        }

        public enum CapitalStrat{
            Random,
        }

        public enum ResourceStrat{
            RegionRandom,
        }


    }
}



  