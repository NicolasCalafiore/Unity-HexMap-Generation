using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using UnityEngine;


public class GameManager: MonoBehaviour{

    /*
        GameManager is used to manage the game - Main entrypoint for all scripts.
    */

    [SerializeField] private int player_view;
    [SerializeField] public TerrainHandler terrain_manager;
    [SerializeField] public PlayerManager player_manager;
    [SerializeField] public CityManager city_manager;
    [SerializeField] public MapGeneration map_generation;
    [SerializeField] public TerritoryManager territory_manager;
    [SerializeField] public FogOfWar fog;
    [SerializeField] public GameObject perlin_map_object;
    [SerializeField] public Vector2 map_size;
    public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
    [SerializeField] public int player_count;
    [SerializeField] private int elevation_strategy;
    [SerializeField] private int land_strategy;
    [SerializeField] private int region_strategy;
    [SerializeField] private int features_strategy;
    [SerializeField] private int capital_strategy;

    void Start(){

        InitializeCoreComponents(); // Initializes TerrainHandler.cs, PlayerManager.cs, CityManager.cs, MapGeneration.cs +

        GameGeneration(player_manager, map_generation, city_manager, terrain_manager);   // Generates all maps, hex_list, and cities in List<List<float>> format

        HexTraitsInitializaiton(city_manager, map_generation, player_manager);  // Sets all HexTile properties based off of List<List<float>> maps

        SpawnGameObjects(terrain_manager, city_manager, map_generation); // Spawns all GameObjects into GameWorld


        FogOfWar fog = new FogOfWar(map_size); // Initializes Fog of War
        fog.InitializePlayerFogOfWar(player_manager.player_list, map_size); // Initializes Fog of War for all players
        terrain_manager.ShowFogOfWar(player_manager.player_list[player_view], map_size);

    }


    void InitializeCoreComponents(){
        terrain_manager = new TerrainHandler();
        player_manager = new PlayerManager();
        city_manager = new CityManager();
        map_generation = new MapGeneration(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy);
        territory_manager = new TerritoryManager();
    }

    void GameGeneration(PlayerManager player_manager, MapGeneration map_generation, CityManager city_manager, TerrainHandler terrain_manager){
        player_manager.GeneratePlayers(player_count);
        map_generation.GenerateTerrain();
        city_manager.GenerateCityMap(map_generation.water_map, player_manager.player_list, map_size, map_generation.features_map, map_generation.resource_map, capital_strategy);
    }

    void HexTraitsInitializaiton(CityManager city_manager, MapGeneration map_generation, PlayerManager player_manager){
        city_manager.InitializeCapitalCityObjects(player_manager.player_list, map_generation.hex_list);
        territory_manager.GenerateCapitalTerritory(city_manager.city_map, player_manager.player_list);
        map_generation.ApplyHexCharacteristics();
        player_manager.SetStateName(map_generation.hex_list);
        DecoratorHandler.SetHexDecorators(map_generation.hex_list);   
    }

    void SpawnGameObjects(TerrainHandler terrain_manager, CityManager city_manager, MapGeneration map_generation ){
        terrain_manager.SpawnTerrain(map_size, map_generation.hex_list);
        terrain_manager.SpawnCapitals(map_generation.hex_list, CityManager.capitals_list, city_manager);
    }


    void Update(){
         if(Input.GetKeyDown(KeyCode.Space)){
            if(player_view >= 0){
                terrain_manager.ShowFogOfWar(player_manager.player_list[player_view], map_size); // Shows Fog of War for all players
                Vector2 coordinates = player_manager.player_list[player_view].GetCity(0).GetColRow();
                HexTile hexTile = map_generation.hex_list[(int) coordinates.x * (int) map_size.y + (int) coordinates.y];
                GameObject hex = TerrainHandler.hex_to_hex_go[hexTile];
                Vector3 vector = hex.transform.position;
                vector.y += 10f;
                vector.z -= 10f;

                CameraMovement.MoveCameraTo(vector);
            }
             else{
                for(int i = 0; i < map_size.x; i++){
                    for(int j = 0; j < map_size.y; j++){
                        terrain_manager.SpawnHexTile(new Vector2(i, j), map_size); // Shows Fog of War for all players
                    }
                }
             }
         }
    }

    public void NextPlayer(){
        player_view += 1;
        terrain_manager.ShowFogOfWar(player_manager.player_list[player_view], map_size); // Shows Fog of War for all players
        Vector2 coordinates = player_manager.player_list[player_view].GetCity(0).GetColRow();
        HexTile hexTile = map_generation.hex_list[(int) coordinates.x * (int) map_size.y + (int) coordinates.y];
        GameObject hex = TerrainHandler.hex_to_hex_go[hexTile];
        Vector3 vector = hex.transform.position;
        vector.y += 10f;
        vector.z -= 10f;

        CameraMovement.MoveCameraTo(vector);
    }
}