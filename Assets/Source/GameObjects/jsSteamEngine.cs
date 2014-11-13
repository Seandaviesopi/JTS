using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class jsSteamEngine : MonoBehaviour 
{
    // Objects Powered by this Engine
    public jsPoweredObject poweredObject;
    // 
    public GameObject waterResource;
    //
    public Transform waterSpawn;
    //
    private bool bIsPowered = false;

    //
    void Awake()
    {
        waterSpawn = transform.FindChild("WaterSpawn");
    }

    //
    public void PowerFinished()
    {
        if (waterResource)
        {
            //
            bIsPowered = false;
            //
            GameObject.Instantiate(waterResource, waterSpawn.position, new Quaternion());
        }
    }

    // Collision Enter
    void OnTriggerStay2D(Collider2D other)
    {
        //
        if (!bIsPowered)
        {
            //
            jsWaterResource newWaterResource = other.gameObject.GetComponent<jsWaterResource>();
            // If water resource
            if (newWaterResource && newWaterResource.bCanPickup)
            {
                // Destroy Other
                Destroy(other.gameObject);
                //
                poweredObject.StartPower();
                //
                bIsPowered = true;
            }
        }
    }
}
