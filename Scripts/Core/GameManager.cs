using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Players;
using Terrain;
using UnityEngine;


public class GameManager: MonoBehaviour{

    /*
        GameManager is used to manage the game
    */
    [SerializeField] private Vector2 map_size = new Vector2();
    [SerializeField] private int elevation_strategy;
    [SerializeField] private int land_strategy;
    [SerializeField] private int region_strategy;
    [SerializeField] private int features_strategy;
    [SerializeField] public GameObject perlin_map_object;
    [SerializeField] public int player_count;
    public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
    MapGeneration map_generation;
    TurnManager turn_manager;
    PlayerManager player_manager;
    TerrainHandler terrain_handler;
    CityManager city_manager;

    void Start(){
        terrain_handler = new TerrainHandler();
        player_manager = new PlayerManager();
        city_manager = new CityManager();

        map_generation = new MapGeneration(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy, terrain_handler);
        //terrain_handler.TerrainSpawned += OnTerrainSpawned;

        player_manager.GeneratePlayers(player_count);
        map_generation.GenerateTerrain(player_manager.player_list);
        city_manager.GenerateCityMap(map_generation.water_map, player_manager.player_list, map_generation.features_map, map_size);
        city_manager.SetCapitalCities(player_manager.player_list, map_generation.hex_list);

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

}