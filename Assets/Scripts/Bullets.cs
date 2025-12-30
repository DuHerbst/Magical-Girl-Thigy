using System;
using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    public abstract float speed { get; set; }
    public abstract float damage { get; set; }
    public Vector2 movement;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = speed;
        damage = damage;
    }

    private void FixedUpdate()
    {
     
        movement = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));
        

        rb.linearVelocity = new Vector2(movement.x * speed, movement.y * speed);
    }
}
