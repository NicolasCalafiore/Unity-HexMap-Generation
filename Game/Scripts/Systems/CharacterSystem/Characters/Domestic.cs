using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using UnityEngine;


namespace Cabinet{
    public class Domestic : ICharacter
    {
        private List<HexTile> potential_construction_tiles = new List<HexTile>();
        private List<HexTile> potential_expansion_tiles = new List<HexTile>();


        public Domestic(List<string> names, CharacterEnums.CharacterGender gender, Player player, List<string> titles){
            this.first_name = names[0];
            this.last_name = names[1];
            this.gender = gender;
            this.character_type = CharacterEnums.CharacterType.Advisor;
            this.owner_player = player;
            this.title = titles[UnityEngine.Random.Range(0, titles.Count)];
        }


        public void IdentifyConstructionOpportunities(List<List<float>> territory_map, int player_id){

            potential_construction_tiles.AddRange(
                territory_map.SelectMany((row, i) => row.Select((col, j) => new {i, j}))
                            .Where(coord => IsConstructionOpportunity(coord.i, coord.j, player_id, territory_map))
                            .Select(coord => HexTile.col_row_to_hex[new Vector2Int(coord.i, coord.j)]));
        }

        public void IdentifyExpansionOpportuntiies(List<List<float>> fog_of_war, int player_id, List<List<float>> territory_map){

            potential_expansion_tiles.AddRange(
                territory_map.SelectMany((row, i) => row.Select((col, j) => new {i, j}))
                            .Where(coord => IsExpansionOpportunity(coord.i, coord.j, player_id, fog_of_war, territory_map))
                            .Select(coord => HexTile.col_row_to_hex[new Vector2Int(coord.i, coord.j)]));
        
        }

        public List<HexTile> GetPotentialConstructionTiles(){
            return potential_construction_tiles;
        }

        public List<HexTile> GetPotentialExpansionTiles(){
            return potential_expansion_tiles;
        }


        private bool IsConstructionOpportunity(int i, int j, int player_id, List<List<float>> territory_map){
            HexTile hex_tile = HexTile.col_row_to_hex[new Vector2Int(i, j)];

            if(territory_map[i][j] != player_id) return false;
            if(hex_tile.GetResourceType() == ResourceEnums.HexResource.None) return false;

            return true;
        }

        private bool IsExpansionOpportunity(int i, int j, int player_id, List<List<float>> fog_of_war, List<List<float>> territory_map){
            HexTile hex_tile = HexTile.col_row_to_hex[new Vector2Int(i, j)];

            if(fog_of_war[i][j] == (int) FogEnums.FogType.Undiscovered) return false;
            if(territory_map[i][j] != -1) return false;
            if(territory_map[i][j] == player_id) return false;
            if(hex_tile.GetResourceType() == ResourceEnums.HexResource.None) return false;
          
            return true;
        }
        
    }

}