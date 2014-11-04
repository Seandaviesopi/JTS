using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
    // Move Physics
    public enum MoveState
    {
        NONE,
        WALKING,
        FALLING,
        JETPACK,
        MAX
    }

    // Character Component
    [HideInInspector]
    public PlayerCharacter PC;

    // Current Move Physics
    [HideInInspector]
    public MoveState moveMode = MoveState.NONE;

    //
    public float walkSpeed,
                 sideFallDecel,
                 maxFallSpeed;

    // Current Velocity of Character
    public Vector2 velocity = Vector2.zero;

    //
    public int horizontalRays = 6,
               verticalRays = 4,
               rayMargin = 2;

    //
    public string groundLayer = "Ground";

    //
    private int groundLayerMask;

    /*
     * 
     */
    void Awake()
    {
        // Get Player Character
        PC = GetComponent<PlayerCharacter>();
        // Default Move State
        moveMode = MoveState.FALLING;
        //
        groundLayerMask = LayerMask.NameToLayer(groundLayer);
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
            // Jetpack Physics
            case MoveState.JETPACK:
                MoveJetpack(worldMovement);
                break;
            // Error
            default:
                return;
        }

        if (CheckHorizontalCollision())
        {
            velocity.x = 0;
        }
    }

    /*
     * 
     */
    protected void MoveWalking(Vector2 worldMovement)
    {
        // Current Position 2D
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        //
        Vector2 moveVector = worldMovement * walkSpeed;
        //
        rigidbody2D.MovePosition(position2D + moveVector * Time.fixedDeltaTime);
        //
        if (!CheckVerticalCollision())
        {
            velocity = moveVector;
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
        //
        velocity += velocity.y <= maxFallSpeed ? Vector2.zero : Physics2D.gravity;
        //
        if (velocity.x != 0)
        {
            //
            velocity.x += Mathf.Sign(velocity.x) * sideFallDecel;
        }
        
        //
        rigidbody2D.MovePosition(position2D + velocity * Time.fixedDeltaTime);
        //
        if (velocity.y < 0 && CheckVerticalCollision())
        {
            velocity.y = 0;
            moveMode = MoveState.WALKING;
        }
    }

    /*
     * 
     */
    protected void MoveJetpack(Vector2 worldMovement)
    {
        // Current Position 2D
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        //
        if (worldMovement.x != 0)
        {
            //
            velocity.x = transform.eulerAngles.y == 0f ? 5 : -5;
        }
        else
        {
            velocity.x = 0f;
        }
        //
        rigidbody2D.MovePosition(position2D + velocity * Time.fixedDeltaTime);
    }

    /*
     * 
     */
    protected bool CheckVerticalCollision()
    {
        //
        Vector2 startLoc = new Vector2(collider2D.bounds.min.x + rayMargin, collider2D.bounds.center.y);
        Vector2 endLoc = new Vector2(collider2D.bounds.max.x - rayMargin, collider2D.bounds.center.y);
        //
        for (int i = 0; i < verticalRays; i++)
        {
            //
            float lerpAmount = (float)i / ((float)verticalRays - 1);
            // 
            Vector2 rayOrigin = Vector2.Lerp(startLoc, endLoc, lerpAmount);
            //
            RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, Mathf.Sign(velocity.y) * Vector2.up, rayMargin, 1 << groundLayerMask);
            //
            if (hitInfo.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    /*
     * 
     */
    protected bool CheckHorizontalCollision()
    {
        //
        if (velocity.x != 0)
        {
            //
            Vector2 startLoc = new Vector2(collider2D.bounds.center.x, collider2D.bounds.min.y + rayMargin);
            Vector2 endLoc = new Vector2(collider2D.bounds.center.x, collider2D.bounds.max.y - rayMargin);
            //
            for (int i = 0; i < verticalRays; i++)
            {
                //
                float lerpAmount = (float)i / ((float)horizontalRays - 1);
                // 
                Vector2 rayOrigin = Vector2.Lerp(startLoc, endLoc, lerpAmount);
                //
                RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, Mathf.Sign(velocity.x) * Vector2.right, rayMargin, 1 << groundLayerMask);
                //
                if (hitInfo.collider != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
