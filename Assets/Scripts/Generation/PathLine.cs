using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    public LineRenderer line;

    public void CreateLine(List<HexCoordinate> coordinates)
    {
        line.positionCount = coordinates.Count;

        for (int i = 0; i < coordinates.Count; i++)
        {
            line.SetPosition(i, new Vector3((coordinates[i].X + coordinates[i].Z * 0.5f) * 17.32f, 0.3f, coordinates[i].Z * 15));
        }
    }    
}
