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
    
    void FixedUpdate()
    {
        if (waterState == WaterResourceState.STEAM)
        {
            rigidbody2D.MovePosition((Vector2)transform.position + (-Physics2D.gravity / 2) * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "CeilingTile")
        {
            if (waterState == WaterResourceState.STEAM)
            {
                waterState = WaterResourceState.WATER;
            }
        }
    }
}
