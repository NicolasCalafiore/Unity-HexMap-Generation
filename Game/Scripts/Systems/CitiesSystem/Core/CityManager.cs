using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Terrain;
using UnityEngine;
namespace Cities
{
    public static class CityManager{
        public static List<City> capital_strategy = new List<City>();
        private static List<City> capitals_list = new List<City>();
        public static Dictionary<City, GameObject> city_to_city_go = new Dictionary<City, GameObject>(); // Given Hex gives Hex-Object
    
        // Sets the region type of the capitals for name generation
        public static void SetRegionTypes(){
            foreach (City city in capitals_list)
                city.region_type = HexManager.col_row_to_hex[city.col_row].region_type;
        }
        
        // Generates the capital cities for each player
        // The capital cities are generated based on the structure map
        public static void GenerateCapitalCityObjects()
        {
            for(int i = 0; i < MapManager.city_map_handler.structure_map.Count; i++)
                for(int j = 0; j < MapManager.city_map_handler.structure_map.Count; j++)
                    IfCapitalCreateCity(i, j);
        }

        // If the structure map has a capital structure, a city is created
        public static void IfCapitalCreateCity(int i, int j){
            if(MapManager.city_map_handler.structure_map[i][j] == (int) StructureEnums.StructureType.Capital)
                CreateCity(i, j);
        }

        private static void CreateCity(int i, int j){
            City city = new City("Error", PlayerManager.player_list[capitals_list.Count], new Vector2(i,j));
            capitals_list.Add(city);
            PlayerManager.player_list[capitals_list.Count - 1].AddCity(city); 
        }

        public static string GenerateCityName(HexTile hex){
            List<string> cityNames = IOHandler.ReadCityNamesRegionSpecified(hex);   // TO DO: OPTIMIZATION POINT
            return cityNames[UnityEngine.Random.Range(0, cityNames.Count)];
        }

        public static void SetCityTerritory(){
            foreach(City city in capitals_list)
                HexManager.AddHexTileToCityTerritory(city);
        }

        public static List<City> GetCapitalsList() => capitals_list;

        
    
    }
}