using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    

    public static List<HexCoordinate> CreatePath(HexCoordinate c1, HexCoordinate c2)
    {
        List<HexCoordinate> path = new List<HexCoordinate>();

        if (c1 == c2)
            path = new List<HexCoordinate>() { new HexCoordinate(c1.X, c1.Z) };
        else
        {
            path.Add(c1);
            HexCoordinate nextStep = new HexCoordinate(-10, -10);

            while(nextStep != c2)
            {
                HexCoordinate minDistanceCoordinate;

                if (nextStep != new HexCoordinate(-10, -10))
                    minDistanceCoordinate = nextStep;
                else
                    minDistanceCoordinate = c1;

                float minDistance = 100;
                var neighbors = minDistanceCoordinate.GetNeighbors();
                for (int j = 0; j < 6; j++)
                {
                    HexCoordinate neighbor = neighbors[j];
                    Debug.Log("Neighbor 1: " + neighbor);
                    var a = Mathf.Pow((c2.X - neighbor.X), 2);
                    var b = Mathf.Pow((c2.Z - neighbor.Z), 2);
                    float distance = Mathf.Sqrt(a + b);
                    Debug.Log("Neighbor 2: " + neighbor + " distance: " + distance);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minDistanceCoordinate = neighbor;
                    }
                }

                nextStep = minDistanceCoordinate;
                path.Add(nextStep);
            }

            path.Add(c2);
        }

        return path;
    }
}
