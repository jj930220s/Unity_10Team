using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int poolSize = 6;
    public Transform player;
    public float tileSize = 5f;
    public int maxTiles = 10;
    public int extraTiles = 2;

    private ObjectPool<Tile> tilePool;
    private Dictionary<Vector2, Tile> activeTiles = new Dictionary<Vector2, Tile>();
    private Vector2 lastPlayerPos;

    public NavMeshSurface navMeshSurface;

    void Awake()
    {
        Tile tileTemplate = tilePrefab.GetComponent<Tile>();
        tilePool = new ObjectPool<Tile>(tileTemplate, poolSize, transform);
        lastPlayerPos = new Vector2(player.position.x, player.position.z);
        UpdateMap();
        UpdateNavMesh();
    }

    void Update()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        if (Vector2.Distance(playerPos, lastPlayerPos) > tileSize / 2)
        {
            lastPlayerPos = playerPos;
            UpdateMap();
        }
    }

    void UpdateMap()
    {
        HashSet<Vector2> neededTiles = new HashSet<Vector2>();

        int halfSize = (int)(Mathf.Sqrt(poolSize) / 2) + extraTiles;
        Vector2 playerTilePos = new Vector2(
            Mathf.Round(player.position.x / tileSize),
            Mathf.Round(player.position.z / tileSize)
        );

        for (int x = -halfSize; x <= halfSize; x++)
        {
            for (int y = -halfSize; y <= halfSize; y++)
            {
                Vector2 tilePos = playerTilePos + new Vector2(x, y);
                neededTiles.Add(tilePos);

                if (!activeTiles.ContainsKey(tilePos) && activeTiles.Count < maxTiles)
                {
                    Tile tile = tilePool.Get();
                    if (tile == null) tile = Instantiate(tilePrefab, transform).GetComponent<Tile>();

                    tile.Activate(new Vector3(tilePos.x * tileSize, 0, tilePos.y * tileSize), transform);
                    activeTiles[tilePos] = tile;
                }
            }
        }

        List<Vector2> tilesToRemove = new List<Vector2>();
        foreach (var tile in activeTiles)
        {
            if (!neededTiles.Contains(tile.Key))
            {
                tilePool.Release(tile.Value);
                tilesToRemove.Add(tile.Key);
            }
        }

        foreach (var pos in tilesToRemove)
        {
            activeTiles.Remove(pos);
        }

        UpdateNavMesh();
    }

    void UpdateNavMesh()
    {
        if (navMeshSurface == null)
        {
            Debug.LogError("navMeshSurface가 할당되지 않음!");
            return;
        }

        navMeshSurface.layerMask = LayerMask.GetMask("Ground");
        navMeshSurface.collectObjects = CollectObjects.Children;
        navMeshSurface.BuildNavMesh();
    }
}
