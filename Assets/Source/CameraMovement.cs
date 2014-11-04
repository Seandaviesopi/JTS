using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
    public Transform targetActor;

    public float dampTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

	void FixedUpdate () 
    {
        if (targetActor) 
        {
            Vector3 point = camera.WorldToViewportPoint(targetActor.position);
            Vector3 delta = targetActor.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            destination.y = transform.position.y;
            destination.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
     }
	}
}
