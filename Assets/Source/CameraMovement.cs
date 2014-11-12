using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
    // Damp movement
    public float dampTime = 0.3f;
    // Target actor of Camera
    protected Transform targetActor;
    //
    protected Bounds cameraBounds;
    // Current Velocity
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        // Get Player
        targetActor = GameObject.FindGameObjectWithTag("Player").transform;
        //
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        //
        if (background != null)
        {
            cameraBounds = background.renderer.bounds;
        }
    }

	void FixedUpdate () 
    {
        // Valid
        if (targetActor != null) 
        {
            
            // Get Half Screen Location of target
            Vector3 cameraPoint = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            // Vector Distance
            Vector3 distance = targetActor.position - cameraPoint;
            // Calculate Camera Destination
            Vector3 destination = transform.position + distance;
            //
            destination.x = Mathf.Clamp(destination.x, cameraBounds.min.x + camera.orthographicSize, cameraBounds.max.x - camera.orthographicSize);
            destination.y = transform.position.y;
            destination.z = transform.position.z;
            //
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            
        }
	}
}
