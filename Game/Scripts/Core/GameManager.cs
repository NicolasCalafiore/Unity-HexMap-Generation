using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Character;
using Cities;
using Players;
using Cities;
using Terrain;
using UnityEngine;


public class GameManager: MonoBehaviour{
    void Start(){

        GameGeneration(); 

        MapDependantHexInitialization();

        HexDependantHexInitialization();  

        SpawnGameObjects();

        InitializeGameWorld();
    }
    void GameGeneration(){
        PlayerManager.GeneratePlayers();
        MapManager.GenerateTerrainMaps();
        MapManager.GenerateCitiesMaps();
        PlayerManager.GenerateGovernments();
        CityManager.GenerateCapitalCityObjects();
        CharacterManager.GenerateGovernmentsCharacters(); 
    }

    void MapDependantHexInitialization(){
        HexManager.GenerateHexList();
        FogManager.InitializePlayerFogOfWar(); 
    }

    void HexDependantHexInitialization(){
        MapManager.GenerateTerritoryMaps();
        MapManager.GenerateShores(); 
        PlayerManager.SetStateNames(); 
        HexManager.SetHexDecorators(); 
        CityManager.SetCityTerritory();
        CityManager.SetRegionTypes();
    }

    void SpawnGameObjects(){
        TerrainManager.SpawnTerrain();
        TerrainManager.SpawnStructures();
    }

    void InitializeGameWorld()
    {
        PlayerManager.AllScanForNewPlayers();
        TraitManager.GenerateCharacterTraits();
        PlayerManager.SimulateGovernments();                   // PlayerManager.AllScanForNewPlayers(); is called twice now. Relies on traits for foreign. Traits rely on known_players
        PlayerManager.SetPlayerView(PlayerManager.player_list[1]);
        FogManager.ShowFogOfWar();
        CameraMovement.CenterCamera();
        TerrainManager.SpawnAIFlags();
        UIManager.InitializeUI();
    }

    



    void Update(){

    }

}