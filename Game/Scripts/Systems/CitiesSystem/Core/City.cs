using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Terrain;
using UnityEngine;
using static Terrain.RegionsEnums;

namespace Cities
{
    public class City
    {
        public List<HexTile> hex_territory_list = new List<HexTile>();
        public Vector2 col_row {get; set;}
        public HexRegion region_type {get; set;}
        public string name {get; set;}
        public Player owner_player {get; set;}
        public float inhabitants {get; set;} = 500;
        public float stability {get; set;} = 50;
        public float nourishment {get; set;} = 50;
        public float construction {get; set;} = 50;

        public City(string name, Player player, Vector2 col_row){
            this.name = name;
            this.owner_player = player;
            this.col_row = col_row;
        }

        public void CalculateCityNourishment(TerritoryManager territory_manager) =>
            nourishment += territory_manager.CalculateCityNourishment(this);
    
        public void CalculateCityConstruction(TerritoryManager territory_manager) => 
            construction += territory_manager.CalculateCityConstruction(this);
        
        public List<HexTile> GetHexTerritoryList() => hex_territory_list;

    }
}