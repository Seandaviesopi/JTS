using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
    // Character Component
    [HideInInspector]
    public PlayerCharacter PC;
    //
    public float gravity,
                 airMoveMultiplier,
                 walkSpeed;
    // Current Velocity of Character
    public Vector2 velocity = Vector2.zero;
    // Move Physics
    protected enum MoveState
    {
        NONE,
        WALKING,
        FALLING,
        MAX
    }
    // Current Move Physics
    protected MoveState moveMode = MoveState.NONE;
    // Holds Transform of Ground Check
    private Transform groundCheck;

    /*
     * 
     */
    void Awake()
    {
        // Get Player Character
        PC = GetComponent<PlayerCharacter>();
        // Get groundCheck Transform
        groundCheck = transform.FindChild("GroundCheck");
        // Default Move State
        moveMode = MoveState.WALKING;
    }

    /*
     * 
     */
    public void PerformMovement(Vector2 worldMovement)
    {
        // Perform Physics Based Movement
        switch(moveMode)
        {
            // No Physics
            case MoveState.NONE:
                return;
            // Walking Phyiscs
            case MoveState.WALKING:
                MoveWalking(worldMovement);
                break;
            // Falling Physics
            case MoveState.FALLING:
                MoveFalling(worldMovement);
                break;
            // Error
            default:
                return;
        }
    }

    /*
     * 
     */
    protected void MoveWalking(Vector2 worldMovement)
    {
        // Current Position 2D
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        // Move Amount
        Vector2 moveVector = worldMovement * walkSpeed;
        // Apply Move
        rigidbody2D.MovePosition(position2D + ((moveVector + velocity) * Time.fixedDeltaTime));
        // If not grounded
        if (!IsGrounded())
        {
            // Set Falling
            moveMode = MoveState.FALLING;
        }
    }

    /*
     * 
     */
    protected void MoveFalling(Vector2 worldMovement)
    {
        // Current Position 2D
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        // Move Amount
        Vector2 moveVector = (transform.eulerAngles == new Vector3(0f, 0f, 0f) ? Vector2.right : -Vector2.right) * airMoveMultiplier;
        // Apply Gravity Velocity
        velocity.y += (Physics2D.gravity.y * rigidbody2D.gravityScale) / rigidbody2D.mass;
        // Apply Move
        rigidbody2D.MovePosition(position2D + ((moveVector + velocity) * Time.fixedDeltaTime));
        // If grounded
        if (IsGrounded())
        {
            // Zero out Velocity
            velocity = Vector2.zero;
            // Set Walking
            moveMode = MoveState.WALKING;
        }
    }

    /*
     * 
     */
    protected bool IsGrounded()
    {
        Vector2 rightBound = new Vector2(transform.position.x + .4f, transform.position.y);
        Vector2 leftBound = new Vector2(transform.position.x - .4f, transform.position.y);

        bool isOnGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
                          Physics2D.Linecast(rightBound, new Vector2(rightBound.x, groundCheck.position.y), 1 << LayerMask.NameToLayer("Ground")) ||
                          Physics2D.Linecast(leftBound, new Vector2(leftBound.x, groundCheck.position.y), 1 << LayerMask.NameToLayer("Ground"));

        return isOnGround;
    }
}
