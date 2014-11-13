using UnityEngine;
using System.Collections;

public class jsChildTriggerInterface : MonoBehaviour 
{
    // Reference to the parent object
    protected GameObject parentObject;

    #region MonoBehaviour

    void Awake()
    {
        // If we have a root
        if (transform.root != null)
            // Set parent object
            parentObject = transform.parent.gameObject;
        //
        if (parentObject == null)
            throw new UnityException("Parent object not set");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Send collision message to parent
        parentObject.SendMessage(System.Reflection.MethodBase.GetCurrentMethod().Name, other);
    }

    #endregion
}
