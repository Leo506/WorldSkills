using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        used = new List<Vector3Int>();

        Vector3Int randomPos;
        do
            randomPos = new Vector3Int(Random.Range(-5, 6), Random.Range(-5, 6), 0);
        while (used.Contains(randomPos) || !CheckBounds(randomPos));

        // Спавн травы
        for (int i = 0; i < 50; i++)
        {
            tilemap.SetTile(randomPos, stoneTiles[Random.Range(0, stoneTiles.Length)]);
            used.Add(randomPos);

            var neighbours = GetNeighbours(randomPos);
            if (neighbours.Count == 0)
                continue;
            randomPos = neighbours[Random.Range(0, neighbours.Count)];
        }

        do
            randomPos = new Vector3Int(Random.Range(-5, 6), Random.Range(-5, 6), 0);
        while (used.Contains(randomPos) || !CheckBounds(randomPos));


        // Спавн воды
        for (int i = 0; i < 30; i++)
        {
            tilemap.SetTile(randomPos, waterTiles[Random.Range(0, waterTiles.Length)]);
            used.Add(randomPos);

            var neighbours = GetNeighbours(randomPos);
            if (neighbours.Count == 0)
                continue;
            randomPos = neighbours[Random.Range(0, neighbours.Count)];
        }


        do
            randomPos = new Vector3Int(Random.Range(-5, 6), Random.Range(-5, 6), 0);
        while (used.Contains(randomPos) || !CheckBounds(randomPos));


        // Спавн ада
        for (int i = 0; i < 10; i++)
        {
            tilemap.SetTile(randomPos, hellTiles[Random.Range(0, hellTiles.Length)]);
            used.Add(randomPos);

            var neighbours = GetNeighbours(randomPos);
            if (neighbours.Count == 0)
                continue;
            randomPos = neighbours[Random.Range(0, neighbours.Count)];
        }


        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                var xPos = (int)(x - y / 2 + 0.5f * y) - 5;
                var yPos = (int)y - 5;
                var pos = new Vector3Int(xPos, yPos, 0);
                if (!used.Contains(pos))
                {
                    tilemap.SetTile(pos, grassTiles[Random.Range(0, grassTiles.Length)]);
                    used.Add(pos);
                }
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
