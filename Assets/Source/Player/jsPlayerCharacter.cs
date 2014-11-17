using UnityEngine;
using System.Collections;

public class jsPlayerCharacter : MonoBehaviour 
{
    //
    public jsPlayerController2D characterMovement;
    //
    public BoxCollider2D boxCollision;
    //
    public CircleCollider2D circleCollision;

    // Water Resource Prefab
    public GameObject waterResource;

    //
    public float waterThrowVelocity;

    //
    public int waterAmount = 0;

    #region jetpack variables

    public float jetpackTileSize;

    protected bool bWantsJetpack = false,
                   bActiveJetpack = false;

    protected Vector2 jetStart = Vector2.zero;

    #endregion

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

    #region MonoBehaviour

    void Awake()
    {
        characterMovement = GetComponent<jsPlayerController2D>();

        boxCollision = GetComponent<BoxCollider2D>();

        circleCollision = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExecuteClickInteraction();
        }

        //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStartJetpack();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            OnStopJetpack();
        }
        //
        MoveJetpack();
        // Get Horizontal Input
        float horizontalInput = Input.GetAxis(horizontalAxis);
        // If Horizontal Input
        if (horizontalInput != 0f)
        {
            // Move Left or Right
            MoveRight(horizontalInput);
        }

        Vector2 velocityMovement = ConsumeMovementVector() * Time.deltaTime;

        if (!characterMovement.isGrounded)
        {
            velocityMovement += Physics2D.gravity * gravityScale * Time.deltaTime;
        }

        characterMovement.Move(velocityMovement);
    }

    void FixedUpdate()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (characterMovement.isGrounded)
        {
            //
            jsWaterResource newWaterResource = other.gameObject.GetComponent<jsWaterResource>();
            // If water resource
            if (newWaterResource)
            {
                // Increment Water Amount
                waterAmount++;
                // Destroy Other
                Destroy(other.gameObject);
            }
        }
    }

    #endregion

    #region Jetpack Functions

    protected void OnStartJetpack()
    {
        bWantsJetpack = true;
    }

    protected void OnStopJetpack()
    {
        bWantsJetpack = false;
    }

    protected void MoveJetpack()
    { 
        if (bWantsJetpack && !bActiveJetpack && waterAmount != 0)
        {
            bActiveJetpack = true;
            jetStart = transform.position;
        }

        if (bActiveJetpack)
        {
            if (Vector2.Distance(jetStart, transform.position) >= jetpackTileSize)
            {
                Vector2 spawnLocation = transform.eulerAngles.y == 0f ? new Vector2(renderer.bounds.min.x, renderer.bounds.min.y) : new Vector2(renderer.bounds.max.x, renderer.bounds.min.y);

                DropWaterResource(spawnLocation, (spawnLocation - (Vector2)transform.position).normalized * 100f);

                if (bWantsJetpack && waterAmount != 0)
                {
                    jetStart = new Vector2(transform.position.x, jetStart.y + jetpackTileSize);
                }
                else
                {
                    bActiveJetpack = false;
                }
            }
            else
            {
                AddMovementInput(Vector2.up * 25f, 1f);
            }
        }
    }

    #endregion

    #region Movement Functions

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

    #endregion

    protected void ExecuteClickInteraction()
    {
        //
        if (waterAmount != 0)
        {
            //
            Vector2 mouseClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //
            Vector2 spawnVelocity = (mouseClick - (Vector2)transform.position).normalized;
            //
            Vector2 spawnLocation = mouseClick.x > transform.position.x ? (Vector2)transform.position + Vector2.right * 2 : (Vector2)transform.position + -Vector2.right * 2;
            //
            GameObject waterResource = DropWaterResource(spawnLocation, spawnVelocity * waterThrowVelocity);
        }
    }

    protected GameObject DropWaterResource(Vector2 spawnLocation, Vector2 spawnVelocity)
    {
        //
        waterAmount--;
        //
        GameObject newWaterResource = (GameObject)Instantiate(waterResource, spawnLocation, new Quaternion());
        //
        newWaterResource.rigidbody2D.AddForce(spawnVelocity);
        //
        return newWaterResource;
    }
}
