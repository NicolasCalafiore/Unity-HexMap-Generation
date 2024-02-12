using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Character;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using UnityEngine;


public class GameManager: MonoBehaviour{

    private FogManager fog_manager = new FogManager();
    private HexManager hex_manager = new HexManager();
    private UIManager ui_manager = new UIManager(); //TO DO: RE-IMPLEMENT
    private TerrainGameHandler terrain_manager = new TerrainGameHandler();
    private PlayerManager player_manager = new PlayerManager();
    private CityManager city_manager = new CityManager();
    private MapGeneration map_generation = new MapGeneration();
    private CharacterManager character_manager = new CharacterManager();

    void Start(){

        GameGeneration(); 

        MapDependantHexInitialization();

        HexDependantHexInitialization();  

        SpawnGameObjects();

        InitializeGameWorld();


    }
    void GameGeneration(){
        player_manager.GeneratePlayers();
        map_generation.GenerateTerrainMaps();
        map_generation.GenerateCitiesMaps(player_manager);
        player_manager.GenerateGovernments();
        city_manager.GenerateCapitalCityObjects(player_manager, map_generation);
        character_manager.GenerateGovernmentsCharacters(map_generation, player_manager); 
    }

    void MapDependantHexInitialization(){
        hex_manager.GenerateHexList(map_generation, hex_manager);
        fog_manager.InitializePlayerFogOfWar(player_manager, map_generation); 
    }

    void HexDependantHexInitialization(){
        map_generation.GenerateTerritoryMaps(player_manager);
        map_generation.GenerateShores(hex_manager); 
        player_manager.SetStateName(hex_manager); 
        hex_manager.SetHexDecorators(); 
        city_manager.SetCityTerritory(hex_manager, map_generation);
    }

    void SpawnGameObjects(){
        terrain_manager.SpawnTerrain(hex_manager);
        terrain_manager.SpawnStructures(hex_manager, city_manager);
        fog_manager.ShowFogOfWar(player_manager, map_generation);
    }

    private void InitializeGameWorld()
    {
        foreach(Player i in player_manager.GetPlayerList()){
            i.GovernmentSimulation(map_generation);
        }

        CameraMovement.CenterCamera(player_manager, fog_manager, map_generation, hex_manager);
        TerrainGameHandler.SpawnAIFlags(player_manager);
    }

    



    void Update(){

    }

}