using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFlip : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float backwardsPower = 15f;
    [SerializeField]
    private float backwardsPowerRand;
    public float lifeTime = 1f;
    public float upForce = 1f;

    public Rigidbody2D rb;

    void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        // Torque backwards
        rb.AddTorque(rotationSpeed, ForceMode2D.Force);
        // Randomize backwards power 50% to 200%
        backwardsPowerRand = Random.Range(backwardsPower * 0.5f, backwardsPower * 2f);
        // Throw backwards
        rb.AddForce(-transform.right * backwardsPowerRand, ForceMode2D.Force);
        // Throw upwards
        rb.AddForce(transform.up * upForce, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }
}
