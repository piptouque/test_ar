using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        /* adding swiping event listener */
        UserInterface.OnPinches += UserInterface_OnPinchesExplode;
    }


    private void UserInterface_OnPinchesExplode(List<Pinch> pinches)
    {
        
    }
}
