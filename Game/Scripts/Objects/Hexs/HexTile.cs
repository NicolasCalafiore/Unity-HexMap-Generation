using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System;
using Players;

namespace Terrain {
    public class HexTile {

        /*
            Used to store all HexTile properties
            Is wrap around any HexTile decorators
        */

        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2; // Used to calculate HexTile position
        public float nourishment { get; set; }

        public float construction { get; set; }

        public readonly int column;  //Column
        public readonly int row;  //Row
        private readonly int S;  // -(column + row)
        public float elevation; //Elevation

        public float gCost;
        public float hCost;
        public float fCost { get { return gCost + hCost; } }
        public bool is_walakble = true;


        public EnumHandler.HexElevation elevation_type { get; set;}
        public EnumHandler.HexRegion region_type;  //Region
        public EnumHandler.LandType land_type; //Land or Water
        public EnumHandler.HexNaturalFeature feature_type; //Natural Feature
        public EnumHandler.HexResource resource_type;  //Resource
        public EnumHandler.StructureType structure_type;   //Structures
        public Player owner_player;   
        public City owner_city;
        private bool is_coast = false;
        public virtual float MovementCost { get; set; } = 1.0f; // Default movement costs

        public HexTile(int column, int row)
        {
            this.column = column;
            this.row = row;
            this.S = -(column + row);
        }
        public HexTile SetElevation(EnumHandler.HexElevation elevation_type)
        {
            this.elevation = (float) elevation_type / 100;
            this.elevation_type = elevation_type;
            return this;
        }

        public HexTile SetStructureType(EnumHandler.StructureType structure_type){
            this.structure_type = structure_type;
            return this;
        }

        public HexTile SetFeatureType(EnumHandler.HexNaturalFeature feature_type){
            this.feature_type = feature_type;
            return this;
        }

        public HexTile SetLandType(EnumHandler.LandType land_type){
            this.land_type = land_type;
            return this;
        }
        public HexTile SetRegionType(EnumHandler.HexRegion region_type){
            this.region_type = region_type;
            return this;
        }

        public HexTile SetResourceType(EnumHandler.HexResource resource_type){
            this.resource_type = resource_type;
            return this;
        }

        public HexTile SetOwnerPlayer(Player owner_player){
            this.owner_player = owner_player;
            return this;
        }
        public HexTile SetOwnerCity(City owner_city){
            this.owner_city = owner_city;
            return this;
        }




        
        public Vector2 GetColRow()
        {
            return new Vector2(this.column, this.row);
        }

        public Vector3 GetPosition()
        {  
            float radius = 1f;
            float height = radius * 2;
            float width = WIDTH_MULTIPLIER * height;

            float vert = height * 0.75f;
            float horiz = width;

            return new Vector3(
                horiz * (this.column + this.row/2f),
                elevation,
                vert * this.row
            );
        }

        public void SetCoast(){
            is_coast = true;
            elevation = elevation < 0 ? 0 : elevation;
        }

        public bool IsCoast(){
            return is_coast;
        }

    }
}