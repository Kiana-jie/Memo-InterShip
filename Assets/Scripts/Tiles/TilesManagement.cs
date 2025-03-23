using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilesManagement : MonoBehaviour
{
    public TilemapCollider2D tilemapCollider;
    public Tilemap tilemap;
    public Dictionary<Vector3Int, TileData> tiles = new Dictionary<Vector3Int, TileData>();

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapCollider = GetComponent<TilemapCollider2D>();

        InitializeTileHealth();
    }
    public void UpdateTilemapCollider()
    {
        tilemapCollider.enabled = false; // 先禁用
        //compositeCollider.enabled = false;
        tilemapCollider.enabled = true;  // 再启用，让 CompositeCollider2D 重新组合
        //compositeCollider.enabled = true;
    }

    public void InitializeTileHealth()
    {
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                tiles[pos] = new TileData(tile, 8); // 设定默认血量为 8
            }
        }
    }

    public void PlayDamageAnimation(Vector3Int tilePos)
    {
        TileBase tile = tilemap.GetTile(tilePos);
        if (tile == null) return;
        int a = tiles[tilePos].health;

        // 这里可以根据 Tile 血量更换不同的 Tile 表现（比如显示裂缝）
        if (a == 7)
        {
            // 更换受损状态 Tile（可以预设一个 Tile 作为裂缝效果）
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_70"));
        }
        else if (a == 6)
        {
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_71"));
        }
        else if (a == 5)
        {
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_72"));
        }
        else if (a == 4)
        {
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_73"));
        }
        else if (a == 3)
        {
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_74"));
        }
        else if (a == 2)
        {
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_75"));
        }
        else if (a == 1)
        {
            tilemap.SetTile(tilePos, Resources.Load<TileBase>("Tilemap/TilePalette/GroundLayer/stage1_76"));
        }
    }
}
