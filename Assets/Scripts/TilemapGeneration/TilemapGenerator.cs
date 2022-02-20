using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BSP;


public class TilemapGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public Tilemap obstacleTilemap;
    public Tile[] tiles;
    public Tile[] obstacleTiles;

    public Dictionary<int, List<Leaf>> map = new Dictionary<int, List<Leaf>>();
    public int seed;

    private void Start()
    {

        tilemap.size = new Vector3Int(300, 300, 0);
        obstacleTilemap.size = tilemap.size;
        var leaves = Program.GetLeaves(new Vector2(-150, -150), 300, 300);
        

        int i = 0;
        Debug.Log(leaves.Count);
        foreach (var item in leaves)
        {
            int startX = (int)item.position.x;
            int stopX = (int)item.position.x + item.width;

            int startY = (int)item.position.y;
            int stopY = (int)item.position.y + item.height;

            for (int x = startX; x < stopX; x++)
            {
                for (int y = startY; y < stopY; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tiles[i]);
                }
            }

            if (map.ContainsKey(i))
                map[i].Add(item);
            else
                map.Add(i, new List<Leaf> { item });

            i++;
            if (i >= tiles.Length)
                i = 0;
        }

        seed = Random.Range(0, 100);
        for (int j = 0; j < 4; j++)
        {
            foreach (var item in GetObstacles(j))
            {
                obstacleTilemap.SetTile(item, obstacleTiles[0]);
            }
        }
    }


    List<Vector3Int> GetObstacles(int type)
    {
        var random = new System.Random(seed);  // Инициализация рандомайзера
        List<Vector3Int> toReturn = new List<Vector3Int>();

        for (int l = 0; l < map[type].Count; l++)
        {
            Leaf leaf = map[type][l];
            int count = (int)(0.25f * leaf.width * leaf.height);  // Кол-во препятствий
            for (int i = 0; i < count; i++)
            {
                Vector3Int pos;
                int x, y;
                do
                {
                    x = random.Next((int)leaf.position.x, (int)leaf.position.x + leaf.width);
                    y = random.Next((int)leaf.position.y, (int)leaf.position.y + leaf.height);
                    pos = new Vector3Int(x, y, 0);
                }
                while (toReturn.Contains(pos));
                toReturn.Add(pos);
            }
        }

        return toReturn;
    }


    public void LoadMap()
    {
        tilemap.ClearAllTiles();
        obstacleTilemap.ClearAllTiles();
        foreach (var item in map)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                Leaf leaf = item.Value[i];

                int startX = (int)leaf.position.x;
                int stopX = (int)leaf.position.x + leaf.width;

                int startY = (int)leaf.position.y;
                int stopY = (int)leaf.position.y + leaf.height;

                for (int x = startX; x < stopX; x++)
                {
                    for (int y = startY; y < stopY; y++)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), tiles[item.Key]);
                    }
                }
            }
        }

        for (int j = 0; j < 4; j++)
        {
            foreach (var item in GetObstacles(j))
            {
                obstacleTilemap.SetTile(item, obstacleTiles[0]);
            }
        }
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
