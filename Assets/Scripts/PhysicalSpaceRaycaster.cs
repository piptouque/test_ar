
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PhysicalSpaceRaycaster : IRaycaster
{
    private ARRaycastManager _manager;

    public PhysicalSpaceRaycaster(ARRaycastManager manager)
    {
        _manager = manager;
    }

    public bool castToPlane(Camera cam, out Vector3 hitPos)
    {
        Vector3 screenCentre = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.0f)); /* z-coord value null ? */
        var hits = new List<ARRaycastHit>();
        bool isPositionValid = _manager.Raycast(screenCentre, hits, TrackableType.Planes);
        hitPos = isPositionValid ? hits[0].pose.position : Vector3.zero;
        return isPositionValid;
    }
}
