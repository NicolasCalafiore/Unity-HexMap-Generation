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
        GameManager is used to manage the game - Main entrypoint for all scripts.
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

        InitializeCoreComponents(); // Initializes TerrainHandler.cs, PlayerManager.cs, CityManager.cs, MapGeneration.cs +

        GameGeneration();   // Generates all maps, hex_list, and cities in List<List<float>> format

        HexTraitsInitializaiton();  // Sets all HexTile properties based off of List<List<float>> maps

        SpawnGameObjects(); // Spawns all GameObjects into GameWorld
    }


    void InitializeCoreComponents(){
        terrain_handler = new TerrainHandler();
        player_manager = new PlayerManager();
        city_manager = new CityManager();
        map_generation = new MapGeneration(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy);
    }

    void GameGeneration(){
        player_manager.GeneratePlayers(player_count);
        map_generation.GenerateTerrain();
        city_manager.GenerateCityMap(map_generation.water_map, player_manager.player_list, map_size, map_generation.features_map, map_generation.resource_map);

    }

    void HexTraitsInitializaiton(){
        city_manager.InitializeCapitalCities(player_manager.player_list, map_generation.hex_list);
        map_generation.ApplyHexCharacteristics();
        DecoratorHandler.SetHexDecorators(map_generation.hex_list);   
    }

    void SpawnGameObjects(){
        terrain_handler.SpawnTerrain(map_size, map_generation.hex_list);
        city_manager.SpawnCapitals(map_generation.hex_list);
    }
}