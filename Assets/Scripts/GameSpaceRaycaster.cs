using UnityEngine;

public class GameSpaceRaycaster : IRaycaster
{
    public bool castToPlane(Camera cam, out Vector3 hitPos)
    {
        if (cam == null)
        {
            throw new UnityException("No Camera present in the current scene.");
        }

        bool isPositionValid;
        Vector3 screenCentre = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.0f)); /* z-coord value null ? */
        Ray ray = cam.ScreenPointToRay(screenCentre);
        RaycastHit hit;
        isPositionValid = Physics.Raycast(ray.origin, ray.direction, out hit, cam.farClipPlane);
        isPositionValid &= hit.normal == Vector3.up; // only ground horizontal planes
        hitPos = isPositionValid ? hit.point : Vector3.zero;
        return isPositionValid;
    }
}
