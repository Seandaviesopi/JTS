using UnityEngine;
using System.Collections;

public class WaterPump : PoweredObject 
{
    public Transform pumpLocOne, pumpLocTwo;

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
            WaterResource newWaterResource = other.gameObject.GetComponent<WaterResource>();
            // If water resource
            if (newWaterResource && newWaterResource.bCanPickup)
            {
                //
                Instantiate(other.gameObject, pumpLocTwo.position, new Quaternion());
                // Destroy Other
                Destroy(other.gameObject);
                //
                bCanPumpWater = false;
            }
        }
    }
}
