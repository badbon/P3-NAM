using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignAIController : MonoBehaviour
{
    public float speed = 5f;              // Movement speed
    public float waypointTolerance = 0.5f; // Distance from waypoint to be considered as reached

    private Vector3 currentWaypoint;

    public CampaignMapGenerator mapGenerator;
    Vector2 mapBounds;


    void Start()
    {
        mapGenerator = FindObjectOfType<CampaignMapGenerator>();
        SetNewWaypoint();  // Choose the initial waypoint
    }

    void Update()
    {
        MoveTowardsWaypoint();
    }

    IEnumerator SetNewWaypointAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentWaypoint = new Vector3
        (Random.Range(-mapGenerator.mapSize.x, mapGenerator.mapSize.x),
         Random.Range(-mapGenerator.mapSize.x, mapGenerator.mapSize.x), 0);
    }

    void MoveTowardsWaypoint()
    {
        Vector3 direction = (currentWaypoint - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, currentWaypoint) <= waypointTolerance)
        {
            StartCoroutine(SetNewWaypointAfterDelay(0.5f));  // Wait for 0.5 seconds before setting a new waypoint
        }
    }


    void SetNewWaypoint()
    {
        currentWaypoint = new Vector3(Random.Range(0, 100), Random.Range(0, 100), 0);
    }
}
