using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public Tilemap obstacleTilemap;
    public Tile[] tiles;
    public Tile[] obstacleTiles;

    Dictionary<int, List<Vector3Int>> map = new Dictionary<int, List<Vector3Int>>();

    private void Start()
    {

        tilemap.size = new Vector3Int(20, 20, 0);
        obstacleTilemap.size = tilemap.size;
        var leaves = BSP.Program.GetLeaves(new Vector2(-5, -5), 10, 10);
        

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
                    if (map.ContainsKey(i))
                        map[i].Add(new Vector3Int(x, y, 0));
                    else
                        map.Add(i, new List<Vector3Int> { new Vector3Int(x, y, 0) });
                }
            }

            i++;
            if (i >= tiles.Length)
                i = 0;
        }

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
        int count = (int)(0.25f * map[type].Count);
        //var random = new System.Random(10);
        List<Vector3Int> toReturn = new List<Vector3Int>();
        for (int i = 0; i < count; i++)
        {
            Vector3Int pos;
            do
                pos = map[type][Random.Range(0, map[type].Count)];
            while (toReturn.Contains(pos));
            toReturn.Add(pos);
        }

        return toReturn;
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
