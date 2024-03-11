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
        public virtual float MovementCost { get; set; } = 1.0f; 
        public float nourishment { get; set; }
        public float construction { get; set; }
        public int column; 
        public int row;  
        protected readonly int S; 
        private readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2; // Used to calculate HexTile position


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
                (int) elevation_type/100,
                vert * this.row
            );
        }

        public void SetCoast(){
            is_coast = true;
            elevation_type = elevation_type < 0 ? 0 : elevation_type;
        }

        // HexTiles 2D position in World map
        public Vector2 GetColRow() => new Vector2(this.column, this.row);
    }
}