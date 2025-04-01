using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform player;
    public float tileSize = 5f;
    public int mapWidth = 20;
    public int mapHeight = 20;
    public float viewDistance = 20f;

    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
    public NavMeshSurface navMeshSurface;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        GenerateMap();
        UpdateTileVisibility();
        UpdateNavMesh();
    }

    void Update()
    {
        UpdateTileVisibility();
    }

    void GenerateMap()
    {
        for (int x = -mapWidth / 2; x <= mapWidth / 2; x++)
        {
            for (int y = -mapHeight / 2; y <= mapHeight / 2; y++)
            {
                Vector2 tilePos = new Vector2(x, y);
                Vector3 worldPos = new Vector3(x * tileSize, 0, y * tileSize);

                Tile tile = Instantiate(tilePrefab, worldPos, Quaternion.Euler(90, 0, 0), transform).GetComponent<Tile>();
                tile.gameObject.SetActive(false);
                tiles[tilePos] = tile;
            }
        }
    }

    void UpdateTileVisibility()
    {
        bool navMeshNeedsUpdate = false;

        foreach (var tileEntry in tiles)
        {
            Tile tile = tileEntry.Value;
            Vector3 tileWorldPos = tile.transform.position;

            bool isVisible = (Mathf.Abs(tileWorldPos.x - player.position.x) <= viewDistance &&
                              Mathf.Abs(tileWorldPos.z - player.position.z) <= viewDistance);

            if (tile.gameObject.activeSelf != isVisible)
            {
                tile.gameObject.SetActive(isVisible);
                navMeshNeedsUpdate = true;
            }
        }

        if (navMeshNeedsUpdate)
        {
            UpdateNavMesh();
        }
    }

    void UpdateNavMesh()
    {
        if (navMeshSurface == null)
        {
            Debug.LogError("navMeshSurface가 할당되지 않음!");
            return;
        }

        navMeshSurface.collectObjects = CollectObjects.Children;
        navMeshSurface.BuildNavMesh();
    }
}
