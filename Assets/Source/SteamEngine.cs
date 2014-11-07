using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class SteamEngine : MonoBehaviour 
{
    // Objects Powered by this Engine
    public PoweredObject poweredObject;
    // 
    public GameObject waterResource;
    //
    public Transform waterSpawn;

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
            GameObject.Instantiate(waterResource, waterSpawn.position, new Quaternion());
        }
    }

    // Collision Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        //
        WaterResource newWaterResource = other.gameObject.GetComponent<WaterResource>();
        // If water resource
        if (newWaterResource && newWaterResource.bCanPickup)
        {
            // Destroy Other
            Destroy(other.gameObject);
            //
            poweredObject.StartPower();
        }
    }
}
