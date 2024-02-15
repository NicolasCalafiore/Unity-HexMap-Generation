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
    }

    void SpawnGameObjects(){
        TerrainManager.SpawnTerrain();
        TerrainManager.SpawnStructures();
    }

    private void InitializeGameWorld()
    {
        Player.SimulateGovernments();
        Player.SetPlayerView(0);
        FogManager.ShowFogOfWar();
        CameraMovement.CenterCamera();
        TerrainManager.SpawnAIFlags();
    }

    



    void Update(){

    }

}