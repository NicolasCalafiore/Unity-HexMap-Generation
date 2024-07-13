using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Cities;
using Terrain;
using TMPro;
using UnityEngine;


namespace Cabinet{
    public class Domestic : AbstractCharacter
    {
        private List<HexTile> potential_construction_tiles = new List<HexTile>();
        private List<HexTile> potential_expansion_tiles = new List<HexTile>();


        public Domestic(List<string> names, CharacterEnums.CharacterGender gender, Player player, List<string> titles)
        : base(names, gender, player, titles){}

        // Identifies potential construction opportunities based on the territory map and player id
        public void IdentifyConstructionOpportunities(Player player){

            List<List<float>> territory_map = MapManager.territory_map_handler.territory_map;

            potential_construction_tiles.AddRange(
                territory_map.SelectMany((row, i) => row.Select((col, j) => new {i, j}))
                            .Where(coord => IsConstructionOpportunity(coord.i, coord.j, player, territory_map))
                            .Select(coord => HexManager.col_row_to_hex[new Vector2Int(coord.i, coord.j)]));
        }

        // Identifies potential expansion opportunities based on the fog of war, territory map and player id
        public void IdentifyExpansionOpportuntiies(List<List<float>> fog_of_war, Player player){
            List<List<float>> territory_map = MapManager.territory_map_handler.territory_map;

            potential_expansion_tiles.AddRange(
                territory_map.SelectMany((row, i) => row.Select((col, j) => new {i, j}))
                            .Where(coord => IsExpansionOpportunity(coord.i, coord.j, player, fog_of_war, territory_map))
                            .Select(coord => HexManager.col_row_to_hex[new Vector2Int(coord.i, coord.j)]));
        
        }

        // Determines if a tile is a potential construction opportunity.
        // A tile is a construction opportunity if it belongs to the player and it has a resource.
        private bool IsConstructionOpportunity(int i, int j, Player player, List<List<float>> territory_map)
        {
            HexTile hex_tile = HexManager.col_row_to_hex[new Vector2Int(i, j)];

            if(territory_map[i][j] != player.id) return false;
            if(hex_tile.resource_type == ResourceEnums.HexResource.None) return false;

            return true;
        }

        // Determines if a tile is a potential expansion opportunity.
        // A tile is an expansion opportunity if it is discovered, not owned by any player, not owned by the current player, and it has a resource.
        private bool IsExpansionOpportunity(int i, int j, Player player, List<List<float>> fog_of_war, List<List<float>> territory_map)
        {
            HexTile hex_tile = HexManager.col_row_to_hex[new Vector2Int(i, j)];

            if(fog_of_war[i][j] == (int) FogEnums.FogType.Undiscovered) return false;
            if(territory_map[i][j] != -1) return false;
            if(territory_map[i][j] == player.id) return false;
            if(hex_tile.resource_type == ResourceEnums.HexResource.None) return false;
          
            return true;
        }

        
        public List<HexTile> GetPotentialConstructionTiles() => potential_construction_tiles;

        public List<HexTile> GetPotentialExpansionTiles() => potential_expansion_tiles;

        
    }

}