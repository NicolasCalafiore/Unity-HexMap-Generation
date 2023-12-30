using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;




namespace Terrain {

    public class MapGeneration : MonoBehaviour
    {

        [SerializeField] private Vector2 map_size = new Vector2();
        [SerializeField] private int elevation_strategy;
        [SerializeField] private int land_strategy;
        [SerializeField] private int region_strategy;
        [SerializeField] private int features_strategy;
        [SerializeField] public GameObject perlin_map_object;
        private List<HexTile> hex_list = new List<HexTile>();   

        

        void Start()
        {
            hex_list = CreateHexObjects(map_size);
            List<List<float>> ocean_map = GenerateWaterMap();
            List<List<float>> regions_map = GenerateRegionMap(ocean_map);
            List<List<float>> elevation_map = GenerateElevationMap(regions_map);
            List<List<float>> features_map = GenerateFeaturesMap(regions_map, ocean_map);

            SetHexElevation(elevation_map);
            SetHexRegion(regions_map);
            SetHexLand(ocean_map);
            SetHexFeatures(features_map);

            SetHexFactors(hex_list);

            TerrainHandler.SpawnTerrain(map_size, hex_list);

            InitializeDebugComponents(elevation_map, regions_map, features_map, ocean_map);

        }

        private static void SetHexFactors(List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                SetFeatureFactor(hex);
                SetLandDecorator(hex);
                SetRegionDecorator(hex);
            }
        }

        private static void SetRegionDecorator(HexTile hex)
        {
            if(hex.GetRegionType() == EnumHandler.HexRegion.Plains){
                hex = new PlainDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Desert){
                hex = new DesertDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Grassland){
                hex = new GrasslandDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Highlands){
                hex = new HighlandsDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Jungle){
                hex = new JungleDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Swamp){
                hex = new SwampDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Tundra){
                hex = new TundraDecorator(hex);
            }

        }

        private static void SetLandDecorator(HexTile hex)
        {
            if(hex.GetLandType() == EnumHandler.LandType.Water){
                hex = new WaterDecorator(hex);
            }
            if(hex.GetLandType() == EnumHandler.LandType.Land){
                hex = new LandDecorator(hex);
            }
        }

        private static void SetFeatureFactor(HexTile hex){

            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Forest){
                hex = new ForestDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Rocks){
                hex = new RockDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Jungle){
                hex = new JungleDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Oasis){
                hex = new OasisDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Swamp){
                hex = new SwampDecorator(hex);
            }
        }


        

        private void InitializeDebugComponents(List<List<float>> elevation_map, List<List<float>> regions_map, List<List<float>> features_map, List<List<float>> ocean_map){
            DebugHandler.SetHexAsChildren(this);
            DebugHandler.SpawnPerlinViewers(map_size, elevation_map, "elevation");
            DebugHandler.SpawnPerlinViewers(map_size, regions_map, "regions_map");

            DebugHandler.PrintMapDebug("ocean_map", ocean_map);
            DebugHandler.PrintMapDebug("regions_map", regions_map);
            DebugHandler.PrintMapDebug("features_map", features_map);
            DebugHandler.PrintMapDebug("elevation_map", elevation_map);
        }

            private List<HexTile> CreateHexObjects(Vector2 map_size){
            List<HexTile> hex_list = new List<HexTile>();

            // Create Hex objects for each column and row in the map
            for(int column = 0; column < map_size.x; column++)
            {
                for(int row = 0; row < map_size.y; row++)
                {
                    HexTile hex = new HexTile(column, row);
                    hex_list.Add(hex);
                }
            }

            return hex_list;
        }

        private List<List<float>> GenerateFeaturesMap(List<List<float>> regions_map, List<List<float>> ocean_map)
        {
            FeaturesStrategy strategy = null;
            switch (features_strategy)
            {
                case 0:
                    strategy = new RegionSpecificRandom();
                    break;
                case 1:
                    strategy = new RegionSpecificRandom();
                    break;
                default:
                    strategy = new RegionSpecificRandom();
                    break;
            }

            List<List<float>> features_map = strategy.GenerateFeaturesMap(map_size, regions_map, ocean_map);
            return features_map;
        }

        private List<List<float>> GenerateRegionMap(List<List<float>> ocean_map){

            RegionStrategy strategy = null;

            switch(region_strategy){
                case 0:
                    strategy = new RegionPerlin();
                    break;
                case 1:
                    strategy = new MapFactor();
                    break; 
                default:
                    strategy = new RegionPerlin();
                    break;
            }

            List<List<float>> region_map = strategy.GenerateRegionsMap(map_size, ocean_map);
 
            return region_map;
        }
        
        private List<List<float>> GenerateWaterMap(){

        WaterStrategy strategy = null;
            switch (land_strategy)
            {
                case 0:
                    strategy = new PerlinStrategy();
                    break;
                case 1:
                    strategy = new PerlinStrategy();
                    break;
                default:
                    strategy = new PerlinStrategy();
                    break;
            }

            List<List<float>> ocean_map = strategy.GenerateWaterMap(map_size, "ocean");
            List<List<float>> river_map = strategy.GenerateWaterMap(map_size, "river");

            ocean_map = TerrainUtils.CombineMapValue(ocean_map, river_map, map_size, (int) EnumHandler.LandType.Water);
            strategy.OceanBorder(ocean_map);

            DebugHandler.SpawnPerlinViewers(map_size, river_map, "river_map");
            DebugHandler.SpawnPerlinViewers(map_size, ocean_map, "ocean_map");

            return ocean_map;
        }



        private List<List<float>> GenerateElevationMap(List<List<float>> regions_map){
            List<List<float>> elevation_map = TerrainUtils.GenerateMap(map_size);

            ElevationStrategy strategy = null;
            switch (elevation_strategy)
            {
                case 0:
                    strategy = new ElevationRandom();
                    break;
                case 1:
                    strategy = new ElevationGrouping();
                    break;
                default:
                    strategy = new ElevationRandom();
                    break;
            }

            strategy.GenerateElevationMap(elevation_map, map_size, regions_map);
            return elevation_map;
        }



        public Vector2 GetMapSize(){
            return map_size;
        }

        private void SetHexFeatures(List<List<float>> features_map)
        {
            for(int i = 0; i < hex_list.Count; i++){
                Vector2 coordinates = hex_list[i].GetColRow();
                hex_list[i].SetFeatureType(EnumHandler.GetFeatureType(features_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        private void SetHexLand(List<List<float>> land_map)
        {
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetLandType(EnumHandler.GetLandType(land_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        private void SetHexElevation(List<List<float>> elevation_map){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetElevation(elevation_map[ (int) coordinates.x][ (int) coordinates.y], EnumHandler.GetElevationType(elevation_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }
        private void SetHexRegion(List<List<float>> regions_map){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetRegionType(EnumHandler.GetRegionType(regions_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public List<HexTile> GetHexList(){
            return hex_list;
        }

    }
}



  