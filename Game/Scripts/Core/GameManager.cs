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
    void Start(){

        GameGeneration(); 

        MapDependantHexInitialization();

        HexDependantHexInitialization();  

        SpawnGameObjects();

        InitializeGameWorld();
    }
    void GameGeneration(){
        Player.GeneratePlayers();
        MapManager.GenerateTerrainMaps();
        MapManager.GenerateCitiesMaps();
        Player.GenerateGovernments();
        City.GenerateCapitalCityObjects();
        CharacterManager.GenerateGovernmentsCharacters(); 
    }

    void MapDependantHexInitialization(){
        HexTile.GenerateHexList();
        FogManager.InitializePlayerFogOfWar(); 
    }

    void HexDependantHexInitialization(){
        MapManager.GenerateTerritoryMaps();
        MapManager.GenerateShores(); 
        Player.SetStateNames(); 
        HexTile.SetHexDecorators(); 
        City.SetCityTerritory();
        City.SetRegionTypes();
    }

    void SpawnGameObjects(){
        TerrainManager.SpawnTerrain();
        TerrainManager.SpawnStructures();
    }

    void InitializeGameWorld()
    {
        Player.AllScanForNewPlayers();
        CharacterManager.GenerateCharacterTraits();
        Player.SimulateGovernments();                   // Player.AllScanForNewPlayers(); is called twice now. Relies on traits for foreign. Traits rely on known_players
        Player.SetPlayerView(Player.GetPlayerList()[0], false);
        FogManager.ShowFogOfWar();
        CameraMovement.CenterCamera();
        TerrainManager.SpawnAIFlags();
    }

    



    void Update(){

    }

}