using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public Domestic(List<string> names, EnumHandler.CharacterGender gender){
            this.first_name = names[0];
            this.last_name = names[1];
            this.gender = gender;
            this.character_type = EnumHandler.CharacterType.Advisor;

        }


        public void IdentifyConstructionOppruntities(List<List<float>> territory_map, int player_id){

            for(int i = 0; i < territory_map.Count; i++){
                for(int j = 0; j < territory_map[i].Count; j++){

                    if(territory_map[i][j] == player_id){  //If you can see the HexTile
                        HexTile hex_tile = HexManager.col_row_to_hex[new Vector2Int(i, j)];
                        if(hex_tile.resource_type != EnumHandler.HexResource.None){
                            potential_construction_tiles.Add(hex_tile);
                        }
                        
                    } 

                    
                }
            }
        
        }

        public void IdentifyExpansionOpportuntiies(List<List<float>> fog_of_war, int player_id, List<List<float>> territory_map){

            for(int i = 0; i < fog_of_war.Count; i++){
                for(int j = 0; j < fog_of_war[i].Count; j++){

                    if(fog_of_war[i][j] == 1 && territory_map[i][j] != player_id){  //If you can see the HexTile
                        HexTile hex_tile = HexManager.col_row_to_hex[new Vector2Int(i, j)];
                        if(hex_tile.resource_type != EnumHandler.HexResource.None){
                            potential_expansion_tiles.Add(hex_tile);
                        }
                        
                    } 

                    
                }
            }
        
        }

        public List<HexTile> GetPotentialConstructionTiles(){
            return potential_construction_tiles;
        }

        public List<HexTile> GetPotentialExpansionTiles(){
            return potential_expansion_tiles;
        }


        
    }

}