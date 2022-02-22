using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New MoveTile", menuName ="Tiles/MoveTile")]
public class MoveTiles : Tile
{
    [SerializeField] Sprite commonTile;
    [SerializeField] Sprite highLightSprite;

    public MoveTiles()
    {
        this.sprite = commonTile;
    }


    /*public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }*/


    public void SetHighLigh(bool on)
    {
        this.sprite = on ? highLightSprite : commonTile;
    }
}
