using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerCharacter : MonoBehaviour 
{
    // <summary>
    //      Character Component References
    // </summary>
    [HideInInspector]
    public CharacterController2D movementController;

    #region movement variables

    // <summary>
    //      Strings that store the names of used Axis
    // </summary>
    public string horizontalAxis = "Horizontal";

    // Scale of Gravity on Character
    public float gravityScale;

    // Speed the Character moves on ground
    public float groundSpeed;

    // <summary>
    //      Values used for storing current and previous velocity accumulations
    // </summary>
    protected Vector2 velocityValue, 
                      lastVelocityValue;

    #endregion

    //
    protected int waterAmount = 0;

    void Awake()
    {
        movementController = GetComponent<CharacterController2D>();
    }

    void FixedUpdate()
    {
        // Get Horizontal Input
        float horizontalInput = Input.GetAxis(horizontalAxis);
        // If Horizontal Input
        if (horizontalInput != 0f)
        {
            // Move Left or Right
            MoveRight(horizontalInput);
        }


        Vector2 velocityMovement = ConsumeMovementVector() * Time.deltaTime;

        if (movementController.isGrounded)
        {
            movementController.velocity.y = 0;
        }
        else
        {
            velocityMovement += Physics2D.gravity * gravityScale * Time.deltaTime;
        }

        movementController.move(velocityMovement);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If water resource
        if (other.gameObject.tag == "WaterResource")
        {
            // Increment Water Amount
            waterAmount++;
            // Destroy Other
            Destroy(other.gameObject);
        }
    }

    public Vector2 ConsumeMovementVector()
    {
        // Cache vector value
        lastVelocityValue = velocityValue;
        // Zero out Current Value
        velocityValue = Vector2.zero;
        // Return Cached Value
        return lastVelocityValue;
    }

    protected void AddMovementInput(Vector2 worldVelocity, float inputScale)
    {
        // Add scaled direction vector
        velocityValue += worldVelocity * inputScale;
    }

    private void MoveRight(float inputAmount)
    {
        // Change look rotation
        transform.eulerAngles = inputAmount < 0 ? new Vector3(0f, 180f, 0f) : new Vector3(0f, 0f, 0f);
        // Calculate Movement Velocity
        Vector2 velocityMovement = Vector2.right * groundSpeed;
        // Add right or left movement input
        AddMovementInput(velocityMovement, inputAmount);
    }
}
