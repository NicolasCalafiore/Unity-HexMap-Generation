using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class CityManager
{
    /*
        CityManager is used to manipulate the cities in the game
    */

    public static List<City> capitals_list = new List<City>();
    public static Dictionary<City, GameObject> city_to_city_go = new Dictionary<City, GameObject>(); // Given Hex gives Hex-Object
    public CityManager(){

    }

    public void GenerateCapitalCityObjects(List<Player> player_list, MapGeneration map_generation){ 
        int PLAYER_INDEX = 0;
        for(int i = 0; i < map_generation.city_map_handler.structure_map.Count; i++){
            for(int j = 0; j < map_generation.city_map_handler.structure_map.Count; j++){

                if(map_generation.city_map_handler.structure_map[i][j] == (int) EnumHandler.StructureType.Capital){
                    City city = new City("Error", player_list[PLAYER_INDEX].id, new Vector2(i,j));
                    capitals_list.Add(city);
                    player_list[PLAYER_INDEX].AddCity(city); // Add city to player
                    PLAYER_INDEX++;
                }
                
            }
        }
    }

    public string GenerateCityName(HexTile hex){
        List<string> cityNames = IOHandler.ReadCityNamesRegionSpecified("C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\CityNames.xml", hex.region_type.ToString());
        int random_pick = UnityEngine.Random.Range(0, cityNames.Count);

        string name = cityNames[random_pick];
        return name;
    }

    public void SetCityTerritory(HexManager hex_factory, Vector2 map_size){
        foreach(City city in capitals_list){
            hex_factory.AddHexTileToCityTerritory(city, map_size);
        }
    }



    

}
