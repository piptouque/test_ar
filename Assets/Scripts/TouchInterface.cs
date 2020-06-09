using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInterface : UserInterface 
{

    override protected void LookForSwipes()
    {
        var swipes = new List<Swipe>();
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
               swipes.Add(ComputeSwipe(touch)); 
            }
        }

        if (swipes.Count > 0)
        {
            SendSwipes(swipes);
        }
    }

    protected override void LookForPinches()
    {
        throw new NotImplementedException();
    }

    private Swipe ComputeSwipe(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
        {
            throw new ArgumentException("Touch must move for swipe to occur.");
        }
        Vector2 dir = touch.deltaPosition.normalized;
        float speed = touch.deltaPosition.magnitude / touch.deltaTime;
        return new Swipe(dir, speed);
    }
}

