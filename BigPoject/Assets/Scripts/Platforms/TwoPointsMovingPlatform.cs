using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointsMovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _firstTargetPoint = null;         // Contains the first point to which the platform will move.
    [SerializeField] private Transform _secondTargetPoint = null;        // Contains the second point to which the platform will move.
    [SerializeField] private float _movementSpeed = 0;                   // Contains speed of platform movement;
    private Vector2 _firstTargetPosition = Vector2.zero;                 // Contains position of first target.
    private Vector2 _secondTargetPosition = Vector2.zero;                // Contains position of second target.
    

    private void Awake()
    {
        InitializePositions();
    }


    private void FixedUpdate()
    {
        if (IsTargetPointsExist())
        {
            Move();    
        }
    }


    private void InitializePositions()
    {
        _firstTargetPosition = _firstTargetPoint.position;
        _secondTargetPosition = _secondTargetPoint.position;
    }   


    protected virtual void Move()
    {    
        Vector2 positionHolder = Vector2.zero;
        Vector2 currentPosition = gameObject.transform.position;
        float threshold = 0.1f;

        if (Vector2.Distance(currentPosition, _firstTargetPosition) < threshold)
        {
            positionHolder = _firstTargetPosition;
            _firstTargetPosition = _secondTargetPosition;
            _secondTargetPosition = positionHolder;
        }

        float step = _movementSpeed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(currentPosition, _firstTargetPosition, step);
    }


    protected bool IsTargetPointsExist()
    {
        return (_firstTargetPoint != null) && (_secondTargetPoint != null);
    } 


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(gameObject.transform);
        }    
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }  
    }
}
