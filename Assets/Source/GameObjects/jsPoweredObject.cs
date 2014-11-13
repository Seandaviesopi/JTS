using UnityEngine;
using System.Collections;

abstract public class jsPoweredObject : MonoBehaviour 
{
    //
    public jsSteamEngine poweringObject;
    //
    protected bool bIsPowered;

    void Update()
    {
        //
        if (bIsPowered)
        {
            ExecutePower();
        }
    }

    //
    public void SetOwner(jsSteamEngine newOwner)
    {
        poweringObject = newOwner;
    }

    //
    virtual public void StartPower()
    {
        bIsPowered = true;
    }

    //
    virtual public void StopPower()
    {
        //
        bIsPowered = false;
        //
        poweringObject.PowerFinished();
    }

    //
    abstract protected void ExecutePower();
}
