using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float health = 100f; // 100/100
    private float maxHealth = 100f; // Start health
    public float speed = 10.0f;

    public Loadout loadout;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    internal void TakeDamage(float damage)
    {
        Debug.Log("NPC took " + damage + " damage");
        health -= damage;
        if(health <= 0)
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
