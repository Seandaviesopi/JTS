using UnityEngine;
using System.Collections;

public class jsWaterPump : jsPoweredObject 
{
    public Transform pumpLocOne, pumpLocTwo;

    // Water Resource Prefab
    public GameObject waterResource;

    public Vector2 spawnVelocity = Vector2.zero;

    public bool bPumpRight = true;

    public float tickTime;

    public int maxTicks;

    protected bool bCanPumpWater = false;

    protected float currentTime = 0f, 
                    timerInterval = 0f;

    protected int currentTicks = 0;

    public override void StopPower()
    {
        base.StopPower();

        currentTime = 0f;
        timerInterval = 0f;
        currentTicks = 0;
    }

    protected override void ExecutePower()
    {
        // Increase timer
        currentTime += Time.deltaTime;
        // If timer interval
        if (currentTime >= timerInterval + tickTime)
        {
            // Increment Ticks
            currentTicks++;
            // If max ticks
            if (currentTicks == maxTicks)
            {
                // Stop Power
                StopPower();
                return;
            }
            //
            bCanPumpWater = true;
            //
            timerInterval = currentTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (bCanPumpWater)
        {
            //
            jsWaterResource otherWaterResource = other.gameObject.GetComponent<jsWaterResource>();
            // If water resource
            if (otherWaterResource)
            {
                // Destroy Other
                Destroy(other.gameObject);
                //
                GameObject newWaterResource = (GameObject)Instantiate(waterResource, pumpLocTwo.position, new Quaternion());
                // 
                newWaterResource.rigidbody2D.AddForce(new Vector2(Random.Range(-spawnVelocity.x, spawnVelocity.x), spawnVelocity.y));
                //
                bCanPumpWater = false;
            }
        }
    }
}
