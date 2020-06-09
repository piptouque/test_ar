using UnityEngine;

public struct Swipe
{
    /* the Dir is on the (x, z) plane in 2D */
    public Vector2 Dir { get; }
    public float Speed { get; }
    
    public Swipe(Vector2 dir, float speed)
    {
        var normalised = dir.normalized;
        /* around y axis */
        Dir = dir;
        Speed = speed;
    }
    
    public Vector2 AsLateral()
    {
        return new Vector2(Dir.x * Speed, 0.0f);
    }

    public Vector2 AsVertical()
    {
        return new Vector2(0.0f, Dir.y * Speed);
    }

    public Quaternion ToRotation()
    {
        /* around y axis */
        var rot = Quaternion.AngleAxis((Dir.x - Dir.y) * Speed, Vector3.up);
        return rot;
    }
}
