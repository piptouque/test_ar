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

    protected void LookForSwipes()
    {
        var swipes = FindSwipes();
        if (swipes.Count > 0)
        {
            SendSwipes(swipes);
        }
    }

    protected void LookForPinches()
    {
        var pinches = FindPinches();
        if (pinches.Count > 0)
        {
            SendPinches(pinches);
        }
    }

    protected abstract List<Swipe> FindSwipes();
    protected abstract List<Pinch> FindPinches();

    private void SendSwipes(List<Swipe> swipes)
    {
        OnSwipes(swipes);
    }

    private void SendPinches(List<Pinch> pinches)
    {
        OnPinches(pinches);
    }
}