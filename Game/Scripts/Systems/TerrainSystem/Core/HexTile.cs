using UnityEngine;
using Terrain;
using System;
using Players;
using System.Collections.Generic;
using Character;
using Cities;

namespace Terrain {
    public class HexTile {
        public ElevationEnums.HexElevation elevation_type { get; set;}
        public RegionsEnums.HexRegion region_type { get; set;}  
        public LandEnums.LandType land_type { get; set;} 
        public FeaturesEnums.HexNaturalFeature feature_type { get; set;} 
        public ResourceEnums.HexResource resource_type { get; set;}  
        public StructureEnums.StructureType structure_type { get; set;} 
        public Player owner_player { get; set;}   
        public City owner_city { get; set;}
        public bool is_coast { get; set;} = false;
        public float continent_id;
        public float culture_id;
        public virtual float MovementCost { get; set; } = 1.0f; 
        public float nourishment { get; set; }  = 0;
        public float construction { get; set; }  = 0;
        public float defense { get; set; }  = 0;
        public int appeal { get; set; } = 0;
        public int column; 
        public int row;  
        protected readonly int S; 
        private readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2; // Used to calculate HexTile position
        public int upgrade_level = 0;
        public int max_level = 1;
        


        public HexTile(int column, int row)
        {
            this.column = column;
            this.row = row;
            this.S = -(column + row);
        }
        // HexTiles 3D position in World
        public Vector3 GetWorldPosition()
        {  
            float radius = 1f;
            float height = radius * 2;
            float width = WIDTH_MULTIPLIER * height;

            float vert = height * 0.75f;
            float horiz = width;

            return new Vector3(
                horiz * (this.column + this.row/2f),
                (float) elevation_type/100,
                vert * this.row
            );
        }

        public void SetCoast(){
            is_coast = true;
            elevation_type = elevation_type < 0 ? 0 : elevation_type;
        }

        // HexTiles 2D position in World map
        public Vector2 GetColRow() => new Vector2(this.column, this.row);
        public List<HexTile> GetNeighbors(){

            //public static Dictionary<Vector2, GameObject> col_row_to_hex_go = new Dictionary<Vector2, GameObject>(); 
            // public static Dictionary<HexTile, GameObject> hex_to_hex_go = new Dictionary<HexTile, GameObject>(); 
            List<HexTile> neighbors = new List<HexTile>();
            
            if(this.column > 0){
                GameObject hex_go = GraphicsManager.col_row_to_hex_go[new Vector2(column - 1, row)];
                HexTile hex = GraphicsManager.hex_go_to_hex[hex_go];
                neighbors.Add(hex);
            }
            if(this.column < MapManager.GetMapSize().x - 1){
                GameObject hex_go = GraphicsManager.col_row_to_hex_go[new Vector2(column + 1, row)];
                HexTile hex = GraphicsManager.hex_go_to_hex[hex_go];
                neighbors.Add(hex);
            }
            if(this.row > 0){
                GameObject hex_go = GraphicsManager.col_row_to_hex_go[new Vector2(column, row - 1)];
                HexTile hex = GraphicsManager.hex_go_to_hex[hex_go];
                neighbors.Add(hex);
            }
            if(this.row < MapManager.GetMapSize().y - 1){
                GameObject hex_go = GraphicsManager.col_row_to_hex_go[new Vector2(column, row + 1)];
                HexTile hex = GraphicsManager.hex_go_to_hex[hex_go];
                neighbors.Add(hex);
            }
            if(this.column < MapManager.GetMapSize().x - 1 && this.row > 0){
                // Upper-right diagonal neighbor
                GameObject hex_go = GraphicsManager.col_row_to_hex_go[new Vector2(column + 1, row - 1)];
                HexTile hex = GraphicsManager.hex_go_to_hex[hex_go];
                neighbors.Add(hex);
            }

            if(this.row < MapManager.GetMapSize().y - 1 && this.column > 0){
                // Lower-left diagonal neighbor
                GameObject hex_go = GraphicsManager.col_row_to_hex_go[new Vector2(column - 1, row + 1)];
                HexTile hex = GraphicsManager.hex_go_to_hex[hex_go];
                neighbors.Add(hex);
            }
            
            return neighbors;
        }
    }
}