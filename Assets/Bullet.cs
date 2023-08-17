using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Projectile bullet
    public Rigidbody2D rb;
    public float damage;
    public float speed;
    public float range;
    public float accuracyModifier;

    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
