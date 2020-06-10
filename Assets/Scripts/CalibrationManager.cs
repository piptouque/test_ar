using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;


public class CalibrationManager : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectWrapper;
    
    private ARSessionOrigin _arOrigin;
    private ARRaycastManager _arOriginRaycast;

    private Pose _placementPose;
    private bool _placementPoseIsValid;
    private Pose _placementPoseCurrent;
    private bool _placementPoseCurrentIsValid;
    private bool _positionIsLocked;

    private IRaycaster _raycaster;

    void Awake()
    {
        /* adding swiping event listener */
        UserInterface.OnSwipes += UserInterface_OnSwipesRotate;
        
        _arOrigin = FindObjectOfType<ARSessionOrigin>();
        _arOriginRaycast = _arOrigin.GetComponent<ARRaycastManager>();
        
        if (Application.isMobilePlatform)
        {
            _raycaster = new PhysicalSpaceRaycaster(_arOriginRaycast);
        }
        else
        {
            _raycaster = new GameSpaceRaycaster();
        }
        
        ResetCalibration();
    }

    void Start()
    {
        
        ResetCalibration();
    }

    private void GoToGame()
    {
        ApplicationManager.DemandLoadGame();
    }

    public void ConfirmObject()
    {
        if (_placementPoseCurrentIsValid)
        {
            GoToGame();
        }
    }

    public void ResetCalibration()
    {
        /* should be called by the 'cancel' button */

        /* default values */
        _placementPose = new Pose(Vector3.zero, Quaternion.identity);
        _placementPoseCurrent = _placementPose;
        _positionIsLocked = false;
        _placementPoseIsValid = false;
        _placementPoseCurrentIsValid = false;
        HideObject();
    }

    public void ToggleLockObject()
    {
        if (_placementPoseIsValid)
        {
            _positionIsLocked = !_positionIsLocked;
        }
    }
    
    void Update()
    {
        UpdatePositionPose();
        UpdatePlacementIndicator();

        if (!_placementPoseCurrentIsValid)
        {
            HideObject();
        }
        else
        {
            PlaceObject();
            ShowObject();
        }
    }

    private void HideObject()
    {
        objectWrapper.SetActive(false);
    }

    private void ShowObject()
    {
        objectWrapper.SetActive(true);
    }

    private void PlaceObject()
    {
        objectWrapper.transform.SetPositionAndRotation(_placementPoseCurrent.position, _placementPose.rotation);
    }
    

    private void UpdatePlacementIndicator()
    {
        if (_placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
        else
        {
           placementIndicator.SetActive(false); 
        }
    }

    private bool ComputePositionPose(out Vector3 pos)
    {
        return _raycaster.castToPlane(Camera.main, out pos);
    }

    private void UpdatePositionPose()
    {
        Vector3 pos;
        _placementPoseIsValid = ComputePositionPose(out pos);
        if (_placementPoseIsValid)
        {
            _placementPose.position = pos;
            _placementPoseCurrentIsValid = true;
        }

        if (!_positionIsLocked)
        {
            _placementPoseCurrent.position = pos;
        }
    }

    private void UpdateRotationPose(Quaternion rotSwipe)
    {
        _placementPose.rotation *= rotSwipe;
    }


    private void UserInterface_OnSwipesRotate(List<Swipe> swipes)
    {
        if (!gameObject.activeSelf)
        {
            /* nothing to do if de-activated */
            return;
        }
        /*
         * todo: find better way to choose direction
         * right now we just take the 'fastest swipe'
         * since we only need one to rotate our object.
         */
        if (swipes.Count == 0)
        {
            throw new ArgumentException("No swipe to rotate from.");
        }
        var maxSpeedSwipe = swipes.Aggregate((max, swipe) => swipe.Speed > max.Speed ? swipe : max);
        var rotSwipe = maxSpeedSwipe.ToRotation();
        /* rotating the object further according to this swipe */
        UpdateRotationPose(rotSwipe);
    }
}