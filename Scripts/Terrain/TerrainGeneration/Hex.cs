using UnityEngine;
using static TerrainGeneration.ElevationUtils;

namespace TerrainGeneration{
    public class Hex {
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
        public readonly int Q;  //Column
        public readonly int R;  //Row
        public readonly int S;
        public float E; //Elevation
        public HexElevationTypes elevationType;


        public Hex(int q, int r)
        {
            this.Q = q;
            this.R = r;
            this.S = -(q + r);
        }
        public void SetElevation(float elevation, HexElevationTypes elevationType)
        {
            this.elevationType = elevationType;
            this.E = elevation;
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

    }
}