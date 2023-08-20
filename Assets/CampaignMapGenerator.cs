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

    // Campaign
    public int totatEnemyUnits = 10;
    public int totalPlayerUnits = 10;
    public List<Unit> enemyUnits;
    public List<Unit> playerUnits;

    // Conditions of campaign. Support points gives US faction air/arty support. Mine rate gives VC faction more trapsetting, etc.
    public int supportPoints; 
    public int mineRate;

    public int turn = 0; // Turn counter
    public bool playerTurn = true; // If true, player turn. If false, enemy turn.



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

    public void GenerateRandomUnits()
    {
        for (int i = 0; i < totatEnemyUnits; i++)
        {
            string unitName = "Enemy Unit " + i;
            int unitCondition = Random.Range(10, 31);  // Generates a number between 10 and 30 inclusive.
            int unitMorale = Random.Range(80, 101);    // Generates a number between 80 and 100 inclusive.
            int unitAttack = Random.Range(5, 16);      // Generates a number between 5 and 15 inclusive.
            bool unitStealth = (Random.value > 0.5f);  // Randomly sets true or false.
            bool unitEntrenched = (Random.value > 0.5f);

            Unit newUnit = new Unit(unitName, unitCondition, unitMorale, unitAttack, unitStealth, unitEntrenched);
            enemyUnits.Add(newUnit);
        }
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

public class Unit
{
    public string name;
    public int condition; // HP, basically.
    public int morale;
    public int attack; // Raw attack power
    public bool stealth; // If undetected by enemy side, stealth = true
    public bool entrenched; // Immobile, but gets mine and support bonus. Good for ambush or general defense.

    public Unit(string name, int condition = 20, int morale = 100, int attack = 10, bool stealth = true, bool entrenched = false)
    {
        this.name = name;
        this.condition = condition;
        this.morale = morale;
        this.attack = attack;
        this.stealth = stealth;
        this.entrenched = entrenched;
    }
}
