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
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = transform.right * speed;
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(range);
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet hit " + collision.gameObject.name);
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NPCController>().TakeDamage(damage);
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
