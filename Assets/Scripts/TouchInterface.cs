using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchInterface : UserInterface 
{

    override protected List<Swipe> FindSwipes()
    {
        var swipes = new List<Swipe>();
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
               swipes.Add(ComputeSwipe(touch)); 
            }
        }
        return swipes;
    }

    protected override List<Pinch> FindPinches()
    {
        var pinches = new List<Pinch>();
        Touch[] touchesMoved = Input.touches.Where(touch => touch.phase == TouchPhase.Moved).ToArray();
        if (touchesMoved.Length < 2)
        {
            return pinches;
        }
        ComputePinch(Input.touches[0], Input.touches[1]);
        return pinches;
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

    private Pinch ComputePinch(Touch t1, Touch t2)
    {
        if (t1.phase != TouchPhase.Moved || t2.phase != TouchPhase.Moved)
        {
            throw new ArgumentException("Touch must move for pinch to occur.");
        }

        Vector2 delta = t2.deltaPosition - t1.deltaPosition;
        Vector2 dir = delta.normalized;
        float power = delta.magnitude;
        return new Pinch(dir, power);
    }
}

