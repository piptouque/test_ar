using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseInterface : UserInterface 
{
    override protected List<Swipe> FindSwipes()
    {
        var swipes = new List<Swipe>();
        bool isSwiped = (Input.GetMouseButton(0) && !Input.GetMouseButton(1)) || (Input.GetMouseButton(1) && !Input.GetMouseButton(0));
        if (isSwiped)
        {
           swipes.Add(ComputeSwipe()); 
        }
        return swipes;
    }

    override protected List<Pinch> FindPinches()
    {
        var pinches = new List<Pinch>();
        bool isPinched = (Input.GetMouseButton(0) && Input.GetMouseButton(1));
        if (isPinched)
        {
            pinches.Add(ComputePinch());
        }
        return pinches;
    }

    private Swipe ComputeSwipe()
    {
        Vector2 dir = new Vector2(1.0f, 0.0f);
        float speed = Input.mouseScrollDelta.y / Time.deltaTime;
        return new Swipe(dir, speed);
    }

    private Pinch ComputePinch()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float power = dir.magnitude;
        dir.Normalize();
        return new Pinch(dir, power);
    }
}

