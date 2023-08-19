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

    public Vector3Int mapSize = new Vector3Int(100, 100, 1);
    public float treeSpawnChance = 0.02f;  // Chance for a tree to spawn on a grass tile
    public float waterFrequency = 0.1f;    // Controls the frequency of water tiles
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

    private void PlaceWaterTiles()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                float noiseValue = Mathf.PerlinNoise(x * 0.05f, y * 0.05f);

                if (noiseValue < waterFrequency)
                {
                    tilemap.SetTile(cellPosition, waterTile);
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
}
