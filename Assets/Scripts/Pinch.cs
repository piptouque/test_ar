using System;
using UnityEngine;

public class Pinch
{
    public Vector2 Dir { get; }
    public float Power { get; }
    
    public Pinch(Vector2 dir, float power)
    {
        Dir = dir.normalized;
        Power = power;
    }
    
    public float ToExplosition()
    {
        return Math.Sign(Dir.x * Dir.y) * Power;
    }
}
