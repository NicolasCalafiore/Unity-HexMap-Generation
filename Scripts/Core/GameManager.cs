using System;
using System.Collections;
using Terrain;
using UnityEngine;


public class GameManager: MonoBehaviour{
    [SerializeField] private Vector2 map_size = new Vector2();
    [SerializeField] private int elevation_strategy;
    [SerializeField] private int land_strategy;
    [SerializeField] private int region_strategy;
    [SerializeField] private int features_strategy;
    [SerializeField] public GameObject perlin_map_object;
    [SerializeField] public int player_count;
    MapGeneration map_generation;
    TurnManager turn_manager;
    PlayerManager player_manager;
    TerrainHandler terrain_handler;

    void Start(){
        terrain_handler = new TerrainHandler();
        player_manager = new PlayerManager();

        map_generation = new MapGeneration(map_size, elevation_strategy, land_strategy, region_strategy, features_strategy, terrain_handler);
        //terrain_handler.TerrainSpawned += OnTerrainSpawned;

        player_manager.GeneratePlayers(player_count);
        map_generation.GenerateTerrain(player_manager.player_list);
    }

    // public void OnTerrainSpawned(object sender, EventArgs e){
        
    // }

}