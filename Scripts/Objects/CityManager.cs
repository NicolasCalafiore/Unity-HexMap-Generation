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

    public void GenerateCityMap(List<List<float>> water_map, List<Player> player_list, List<List<float>> features_map, Vector2 map_size)
    {
        List<List<float>> city_map = TerrainUtils.GenerateMap(map_size);
        foreach(Player player in player_list){
            Vector3 random_coor = TerrainUtils.RandomVector3(map_size);
            while(water_map[(int) random_coor.x][(int) random_coor.z] == (int) EnumHandler.LandType.Water || features_map[(int) random_coor.x][(int) random_coor.z] != (int) EnumHandler.HexNaturalFeature.None){
                random_coor = TerrainUtils.RandomVector3(map_size);
            }
            city_map[ (int) random_coor.x][ (int) random_coor.z] = (int) EnumHandler.StructureType.Capital;
        }
        this.city_map = city_map;
    }

    public void SetCapitalCities(List<Player> player_list, List<HexTile> hex_list){

        DebugHandler.PrintMapDebug("City Map", city_map);
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

        foreach(HexTile hex in hex_list){
            GameObject hex_object = TerrainHandler.hex_to_hex_go[hex];
            SpawnCapital(hex, hex_object);
        }
    }

    
    static List<string> ReadXml(string filePath, string region)
    {
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("Region")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("City")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }

    private void SpawnCapital(HexTile hex, GameObject hex_object)
    {
        GameObject structure_go = null;
        if(hex.GetStructureType() == EnumHandler.StructureType.Capital){
            structure_go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/City"));
            city_go_to_city.Add(structure_go, capitals_list[counter]);
            counter++;
        }
        if(structure_go != null){
            List<string> cityNames = ReadXml("C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\CityNames.xml", hex.GetRegionType().ToString());
            int random_pick = UnityEngine.Random.Range(0, cityNames.Count);
            string name = cityNames[random_pick];
            city_go_to_city[structure_go].SetName(name);

            structure_go.transform.SetParent(hex_object.transform);
            structure_go.transform.localPosition = new Vector3(0, 0, 0);
            structure_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = city_go_to_city[structure_go].GetName();
        }
    }


}
