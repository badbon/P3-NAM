using UnityEngine;

public class NPCController : MonoBehaviour
{
    public JungleGenerator mapGenerator;
    private Vector2 currentTarget;

    public float health = 100f;
    private float maxHealth = 100f;
    public float speed = 10.0f;
    public float detectionRange = 5.0f;
    public float reachedTargetThreshold = 0.5f; // Distance to consider that NPC reached its target
    public bool isHostile = true;

    private Transform player;

    public Loadout loadout;

    void Start()
    {
        mapGenerator = FindObjectOfType<JungleGenerator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming your player object has the tag "Player"
        ChooseNewTarget();
    }

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget) < reachedTargetThreshold)
        {
            ChooseNewTarget();
        }
    }

    private void ChooseNewTarget()
    {
        Vector3Int randomCell;

        do
        {
            randomCell = new Vector3Int(
                Random.Range(0, mapGenerator.mapSize.x),
                Random.Range(0, mapGenerator.mapSize.y),
                0
            );
        }
        while (mapGenerator.tilemap.GetTile(randomCell) == mapGenerator.waterTile);

        currentTarget = mapGenerator.tilemap.GetCellCenterWorld(randomCell);
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isHostile && distanceToPlayer <= detectionRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Calculate angle towards player
        float aimAngle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;

        // Fire at player with that angle
        loadout.FireWithAccuracy(aimAngle, Vector2.Distance(transform.position, player.position));

        Debug.Log("Attacking Player");
    }


    internal void TakeDamage(float damage)
    {
        Debug.Log("NPC took " + damage + " damage");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("NPC died");
        Destroy(gameObject);
    }
}
