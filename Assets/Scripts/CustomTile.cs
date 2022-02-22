using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new CustomTile", menuName = "Tiles/CustomTile")]
public class CustomTile : Tile
{
    public CustomTile()
    {
        Debug.Log("Создан кастомный тайл");
    }
}
