using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JungleGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grassTile;
    public Tile waterTile;
    public GameObject treePrefab;  // Use a GameObject prefab instead of a Tile for trees
    //... Add more tiles as needed

    public Vector3Int mapSize = new Vector3Int(100, 100, 1);
    public float noiseScale = 0.1f;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                float noiseValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);

                // Now, you decide how to interpret this noise value
                if (noiseValue < 0.3f)
                {
                    tilemap.SetTile(position, waterTile);
                }
                else if (noiseValue < 0.5f)
                {
                    // Instead of setting a tile, we'll instantiate a GameObject here
                    Vector3 worldPos = tilemap.CellToWorld(position);
                    Instantiate(treePrefab, worldPos, Quaternion.identity, this.transform);
                }
                else
                {
                    tilemap.SetTile(position, grassTile);
                }

                // Continue to add more conditions for different tiles or game objects as needed
            }
        }
    }
}
