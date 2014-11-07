using UnityEngine;
using System.Collections;

public class WaterPump : PoweredObject 
{
    public Transform pumpLocOne, pumpLocTwo;

    public bool bPumpRight = true;

    public int maxPumpAmount;

    protected int currentPumpAmount = 0;

    protected override void ExecutePower()
    {
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //
        WaterResource newWaterResource = other.gameObject.GetComponent<WaterResource>();
        // If water resource
        if (newWaterResource && newWaterResource.bCanPickup)
        {
            // Increment Water Amount
            currentPumpAmount++;
            //
            Instantiate(other.gameObject, pumpLocTwo.position, new Quaternion());
            // Destroy Other
            Destroy(other.gameObject);
            
        }
    }
}
