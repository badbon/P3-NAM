using UnityEngine;
using UnityEngine.EventSystems;


public class CampaignPlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false; // Game only moves when player moves.

    public Camera cam;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;


    private void Update()
    {
        // Check if clicked over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on UI");
        }
        else
        {
            //Debug.Log("Clicked on world");
            if (Input.GetMouseButtonDown(0))
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isMoving = true;
            }
        }

        if (isMoving)
        {
            MovePlayer();
        }

        // Camera zooming.
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scrollData * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }

    private void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If player reaches target position
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Party"))
        {
            // Interaction logic here, like showing a dialogue or starting a battle.
            Debug.Log("Player encountered a party.");
        }
    }

}
