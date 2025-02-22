using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    public int width;
    public int height;
    public int cellSize;
    [SerializeField] private List<GameObject> tilePrefabs;

    public Grid<Tile> _grid;
    private const int batchInstantiateSize = 1000;

    protected override void Awake()
    {
        base.Awake();

        _grid = new Grid<Tile>(width, height, cellSize);
        InstantiateMap();    
    }

    private void InstantiateMap()
    {
        int emptyTileCount = GetEmptyTileCount();

        while (emptyTileCount > 0)
        {
            int batchSize = Mathf.Min(emptyTileCount, batchInstantiateSize);

            for (int i = 0; i < batchSize; i++)
            {
                Vector2Int gridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

                if (IsTileTypeEmpty(gridPosition.x, gridPosition.y))
                {
                    _grid._grid[gridPosition.x, gridPosition.y].tileType = TileType.Empty;                    
                    GenerateMap(gridPosition.x, gridPosition.y);
                }
            }

            emptyTileCount = GetEmptyTileCount();
        }
    }

    private int GetEmptyTileCount()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if(_grid._grid[x,z].tileType == TileType.Empty)
                {
                    count++;
                }
            }
        }

        return count;
    }

    bool IsTileTypeEmpty(int x, int z)
    {
        return _grid._grid[x,z].tileType == TileType.Empty ? true : false;
    }

    private void GenerateMap(int x, int z)
    {
        Instantiate(tilePrefabs[0], _grid.GridToWorldPosition( new Vector2Int(x, z)), Quaternion.identity);
    }
}
