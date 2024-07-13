using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Character;
using Cities;
using Diplomacy;
using Graphics;
using Players;
using Terrain;
using UnityEngine;


public class GameManagers: MonoBehaviour{
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
        CityManager.SetCityToHexDictionary();
    }

    void SpawnGameObjects(){
        GraphicsManager.Initialize();
        GraphicsManager.SpawnTerrain();
        GraphicsManager.SpawnStructures();
    }

    void InitializeGameWorld()
    {
        PlayerManager.AllScanForNewPlayers();
        TraitManager.GenerateCharacterTraits();
        DiplomacyManager.GenerateStartingRelationships();
        PlayerManager.SimulateGovernments();
        UIManager.FindUIComponents();
        PlayerManager.SetPlayerView(PlayerManager.player_list[1]);//
        FogManager.ShowFogOfWar(); //
        GraphicsManager.SpawnAIFlags();
        TerrainManager.GenerateHexAppeal();
        PlayerManager.InitializePlayerPriorities();
        GraphicsManager.UpdateAllPlayerUIPriorities();  //Used for cities. Will need to refactor
    }

    



    void Update(){

    }

}