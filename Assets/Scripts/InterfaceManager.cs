using System;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    private void Awake()
    {
        /*
         * If we are in the editor
         * we control through mouse and keyboard
         */
        if (Application.isEditor)
        {
           gameObject.AddComponent(typeof(MouseInterface));
        }
        else
        {
           gameObject.AddComponent(typeof(TouchInterface));
        }
    }
}
