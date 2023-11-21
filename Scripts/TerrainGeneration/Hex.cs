using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex {
    public readonly int Q;  //Column
    public readonly int R;  //Row
    public readonly int S;
    public float E; //Elevation

    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    public void SetElevation(float elevation)
    {
        this.E = elevation;
    }

    public Vector2 GetColRow()
    {
        return new Vector2(this.Q, this.R);
    }

    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

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
