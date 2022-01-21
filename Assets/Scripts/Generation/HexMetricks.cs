using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoordinate
{
    public int X { get; private set; }
    public int Z { get; private set; }

    public HexCoordinate(int x, int z)
    {
        X = x;
        Z = z;
    }

    public static HexCoordinate FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinate(x - z / 2, z);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString();
    }

    public static HexCoordinate FromPosition(Vector3 pos)
    {
        float x = pos.x / (HexMetricks.innerRadius * 2);
        float offset = pos.z / (HexMetricks.outerRadius * 3);

        float y = -x - offset;
        x -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
                iZ = -iY - iZ;
            else if (dZ > dY)
                iZ = -iX - iY;
        }

        return new HexCoordinate(iX, iZ);
    }

    public HexCoordinate[] GetNeighbors()
    {
        HexCoordinate[] hexs =
        {
            new HexCoordinate(X-1, Z + 1),
            new HexCoordinate(X, Z+1),
            new HexCoordinate(X+1, Z),
            new HexCoordinate(X+1, Z-1),
            new HexCoordinate(X, Z-1),
            new HexCoordinate(X-1, Z)
        };

        for (int i = 0; i < 6; i++)
        {
            Debug.Log(hexs[i]);
        }

        return hexs;
    }

    public static bool operator ==(HexCoordinate c1, HexCoordinate c2)
    {
        return c1.X == c2.X && c1.Z == c2.Z;
    }

    public static bool operator !=(HexCoordinate c1, HexCoordinate c2)
    {
        return c1.X != c2.X || c1.Z != c2.Z;
    }
}

public static class HexMetricks 
{
    /// <summary>
    /// Внешний радиус шестиугольника
    /// </summary>
    public const float outerRadius = 10f;

    /// <summary>
    /// Внутренний радиус шестиугольника
    /// </summary>
    public const float innerRadius = outerRadius * 0.866f;


    /// <summary>
    /// Положения углов шестиугольника
    /// </summary>
    public static Vector3[] conrners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius),
    };
}
