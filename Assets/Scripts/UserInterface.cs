using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class UserInterface : MonoBehaviour
{
    public static event Action<List<Swipe>> OnSwipes = delegate { };
    public static event Action<List<Pinch>> OnPinches = delegate { };

    // Update is called once per frame
    void Update()
    {
        LookForSwipes();
        LookForPinches();
    }

    protected abstract void LookForSwipes();

    protected abstract void LookForPinches();

    protected void SendSwipes(List<Swipe> swipes)
    {
        OnSwipes(swipes);
    }

    protected void SendPinches(List<Pinch> pinches)
    {
        OnPinches(pinches);
    }
}