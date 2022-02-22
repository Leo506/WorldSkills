using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    [SerializeField] Tilemap obstacleMap;
    [SerializeField] Tilemap map;
    [SerializeField] GameObject hero;
    [SerializeField] LayerMask obstacleLayer;


    Dictionary<Vector3Int, MoveTiles> tiles = new Dictionary<Vector3Int, MoveTiles>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tilePos = obstacleMap.WorldToCell(clickPoint);

            CustomTile tile = obstacleMap.GetTile(tilePos) as CustomTile;
            if (tile == null)
            {
                if (CheckDistance(tilePos))
                {
                    hero.transform.position = map.GetCellCenterWorld(tilePos);
                    ShowMoveTiles();
                }
            }
        }
    }

    private void ShowMoveTiles()
    {
        if (tiles.Count != 0)
        {
            foreach (var item in tiles)
            {
                item.Value.SetHighLigh(false);
                map.RefreshTile(item.Key);
            }
            tiles.Clear();
        }

        Vector3Int heroTilePos = map.WorldToCell(hero.transform.position);

        foreach (var item in GetNeighbours(heroTilePos))
        {
            MoveTiles move = map.GetTile(item) as MoveTiles;
            if (move != null)
            {
                Debug.Log("Trying to high light");
                tiles.Add(item, move);
                move.SetHighLigh(true);
                map.RefreshTile(item);
            }
        }
    }

    private bool CheckDistance(Vector3Int tilePos)
    {
        Vector3Int heroPos = map.WorldToCell(hero.transform.position);
        bool xDistance = Mathf.Abs(tilePos.x - heroPos.x) <= 1;
        bool yDistance = Mathf.Abs(tilePos.y - heroPos.y) <= 1;

        return xDistance && yDistance;
    }


    private List<Vector3Int> GetNeighbours(Vector3Int pos)
    {
        return new List<Vector3Int>
        {
            pos + new Vector3Int(+1, 0, 0),  // Боковой справа
            pos + new Vector3Int(-1, 0, 0),  // Боковой слева
            pos + new Vector3Int(0, +1, 0),
            pos + new Vector3Int(0, -1, 0),

            pos + (pos.y % 2 == 0 ? new Vector3Int(-1, +1, 0) : new Vector3Int(+1, +1, 0)),

            pos + (pos.y % 2 == 0 ? new Vector3Int(-1, -1, 0) : new Vector3Int(+1, -1, 0))
        };
    }


    private void OnApplicationQuit()
    {
        if (tiles.Count != 0)
        {
            foreach (var item in tiles)
            {
                item.Value.SetHighLigh(false);
            }
        }
    }
}
