using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 2D top-down movement
    public float speed = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        // Get the direction of the player
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Move the player
        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        // Move the player
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
