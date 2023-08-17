using UnityEngine;
using UnityEngine.Tilemaps;

public class JungleGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grassTile;
    public Tile waterTile;
    public GameObject treePrefab;

    public Vector3Int mapSize = new Vector3Int(100, 100, 1);
    public float treeSpawnChance = 0.02f;  // 2% chance for a tree to spawn on a grass tile

    private void Start()
    {
        FillWithGrass();  // Fill the entire map with grass tiles
        PlaceWaterTiles();  // Use Perlin noise to decide where to place some water tiles
        PlaceTrees();  // Randomly place trees on grass tiles
    }

    private void FillWithGrass()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Debug.Log(cellPosition);
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

                // If noiseValue is less than 0.1, place a water tile over the grass tile
                if (noiseValue < 0.1f)
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
                    Instantiate(treePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, this.transform);
                }
            }
        }
    }
}
