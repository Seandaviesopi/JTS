using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class jsWaterResource : MonoBehaviour
{
    //
    public enum WaterResourceState
    {
        WATER,
        STEAM,
        MAX,
    }

    //
    public WaterResourceState waterState = WaterResourceState.WATER;
    //
    public bool bCanPickup = false;
    
    void FixedUpdate()
    {
        if (waterState == WaterResourceState.STEAM)
        {
            rigidbody2D.MovePosition((Vector2)transform.position + (-Physics2D.gravity / 2) * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (waterState == WaterResourceState.STEAM)
            {
                waterState = WaterResourceState.WATER;
            }

            bCanPickup = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!bCanPickup && other.gameObject.tag == "Player")
        {
            bCanPickup = true;
        }
    }
}
