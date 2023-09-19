using UnityEngine;

public class GunOrbit : MonoBehaviour
{
    public float orbitRadius = 50.0f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Get Screen Center
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        // 2. Get Mouse Position
        Vector2 mousePosition = Input.mousePosition;

        // 3. Calculate Direction
        Vector2 directionToMouse = (mousePosition - screenCenter).normalized;

        // 4. Set Gun Position
        Vector2 gunPosition = screenCenter + directionToMouse * orbitRadius;

        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(gunPosition);
        worldPosition.z = 0;  // Ensure z-position remains consistent

        // 5. Position and Rotate the Gun
        transform.position = worldPosition;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 6. Flip Sprite if Necessary
        if (angle > 90 || angle < -90)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
}
