using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class Node
{
    public Vector3Int tilePos;
    public Node previousNode;

    public Node(Vector3Int pos, Node node)
    {
        tilePos = pos;
        previousNode = node;
    }
}

public class TilemapGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public Tile[] grassTiles;
    public Tile[] waterTiles;
    public Tile[] stoneTiles;
    public Tile[] hellTiles;

    List<Vector3Int> used;
    List<Node> nodes;
    Node front;

    BoundsInt bounds;

    private void Start()
    {

        tilemap.size = new Vector3Int(10, 10, 0);
        bounds = tilemap.cellBounds;
        Debug.Log("Tilemap bounds: " + tilemap.cellBounds.min + " " + tilemap.cellBounds.max);

        used = new List<Vector3Int>();

        for (int j = 0; j < 3; j++)
        {
            Vector3Int randomPos;
            do
                randomPos = new Vector3Int(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            while (used.Contains(randomPos));
            tilemap.SetTile(randomPos, grassTiles[j]);
            used.Add(randomPos);

            front = new Node(randomPos, null);

            for (int i = 0; i < 30; i++)
            {
                var neighbours = GetNeighbours(randomPos);
                if (neighbours.Count != 0)
                {
                    randomPos = neighbours[Random.Range(0, neighbours.Count)];
                    tilemap.SetTile(randomPos, grassTiles[j]);
                    used.Add(randomPos);
                    Node node = new Node(randomPos, front);
                    front = node;
                }
                else
                {
                    List<Vector3Int> n = new List<Vector3Int>();
                    do
                    {
                        if (front.previousNode == null)
                            break;
                        else
                            front = front.previousNode;
                        n = GetNeighbours(front.tilePos);

                    } while (n.Count == 0);

                    if (n.Count != 0)
                    {
                        randomPos = n[Random.Range(0, n.Count)];
                        tilemap.SetTile(randomPos, grassTiles[j]);
                        used.Add(randomPos);
                        Node node = new Node(randomPos, front);
                        front = node;
                    }
                }
            }
        }

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (!used.Contains(pos))
                    tilemap.SetTile(pos, waterTiles[0]);
            }
        }



    }


    List<Vector3Int> GetNeighbours(Vector3Int cell)
    {
        bool yEven = cell.y % 2 == 0;

        List<Vector3Int> neighbours = new List<Vector3Int>();

        neighbours.Add(cell + new Vector3Int(+1, +0, 0)); //горизонтальный правый;
        neighbours.Add(cell + new Vector3Int(-1, +0, 0)); //горизонтальный левый
        neighbours.Add(cell + new Vector3Int(+0, +1, 0)); //диагональный верхний (для чётного ряда - правый, для нечётного - левый)
        neighbours.Add(cell + new Vector3Int(+0, -1, 0)); //диагональный нижний (для чётного ряда - правый, для нечётного - левый)

        neighbours.Add(cell + (yEven ? new Vector3Int(-1, +1, 0) : new Vector3Int(+1, +1, 0)));
        neighbours.Add(cell + (yEven ? new Vector3Int(-1, -1, 0) : new Vector3Int(+1, -1, 0)));

        List<Vector3Int> toRemove = new List<Vector3Int>();
        for (int i = 0; i < 6; i++)
        {
            if (!CheckBounds(neighbours[i]) || used.Contains(neighbours[i]))
                toRemove.Add(neighbours[i]);
        }

        if (toRemove.Count != 0)
        {
            foreach (var item in toRemove)
            {
                neighbours.Remove(item);
            }
        }

        return neighbours;
    }


    bool CheckBounds(Vector3Int pos)
    {
        

        if (pos.x >= bounds.min.x && pos.x <= bounds.max.x)
        {
            if (pos.y >= bounds.min.y && pos.y <= bounds.max.y)
            {
                Debug.Log("pos: " + pos + "  minX < pos <  maxX" + (pos.x >= bounds.min.x && pos.x <= bounds.max.x));
                return true;
            }
        }

        return false;
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tile = tilemap.WorldToCell(origin);
            Debug.Log($"WorldPos: {origin} GetCellCenterWorld: {tilemap.GetCellCenterLocal(tile)}");
    
        }
    }
}
