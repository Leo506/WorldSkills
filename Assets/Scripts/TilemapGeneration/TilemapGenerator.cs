using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

struct Seed
{
    public Vector3Int pos;
    public Tile tile;

    public Seed(Vector3Int pos, Tile tile)
    {
        this.pos = pos;
        this.tile = tile;
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

    private void Start()
    {

        tilemap.size = new Vector3Int(10, 10, 0);
        Debug.Log("Tilemap bounds: " + tilemap.localBounds.min + " " + tilemap.localBounds.max);

        List<Seed> seeds = new List<Seed>();

        // Создание случайно расположенных семян
        for (int i = 0; i < 4; i++)
        {
            var pos = new Vector3Int(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            seeds.Add(new Seed(pos, grassTiles[i]));
            tilemap.SetTile(pos, grassTiles[i]);
        }

        // Перемещение семян
        for (int i = 0; i < 800; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var neighbours = GetNeighbours(seeds[j].pos);
                if (neighbours.Count != 0)
                    seeds[j] = new Seed(neighbours[Random.Range(0, neighbours.Count)], seeds[j].tile);
                else
                    seeds[j] = new Seed(new Vector3Int(Random.Range(-5, 5), Random.Range(-5, 5), 0), seeds[j].tile);
                tilemap.SetTile(seeds[j].pos, seeds[j].tile);
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
            if (!CheckBounds(neighbours[i]))
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
        Vector3 min = tilemap.localBounds.min;
        Vector3 max = tilemap.localBounds.max;

        if (pos.x >= min.x && pos.x <= max.x)
        {
            if (pos.y >= min.y && pos.y <= max.y)
                return true;
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
