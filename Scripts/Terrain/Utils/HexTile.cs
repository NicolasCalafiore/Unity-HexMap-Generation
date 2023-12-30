using UnityEngine;
using Terrain;

namespace Terrain {
    public class HexTile {
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
        public readonly int Q;  //Column
        public readonly int R;  //Row
        public readonly int S;
        public float E; //Elevation
        public EnumHandler.HexElevation elevation_type { get; set;}
        public EnumHandler.HexRegion region_type;
        public EnumHandler.LandType land_type;
        public EnumHandler.HexNaturalFeature feature_type;
        public virtual float MovementCost { get; set; } = 1.0f; // Default movement cost

        public HexTile(int q, int r)
        {
            this.Q = q;
            this.R = r;
            this.S = -(q + r);
        }
        public void SetElevation(float elevation, EnumHandler.HexElevation elevation_type)
        {
            this.E = elevation / 100;
            this.elevation_type = elevation_type;
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

        public Vector2 GetColRow()
        {
            return new Vector2(this.Q, this.R);
        }

        public Vector3 GetPosition()
        {  
            float radius = 1f;
            float height = radius * 2;
            float width = WIDTH_MULTIPLIER * height;

            float vert = height * 0.75f;
            float horiz = width;

            return new Vector3(
                horiz * (this.Q + this.R/2f),
                E,
                vert * this.R
            );


        }

        public EnumHandler.HexRegion GetRegionType(){
            return this.region_type;
        }

        public EnumHandler.HexElevation GetElevationType(){
            return this.elevation_type;
        }

        public EnumHandler.LandType GetLandType(){
            return this.land_type;
        }

        public EnumHandler.HexNaturalFeature GetFeatureType(){
            return this.feature_type;
        }

    }
}