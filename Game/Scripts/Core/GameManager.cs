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

    public static FogManager fog_manager = new FogManager();
    public static UIManager ui_manager = new UIManager(); //TO DO: RE-IMPLEMENT
    public static TerrainManager terrain_manager = new TerrainManager();
    public static MapManager map_manager = new MapManager();
    public static CharacterManager character_manager = new CharacterManager();

    void Start(){

        GameGeneration(); 

        MapDependantHexInitialization();

        HexDependantHexInitialization();  

        SpawnGameObjects();

        InitializeGameWorld();


    }
    void GameGeneration(){
        Player.GeneratePlayers();
        map_manager.GenerateTerrainMaps();
        map_manager.GenerateCitiesMaps();
        Player.GenerateGovernments();
        City.GenerateCapitalCityObjects(map_manager);
        character_manager.GenerateGovernmentsCharacters(map_manager); 
    }

    void MapDependantHexInitialization(){
        HexTile.GenerateHexList(map_manager);
        fog_manager.InitializePlayerFogOfWar(map_manager); 
    }

    void HexDependantHexInitialization(){
        map_manager.GenerateTerritoryMaps();
        map_manager.GenerateShores(); 
        Player.SetStateNames(); 
        HexTile.SetHexDecorators(); 
        City.SetCityTerritory(map_manager);
    }

    void SpawnGameObjects(){
        terrain_manager.SpawnTerrain();
        terrain_manager.SpawnStructures();
    }

    private void InitializeGameWorld()
    {
        foreach(Player i in Player.GetPlayerList()){        
            i.GovernmentSimulation(map_manager);
        }

        Player.SetPlayerView(0);
        fog_manager.ShowFogOfWar(map_manager);
        CameraMovement.CenterCamera(fog_manager, map_manager);
        TerrainManager.SpawnAIFlags();
    }

    



    void Update(){

    }

}