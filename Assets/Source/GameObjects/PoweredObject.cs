using UnityEngine;
using System.Collections;

abstract public class PoweredObject : MonoBehaviour 
{
    //
    public SteamEngine poweringObject;
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
    public void SetOwner(SteamEngine newOwner)
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
