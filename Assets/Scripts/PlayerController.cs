using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 2D top-down movement
    public float speed = 10.0f;
    public float health = 100f; // 100/100
    private float maxHealth = 100f; // Start health

    public Vector3 spawnPoint; // For respawns, etc
    public SpriteRenderer[] spriteRenderers; // All visible player parts

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

    internal void TakeDamage(float damage)
    {
        // Take damage
        health -= damage;

        // Check if dead
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        // Respawn
        StartCoroutine(FlashPlayerVisibility());
        transform.position = spawnPoint;
        health = maxHealth;
    }

    public IEnumerator FlashPlayerVisibility()
    {
        // For respawn fx
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = false;
        }

        yield return new WaitForSeconds(0.5f);

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = true;
        }
    }
}
