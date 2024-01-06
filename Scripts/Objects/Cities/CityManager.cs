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

    public static Dictionary<GameObject, City> city_go_to_city = new Dictionary<GameObject, City>(); // Given City gives City-Game-Object
    public static List<City> capitals_list = new List<City>();
    List<List<float>> city_map = new List<List<float>>();
    private static int counter = 0;

    public CityManager(){

    }

    public void GenerateCityMap(List<List<float>> water_map, List<Player> player_list, Vector2 map_size, List<List<float>> feature_map, List<List<float>> resource_map)
    {       // Generates a map of cities for each player at the start of the game

        List<List<float>> city_map = TerrainUtils.GenerateMap(map_size);

        for(int i = 0; i < player_list.Count; i++){
            Vector3 random_coor = TerrainUtils.RandomVector3(map_size);
            while(water_map[(int) random_coor.x][(int) random_coor.z] == (int) EnumHandler.LandType.Water){ // If random_coor is water, generate new random_coor
                random_coor = TerrainUtils.RandomVector3(map_size);
            }

            ClearSpaceForCapital(random_coor, city_map, feature_map, resource_map); // Clear space for capital
        }

        this.city_map = city_map;
    }


    public void InitializeCapitalCities(List<Player> player_list, List<HexTile> hex_list){  //Creates City objects, assigns city object to player object
        int player_index = 0;
        for(int i = 0; i < city_map.Count; i++){
            for(int j = 0; j < city_map.Count; j++){
                if(city_map[i][j] == (int) EnumHandler.StructureType.Capital){
                    City city = new City("Error", player_list[player_index].GetId());
                    capitals_list.Add(city);
                    player_list[player_index].AddCity(city); // Add city to player
                    player_index++;
                }
            }
        }

        HexTileUtils.SetStructureType(city_map, hex_list);
    }

    private void ClearSpaceForCapital(Vector3 random_coor, List<List<float>> city_map, List<List<float>> feature_map, List<List<float>> resource_map){
            city_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.StructureType.Capital;
            feature_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.HexNaturalFeature.None;
            resource_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.HexResource.None;
    }

    public void SpawnCapitals(List<HexTile> hex_list)   // Spawns all capitals into GameWorld
    {
        
        foreach(HexTile hex in hex_list){

            GameObject hex_object = TerrainHandler.hex_to_hex_go[hex];  // Get hex GameObject from hex_to_hex_go Dictionary
            GameObject structure_go = null; // GameObject to be spawned (Capital)

            if(hex.GetStructureType() == EnumHandler.StructureType.Capital){    // If hex is tagged as a capital, spawn capital
                structure_go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/City"));
                city_go_to_city.Add(structure_go, capitals_list[counter]);
                counter++;
            }
            if(structure_go != null){   // If structure_go is not null, spawn structure_go
                city_go_to_city[structure_go].SetName(GetCityName(hex)); // Set city name

                structure_go.transform.SetParent(hex_object.transform); // Set parent to hex_object --> Spawn structure on hex_game_object
                structure_go.transform.localPosition = new Vector3(0, 0, 0);
                structure_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = city_go_to_city[structure_go].GetName();
                structure_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = GameManager.player_id_to_player[city_go_to_city[structure_go].GetPlayerId()].GetOfficialName();
                
            }
        
        }
    }


    private string GetCityName(HexTile hex){
        List<string> cityNames = IOHandler.ReadCityNames("C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\CityNames.xml", hex.GetRegionType().ToString());
        int random_pick = UnityEngine.Random.Range(0, cityNames.Count);
        string name = cityNames[random_pick];
        return name;
    }

}
