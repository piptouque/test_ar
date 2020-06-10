using System;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    private void Awake()
    {
        /*
         * If we are on mobile
         * we control through mouse and keyboard
         */
        if (Application.isMobilePlatform)
        {
           gameObject.AddComponent(typeof(TouchInterface));
        }
        else
        {
           gameObject.AddComponent(typeof(MouseInterface));
        }
    }
}
