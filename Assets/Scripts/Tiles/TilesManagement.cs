using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilesManagement : MonoBehaviour
{
    public TilemapCollider2D tilemapCollider;
    public Tilemap tilemap;
    public Dictionary<Vector3Int, TileData> tiles = new Dictionary<Vector3Int, TileData>();
    public GameObject dropPrefab;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapCollider = GetComponent<TilemapCollider2D>();

        InitializeTiles();
    }
    public void UpdateTilemapCollider()
    {
        tilemapCollider.enabled = false; // �Ƚ���
        //compositeCollider.enabled = false;
        tilemapCollider.enabled = true;  // �����ã��� CompositeCollider2D �������
        //compositeCollider.enabled = true;
    }

    public void InitializeTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                
                tiles[pos] = new TileData(tile, 8, false,-1); // �趨Ĭ��Ѫ��Ϊ 8,��Ϊ���idΪ-1
                if (tile.name.Contains("Coal"))
                {
                    tiles[pos].isOre = true;
                    tiles[pos].itemID = 2;

                }
            }
        }
    }

    public void SpawnDrop(Vector3Int pos,int itemID)
    {
        GameObject drop = Instantiate(dropPrefab,tilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        drop.GetComponent<DropItem>().Initialize(itemID);
    }

    public void PlayDamageAnimation(Vector3Int tilePos)
    {
        TileBase tile = tilemap.GetTile(tilePos);
        if (tile == null) return;
        int a = tiles[tilePos].health;

        // ������Ը��� Tile Ѫ��������ͬ�� Tile ���֣�������ʾ�ѷ죩
        if (a == 7)
        {
            // ��������״̬ Tile������Ԥ��һ�� Tile ��Ϊ�ѷ�Ч����
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
