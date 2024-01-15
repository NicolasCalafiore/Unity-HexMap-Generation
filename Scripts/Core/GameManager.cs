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

    [SerializeField] public int player_count;
    [SerializeField] private int elevation_strategy;
    [SerializeField] private int land_strategy;
    [SerializeField] private int region_strategy;
    [SerializeField] private int features_strategy;
    [SerializeField] private int capital_strategy;


    public static Vector2 map_size = new Vector2(128, 128);
    public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
    public static TerrainHandler terrain_manager;
    public static PlayerManager player_manager;
    public static CityManager city_manager;
    public static MapGeneration map_generation;
    public static TerritoryManager territory_manager;
    public static FogManager fog_manager;
    public static HexFactory hex_factory;
    public static UIManager ui_manager;
    public static int player_view;
    public static List<HexTile> hex_list = new List<HexTile>();   // All HexTile objects

    void Start(){

        InitializeCoreComponents(); // Initializes Core Game Classes

        GameGeneration();   // Generates all maps, hex_list, and cities in List<List<float>> format

        MapDependanHextInitialization();  // Sets all HexTile properties based off of List<List<float>> maps (float to float)

        HexDependantHexInitialization();    // Sets all HexTile properties based off of other HexTile properties (HexTile to HexTile)

        SpawnGameObjects(); // Spawns all GameObjects into GameWorld

        CameraMovement.CenterCamera();



    }

    void InitializeCoreComponents(){
        terrain_manager = new TerrainHandler();
        player_manager = new PlayerManager();
        city_manager = new CityManager();
        map_generation = new MapGeneration(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy);
        territory_manager = new TerritoryManager();
        fog_manager = new FogManager(map_size); 
        hex_factory = new HexFactory();
        ui_manager = new UIManager();
    }

    void GameGeneration(){
        player_manager.GeneratePlayers(player_count);
        map_generation.GenerateTerrainMap();
        city_manager.GenerateStructureMap(map_generation.water_map, player_manager.player_list, map_size, map_generation.features_map, map_generation.resource_map, capital_strategy);
    }

    void MapDependanHextInitialization(){
        city_manager.InitializeCapitalCityObjects(player_manager.player_list);
        fog_manager.InitializePlayerFogOfWar(player_manager.player_list, map_size); 
        hex_list = HexTileUtils.GenerateHexList(map_size, map_generation, city_manager, territory_manager, hex_factory);
    }

    void HexDependantHexInitialization(){
        territory_manager.GenerateCapitalTerritory(city_manager.structure_map, player_manager.player_list, map_size);
        map_generation.InitializeBorderEffects(hex_list);
        player_manager.SetStateName(hex_list); // Needs Characteristics to be applied to apply region-based names
        DecoratorHandler.SetHexDecorators(hex_list);
        city_manager.SetCityTerritory(hex_list, hex_factory, map_size);
    }

    void SpawnGameObjects(){
        terrain_manager.SpawnTerrain(map_size, hex_list);
        terrain_manager.SpawnStructures(hex_list, CityManager.capitals_list, city_manager);
        fog_manager.ShowFogOfWar(player_manager.player_list[player_view], map_size);
    }



    void Update(){

    }

}