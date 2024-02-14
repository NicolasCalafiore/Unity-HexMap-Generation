using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Scripts.Objects
{
    public class City
    {
        public static List<City> capital_strategy = new List<City>();
        private static List<City> capitals_list = new List<City>();
        public static Dictionary<City, GameObject> city_to_city_go = new Dictionary<City, GameObject>(); // Given Hex gives Hex-Object

        public List<HexTile> hex_territory_list = new List<HexTile>();
        private Vector2 COL_ROW;
        private string name;
        private int player_id;
        public float inhabitants = 500;
        public float stability = 50;
        public float nourishment = 50;
        public float construction = 15;

        public City(string name, int player_id, Vector2 col_row)
        {
            this.name = name;
            this.player_id = player_id;
            this.COL_ROW = col_row;
        }

        public void CalculateCityNourishment(TerritoryManager territory_manager){
            this.nourishment += territory_manager.CalculateCityNourishment(this);
        }

        public void CalculateCityConstruction(TerritoryManager territory_manager){
            this.construction += territory_manager.CalculateCityConstruction(this);
        }

        public string GetName()
        {
            return name;
        }

        public int GetPlayerId()
        {
            return player_id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Vector2 GetColRow()
        {
            return COL_ROW;
        }


        public static void GenerateCapitalCityObjects(MapManager map_generation){ 
            List<Player> player_list = Player.GetPlayerList();
            int PLAYER_INDEX = 0;
            for(int i = 0; i < map_generation.city_map_handler.structure_map.Count; i++){
                for(int j = 0; j < map_generation.city_map_handler.structure_map.Count; j++){

                    if(map_generation.city_map_handler.structure_map[i][j] == (int) StructureEnums.StructureType.Capital){
                        City city = new City("Error", player_list[PLAYER_INDEX].id, new Vector2(i,j));
                        capitals_list.Add(city);
                        player_list[PLAYER_INDEX].AddCity(city); // Add city to player
                        PLAYER_INDEX++;
                    }
                    
                }
            }
        }

        public static string GenerateCityName(HexTile hex){
            List<string> cityNames = IOHandler.ReadCityNamesRegionSpecified(hex.GetRegionType().ToString());
            int random_pick = UnityEngine.Random.Range(0, cityNames.Count);

            string name = cityNames[random_pick];
            return name;
        }

        public static void SetCityTerritory(MapManager map_generation){
            foreach(City city in capitals_list){
                HexTile.AddHexTileToCityTerritory(city, map_generation.GetMapSize());
            }
        }

        public static List<City> GetCapitalsList(){
            return capitals_list;
        }


    }
}