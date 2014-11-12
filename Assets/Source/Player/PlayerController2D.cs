using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {

    public LayerMask groundLayer;

    public Transform groundCheck;

    public float groundCheckHeight = 0.2f,
                 groundCheckMargin = 0.2f;

    public bool isGrounded = false;

    public void Move(Vector2 worldMovement)
    {
        Rect newRect = new Rect(collider2D.bounds.min.x + groundCheckMargin, renderer.bounds.min.y, collider2D.bounds.size.x - (groundCheckMargin * 2), -groundCheckHeight);

        isGrounded = Physics2D.OverlapArea(newRect.min, newRect.max, groundLayer);

        rigidbody2D.MovePosition((Vector2)transform.position + worldMovement);
    }


}
