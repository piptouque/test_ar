using System;
using UnityEngine;

public struct Swipe
{
    /* the Dir is on the (x, z) plane in 2D */
    public Vector2 Dir { get; }
    public float Speed { get; }
    
    public Swipe(Vector2 dir, float speed)
    {
        /* around y axis */
        Dir = dir.normalized;
        Speed = speed;
    }
    
    public Quaternion ToRotation()
    {
        /* around y axis */
        var rot = Quaternion.AngleAxis((Dir.x - Dir.y) * Speed, Vector3.up);
        return rot;
    }
}
