﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [Range(0f, 0.4f)] [SerializeField] private float _laziness = 0f;
    [SerializeField] private bool _lookAtTarget = true;
    [SerializeField] private bool _takeOffsetFromInitialPos = true;
    [SerializeField] private Vector3 _generalOffset = Vector3.zero;
    private Vector3 _whereCameraShouldBe = Vector3.zero;
    private bool _warningAlreadyShown = false;
    private Vector3 _currentVelocity = Vector2.zero;


    public Transform Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;
        }
    }


    private void Start()
    {
        if (_takeOffsetFromInitialPos && _target != null) _generalOffset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            _whereCameraShouldBe = _target.position + _generalOffset;
            transform.position = Vector3.SmoothDamp(transform.position, _whereCameraShouldBe,
                                                          ref _currentVelocity, _laziness);

            if (_lookAtTarget)
            {
                transform.LookAt(_target);
            }
        } 
        else
        {
            if (!_warningAlreadyShown)
            {
                Debug.Log("Warning: You should specify a target in the simpleCamFollow script.", gameObject);
                _warningAlreadyShown = true;
            }
        }
    }
}
