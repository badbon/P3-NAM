using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CampaignMapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grassTile;
    public Tile dirtTile;
    public List<GameObject> treePrefabs;  // List of tree prefabs

    public Vector3Int mapSize = new Vector3Int(100, 100, 1);
    public float treeSpawnChance = 0.02f;  // Chance for a tree to spawn on a grass tile
    public float dirtFrequency = 0.15f;  // Controls the frequency of dirt tiles

    public int seed = 0;
    public bool useRandomSeed = true;

    private void Start()
    {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.GetHashCode();
        }

        Random.InitState(seed);

        FillWithGrass();
        PlaceTerrainTiles();  // Replaced PlaceWaterTiles with PlaceTerrainTiles
        PlaceTrees();

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
                // Adjusted seed for better layouts
                float noiseValue = Mathf.PerlinNoise(x * 0.05f + (seed * 0.0001f), y * 0.05f + (seed * 0.0001f));

                if (noiseValue < dirtFrequency)
                {
                    tilemap.SetTile(cellPosition, dirtTile);
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
                    // Select a random tree prefab from the list
                    GameObject randomTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Count)];
                    Instantiate(randomTreePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, this.transform);
                }
            }
        }
    }

    public void SortChildrenByY(Transform parentTransform)
    {
        List<Transform> children = new List<Transform>();

        // Populate the list with each child
        foreach (Transform child in parentTransform)
        {
            children.Add(child);
        }

        // Sort the list based on Y-coordinate
        children.Sort((a, b) => b.position.y.CompareTo(a.position.y));

        // Re-attach the children in the new order
        foreach (Transform child in children)
        {
            child.SetAsLastSibling();
        }
    }

}
