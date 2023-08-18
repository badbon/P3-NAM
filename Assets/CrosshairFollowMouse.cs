using UnityEngine;
using UnityEngine.EventSystems;

public class CrosshairFollowMouse : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined; // Optional: This confines the cursor within the game window.
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
}
