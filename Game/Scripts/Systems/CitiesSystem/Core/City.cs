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
        private List<HexTile> hex_territory_list = new List<HexTile>();
        private Vector2 COL_ROW;
        private RegionsEnums.HexRegion region_type;
        private string name;
        private Player player;
        private float inhabitants = 500;
        private float stability = 50;
        private float nourishment = 50;
        private float construction = 15;

        public City(string name, Player player, Vector2 col_row)
        {
            this.name = name;
            this.player = player;
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

        public Player GetPlayer()
        {
            return player;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Vector2 GetColRow()
        {
            return COL_ROW;
        }

        public List<HexTile> GetHexTerritoryList()
        {
            return hex_territory_list;
        }

        public RegionsEnums.HexRegion GetRegionType()
        {
            return region_type;
        }

        public void SetRegionType(RegionsEnums.HexRegion region_type)
        {
            this.region_type = region_type;
        }

        

        public static void SetRegionTypes()
        {
            foreach (City city in capitals_list)
            {
                city.SetRegionType(HexTile.col_row_to_hex[city.GetColRow()].GetRegionType());
            }
        }
        
        public static void GenerateCapitalCityObjects(){ 
            List<Player> player_list = Player.GetPlayerList();
            int PLAYER_INDEX = 0;
            for(int i = 0; i < MapManager.city_map_handler.structure_map.Count; i++){
                for(int j = 0; j < MapManager.city_map_handler.structure_map.Count; j++){

                    if(MapManager.city_map_handler.structure_map[i][j] == (int) StructureEnums.StructureType.Capital){
                        City city = new City("Error", player_list[PLAYER_INDEX], new Vector2(i,j));
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

        public static void SetCityTerritory(){
            foreach(City city in capitals_list){
                HexTile.AddHexTileToCityTerritory(city, MapManager.GetMapSize());
            }
        }

        public static List<City> GetCapitalsList(){
            return capitals_list;
        }
        



    }
}