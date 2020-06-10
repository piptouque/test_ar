using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPlaced;

    private Vector3[] _childrenStartingPositions;
    private float _explosionPower;
    
    void Awake()
    {
        /* adding swiping event listener */
        UserInterface.OnPinches += UserInterface_OnPinchesExplode;
    }

    void Start()
    {
        SetChildrenStartingPositions();
        _explosionPower = 0.0f;
    }

    public void BackToCalibration()
    {
        ResetObject();
        ApplicationManager.DemandLoadCalibration();
    }

    public void GoToUITest()
    {
        ApplicationManager.DemandLoadUITest();
    }

    private void SetChildrenStartingPositions()
    {
        Transform parent = objectPlaced.transform;
        _childrenStartingPositions = new Vector3[parent.childCount];
        for (int i = 0; i < parent.childCount; ++i)
        {
            _childrenStartingPositions[i] = parent.GetChild(i).localPosition;
        }
    }

    private void ResetObject()
    {
        Transform parent = objectPlaced.transform;
        for (int i = 0; i < parent.childCount; ++i)
        {
            parent.GetChild(i).localPosition = _childrenStartingPositions[i];
        }
        
    }

    private void ExplodeObject(float power)
    {
        _explosionPower = Math.Max(0.0f, power + _explosionPower);
        Transform parent = objectPlaced.transform;
        for (int i = 0; i < parent.childCount; ++i)
        {
            parent.GetChild(i).localPosition = _childrenStartingPositions[i] * (float) Math.Sqrt(1 + _explosionPower);
        }
    }

    private void UserInterface_OnPinchesExplode(List<Pinch> pinches)
    {
        if (!gameObject.activeSelf)
        {
            /* nothing to do if de-activated */
            return;
        }
        if (pinches.Count == 0)
        {
            throw new ArgumentException("Pinches should exist before triggering listener.");
        }
        ExplodeObject(pinches[0].ToExplosition());
    }
}
