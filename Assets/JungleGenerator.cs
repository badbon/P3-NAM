using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JungleGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grassTile;
    public Tile waterTile;
    public List<GameObject> treePrefabs;  // List of tree prefabs

    public Vector3Int mapSize = new Vector3Int(100, 100, 1);
    public float treeSpawnChance = 0.02f;  // Chance for a tree to spawn on a grass tile
    public float waterFrequency = 0.1f;    // Controls the frequency of water tiles

    private void Start()
    {
        FillWithGrass();
        PlaceWaterTiles();
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
