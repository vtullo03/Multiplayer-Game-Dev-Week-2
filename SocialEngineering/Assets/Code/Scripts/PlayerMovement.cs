using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Controls controls;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    private float speed = 5f;

    void Awake()
    {
        controls = GetComponent<Controls>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        velocity = controls.MoveInput() * speed;
        velocity = new Vector2(velocity.x, 0); /* Get rid of movement input in the y-axis*/
        if (velocity.x > 0) spriteRenderer.flipX = true;
        else if (velocity.x < 0) spriteRenderer.flipX = false;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
