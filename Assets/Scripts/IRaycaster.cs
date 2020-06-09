using UnityEngine;

public interface IRaycaster
{
    bool castToPlane(Camera cam, out Vector3 hitPos);
}
