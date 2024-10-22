using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JungleGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grassTile;
    public Tile dirtTile;
    public Tile waterTile;
    public List<GameObject> treePrefabs;  // List of tree prefabs
    public int enemyPresencePercentage = 15; // 100 - a lot of enemies, 0 - no enemies

    public Vector3Int mapSize = new Vector3Int(100, 100, 1);
    public float treeSpawnChance = 0.02f; // Chance for a tree to spawn on a grass tile
    public float waterFrequency = 0.1f; // Controls the frequency of water tiles
    public float dirtFrequency = 0.15f;

    public int seed = 0;
    public bool useRandomSeed = true;
    public List<GameObject> enemyPrefabs;
    private float baseEnemyChance = 0.00001f;

    private void Start()
    {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.GetHashCode();
        }

        Random.InitState(seed);
        FillWithGrass();
        PlaceTerrainTiles();
        PlaceTrees();
        EnemyPresence();

        // To make sure trees don't generate root-first if theyre too close on top of each other
        SortChildrenByY(transform);
    }

    private void FillWithGrass()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(cellPosition, grassTile);
            }
        }
    }

    private void PlaceTerrainTiles()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                float noiseValue = Mathf.PerlinNoise(x * 0.05f + (seed * 0.0001f), y * 0.05f + (seed * 0.0001f));

                if (noiseValue < waterFrequency)
                {
                    tilemap.SetTile(cellPosition, waterTile);
                }
                else if (noiseValue < waterFrequency + dirtFrequency)
                {
                    tilemap.SetTile(cellPosition, dirtTile);
                }
            }
        }
    }

    private void EnemyPresence()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (Random.value < baseEnemyChance * enemyPresencePercentage)
                {
                    GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                    Instantiate(randomEnemyPrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, this.transform);
                }
            }
        }
    }

    private void PlaceTrees()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(cellPosition) == grassTile && Random.value < treeSpawnChance)
                {
                    GameObject randomTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Count)];
                    Instantiate(randomTreePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, this.transform);
                }
            }
        }
    }

    public void SortChildrenByY(Transform parentTransform)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parentTransform)
        {
            children.Add(child);
        }

        children.Sort((a, b) => b.position.y.CompareTo(a.position.y));
        foreach (Transform child in children)
        {
            child.SetAsLastSibling();
        }
    }

}
