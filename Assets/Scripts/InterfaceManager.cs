using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InterfaceManager : MonoBehaviour
    {
        private UserInterface _interface;

        private void Awake()
        {
            /*
             * If we are in the editor
             * we control through mouse and keyboard
             */
            if (Application.isEditor)
            {
               _interface = new MouseInterface(); 
            }
            else
            {
                _interface = new TouchInterface();
            }
        }
    }
}