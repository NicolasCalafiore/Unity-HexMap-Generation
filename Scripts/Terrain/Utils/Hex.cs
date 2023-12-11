using UnityEngine;
using Terrain;

namespace Terrain {
    public class Hex {
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
        public readonly int Q;  //Column
        public readonly int R;  //Row
        public readonly int S;
        public float E; //Elevation
        public TerrainUtils.HexElevation elevation_type;
        public TerrainUtils.HexRegion region_type;
        public TerrainUtils.LandType land_type;

        public Hex(int q, int r)
        {
            this.Q = q;
            this.R = r;
            this.S = -(q + r);
        }
        public void SetElevation(float elevation, TerrainUtils.HexElevation elevation_type)
        {
            this.E = elevation / 100;
            this.elevation_type = elevation_type;
        }

        public Vector2 GetColRow()
        {
            return new Vector2(this.Q, this.R);
        }

        public void SetLandType(TerrainUtils.LandType land_type){
            this.land_type = land_type;

            if(land_type == TerrainUtils.LandType.Water){
                this.E = -0.3f;
            }

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

        public void SetRegionType(TerrainUtils.HexRegion region_type){
            this.region_type = region_type;
        }

        public TerrainUtils.HexRegion GetRegionType(){
            return this.region_type;
        }

        public TerrainUtils.HexElevation GetElevationType(){
            return this.elevation_type;
        }

        public TerrainUtils.LandType GetLandType(){
            return this.land_type;
        }

    }
}