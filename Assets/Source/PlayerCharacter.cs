using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCharacter : MonoBehaviour 
{
    // Character Movement Component
    [HideInInspector]
    public PlayerMovement characterMovement;
    // Axis Names
    public string horizontalAxis = "Horizontal";
    // Axis Input Values
    protected Vector2 inputValue, lastInputValue;
    // Flip flop bool for jetpack use
    private bool bPressedJetpack;

    /*
     * 
     */
    void Awake()
    {
        characterMovement = GetComponent<PlayerMovement>();
    }

    /*
     * 
     */
    void FixedUpdate()
    {
        //
        if (Input.GetKey(KeyCode.Space))
            characterMovement.velocity.y = 5;
        // Horizontal Input Check
        float horizontalInput = Input.GetAxis(horizontalAxis);
        if (horizontalInput != 0)
            MoveRight(horizontalInput);
        // Collect Cumulative Input Value
        Vector2 finalInput = ConsumeMovementInput();
        // Give Final Move input to Movement Component
        characterMovement.PerformMovement(finalInput);
    }

    /*
     * 
     */
    public Vector2 ConsumeMovementInput()
    {
        // Cache input value
        lastInputValue = inputValue;
        // Zero out Current Value
        inputValue = Vector2.zero;
        // Return Cached Value
        return lastInputValue;
    }

    /*
     * 
     */
    protected void AddMovementInput(Vector2 worldDirection, float inputScale)
    {
        // Add scaled direction vector
        inputValue += worldDirection * inputScale;
    }

    /*
     * 
     */
    private void MoveRight(float inputAmount)
    {
        // Change look rotation
        transform.eulerAngles = inputAmount < 0 ? new Vector3(0f, 180f, 0f) : new Vector3(0f, 0f, 0f);
        // Add right or left movement input
        AddMovementInput(Vector2.right, inputAmount);
    }
}
