using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    public static Tilemap FloorMap, WallMap, SpawnPointMap;

    BoundsInt bounds;
    new Camera camera;

    Vector3Int[,] tilePos;

    public enum GameTiles { ENEMY, WALL, FLOOR};
    public static GameTiles[,] gameTiles;

    private void Start()
    {
        FloorMap = GameObject.Find("FloorMap").GetComponent<Tilemap>();
        WallMap = GameObject.Find("WallMap").GetComponent<Tilemap>();
        SpawnPointMap = GameObject.Find("SpawnPointMap").GetComponent<Tilemap>();

        FloorMap.CompressBounds();
        WallMap.CompressBounds();

        bounds = FloorMap.cellBounds;

        CreateTilemap();

        camera = Camera.main;
        //camera.transform.position = new Vector3(bounds.center.x+10, bounds.center.y, -10);

        
    }
    void CreateTilemap()
    {
        tilePos = new Vector3Int[bounds.size.x, bounds.size.y];
        gameTiles = new GameTiles[bounds.size.x, bounds.size.y];

        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (FloorMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    if (WallMap.HasTile(new Vector3Int(x, y, 0)))
                    {
                        FloorMap.SetTile(new Vector3Int(x, y), null);
                        gameTiles[i, j] = GameTiles.WALL;
                    }
                    gameTiles[i, j] = GameTiles.FLOOR;

                    tilePos[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    tilePos[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }

        print("Tilemap Created");
    }
}
