using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;

    private void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                var xPos = (int)(x - y / 2 + 0.5f * y) - 5;
                var yPos = (int)y - 5;

                tilemap.SetTile(new Vector3Int(xPos, yPos, 0), tiles[Random.Range(0, tiles.Length)]);
            }
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tile = tilemap.WorldToCell(origin);
            Debug.Log($"WorldPos: {origin} GetCellCenterWorld: {tilemap.GetCellCenterWorld(tile)}");
    
        }
    }
}
