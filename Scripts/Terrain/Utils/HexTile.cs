using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System;

namespace Terrain {
    public class HexTile {

        /*
            Used to store all HexTile properties
            Is wrap around any HexTile decorators
        */

        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2; // Used to calculate HexTile position
        public float food { get; set; }

        public float production { get; set; }
        public readonly int column;  //Column
        public readonly int row;  //Row
        private readonly int S;  // -(column + row)
        public float elevation; //Elevation


        private EnumHandler.HexElevation elevation_type { get; set;}
        private EnumHandler.HexRegion region_type;  //Region
        private EnumHandler.LandType land_type; //Land or Water
        private EnumHandler.HexNaturalFeature feature_type; //Natural Feature
        private EnumHandler.HexResource resource_type;  //Resource
        private EnumHandler.StructureType structure_type;   //Structures
        public virtual float MovementCost { get; set; } = 1.0f; // Default movement costs

        public HexTile(int column, int row)
        {
            this.column = column;
            this.row = row;
            this.S = -(column + row);
        }
        public void SetElevation(EnumHandler.HexElevation elevation_type)
        {
            this.elevation = (float) elevation_type / 100;
            this.elevation_type = elevation_type;
        }

        public void SetStructureType(EnumHandler.StructureType structure_type){
            this.structure_type = structure_type;
        }

        public void SetFeatureType(EnumHandler.HexNaturalFeature feature_type){
            this.feature_type = feature_type;
        }

        public void SetLandType(EnumHandler.LandType land_type){
            this.land_type = land_type;
        }
        public void SetRegionType(EnumHandler.HexRegion region_type){
                this.region_type = region_type;
        }

        public void SetResourceType(EnumHandler.HexResource resource_type){
            this.resource_type = resource_type;
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

        public EnumHandler.HexRegion GetRegionType(){
            return this.region_type;
        }

        public EnumHandler.HexResource GetResourceType(){
            return this.resource_type;
        }

        public EnumHandler.HexElevation GetElevationType(){
            return this.elevation_type;
        }

        public EnumHandler.LandType GetLandType(){
            return this.land_type;
        }

        public EnumHandler.StructureType GetStructureType(){
            return this.structure_type;
        }

        public EnumHandler.HexNaturalFeature GetFeatureType(){
            return this.feature_type;
        }

        internal City GetCity(int v)
        {
            throw new NotImplementedException();
        }
    }
}