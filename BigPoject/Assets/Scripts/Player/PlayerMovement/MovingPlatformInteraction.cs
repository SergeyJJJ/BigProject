using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformInteraction : MonoBehaviour
{
    private GroundCheck _groundCheck = null;
    private CameraFollow _cameraFollow = null;


    private void Awake()
    {
        InitializeReferencesOnScripts();
    }


    private void InitializeReferencesOnScripts()
    {
        _groundCheck = gameObject.GetComponent<GroundCheck>();
        _cameraFollow = GameObject.FindGameObjectWithTag("DynamicCamera").GetComponent<CameraFollow>();
    }


    private void Update()
    {
        // If player is on the platform.
        if (_groundCheck.IsOnPlatform == true)
        {
            // Make player as a child of the platform.
            MakeChildOf(_groundCheck.OnWhatPlayerStanding.transform);

            // Disable Camera laziness.
            _cameraFollow.DisableCameraLaziness();
        }
        // If player isn`t already in the platform
        else
        {
            // Make player is now not as a child of the platform.
            MakeNotAChild();

            // Enable camera laziness.
            _cameraFollow.EnableCameraLaziness();
        }
    }


    private void MakeChildOf(Transform parent)
    {
        gameObject.transform.SetParent(parent);
    }


    private void MakeNotAChild()
    {
        gameObject.transform.SetParent(null);
    }
}
