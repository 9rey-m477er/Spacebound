using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniDirectionalMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //wasd
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
