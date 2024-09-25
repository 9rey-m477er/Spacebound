using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniDirectionalMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public LayerMask unwalkableLayer;

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
        if (!IsTileUnwalkable(newPosition))
        {
            rb.MovePosition(newPosition);
        }
    }

    bool IsTileUnwalkable(Vector2 targetPosition)
    {
        return Physics2D.OverlapCircle(targetPosition, 0.1f, unwalkableLayer) != null;
    }
}
