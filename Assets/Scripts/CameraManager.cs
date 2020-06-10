using UnityEngine;

    public class CameraManager : MonoBehaviour
    {
        void Start()
        {
            if (!Application.isMobilePlatform)
            {
                gameObject.AddComponent(typeof(CameraMovement));
            }
        }
    }
