using UnityEngine;
using System.Collections;

abstract public class PoweredObject : MonoBehaviour 
{
    //
    protected SteamEngine poweringObject;
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
    public void StartPower()
    {
        bIsPowered = true;
    }

    //
    public void StopPower()
    {
        //
        bIsPowered = false;
        //
        poweringObject.PowerFinished();
    }

    //
    abstract protected void ExecutePower();
}
