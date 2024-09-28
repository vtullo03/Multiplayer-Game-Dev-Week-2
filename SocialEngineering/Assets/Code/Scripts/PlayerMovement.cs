using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void LobbyMovement()
    {
        velocity = new Vector2(velocity.x, 0); /* Get rid of movement input in the y-axis*/
        if (velocity.x > 0) spriteRenderer.flipX = true;
        else if (velocity.x < 0) spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        velocity = controls.MoveInput() * speed;
        if (SceneManager.GetActiveScene().name == "PlayerJoin") LobbyMovement();
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        if (SceneManager.GetActiveScene().name == "MainGame") rb.gravityScale = 0;
        else rb.gravityScale = 12;
    }
}
