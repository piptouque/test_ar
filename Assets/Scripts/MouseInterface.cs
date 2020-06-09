using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseInterface : UserInterface 
{
    override protected void LookForSwipes()
    {
        var swipes = new List<Swipe>();
        bool isSwiped = (Input.GetMouseButton(0) && !Input.GetMouseButton(1)) || (Input.GetMouseButton(1) && !Input.GetMouseButton(0));
        if (isSwiped)
        {
           swipes.Add(ComputeSwipe()); 
        }
        if (swipes.Count > 0)
        {
            SendSwipes(swipes);
        }
    }

    override protected void LookForPinches()
    {
        var pinches = new List<Pinch>();
        bool isPinched = (Input.GetMouseButton(0) && Input.GetMouseButton(1));
        if (isPinched)
        {
            pinches.Add(ComputePinch());
        }
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
    }
}

