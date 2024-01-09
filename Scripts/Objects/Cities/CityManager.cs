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
    public List<List<float>> structure_map = new List<List<float>>();
    public CityManager(){

    }

    public List<List<float>> GenerateStructureMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map, int capital_strategy)
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
        
        this.structure_map = structure_map;
        return structure_map;
    }

    public void InitializeCapitalCityObjects(List<Player> player_list){ 
        int PLAYER_INDEX = 0;
        for(int i = 0; i < structure_map.Count; i++){
            for(int j = 0; j < structure_map.Count; j++){

                if(structure_map[i][j] == (int) EnumHandler.StructureType.Capital){
                    City city = new City("Error", player_list[PLAYER_INDEX].GetId(), new Vector2(i,j));
                    capitals_list.Add(city);
                    player_list[PLAYER_INDEX].AddCity(city); // Add city to player
                    PLAYER_INDEX++;
                }
                
            }
        }
    }

    public string GenerateCityName(HexTile hex){
        List<string> cityNames = IOHandler.ReadCityNames("C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\CityNames.xml", hex.GetRegionType().ToString());
        int random_pick = UnityEngine.Random.Range(0, cityNames.Count);
        string name = cityNames[random_pick];
        return name;
    }

}
