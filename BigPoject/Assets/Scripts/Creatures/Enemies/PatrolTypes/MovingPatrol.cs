using System.Collections.Generic;
using UnityEngine;

namespace Enemies.PatrolTypes
{
    public abstract class MovingPatrol : Patrol
    {
        [SerializeField] private float _patrolSpeed = 0f;                        // Enemy patrolling speed.
        [SerializeField] private float _timeToStayOnPoint = 0f;                  // Define how long enemy will stay on the point before he will move to next point. 
        [SerializeField] private List<Transform> _patrolTrajectoryPoints = null; // Contains path points along which the enemy moves.
        private Vector2 _currentTargetPoint = Vector2.zero;                      // Current target point.
        private int _currentPointIndex = 0;                                      // Target point index.
        private float _stayOnPointTimer = 0f;                                    // Timer that control how long enemy will stand.
        private bool _isWaitingOnPoint = false;                                  // Check if enemy is now waiting.
        
        #region Properties
        
        protected float PatrolSpeed => _patrolSpeed;

        protected float TimeToStayOnPoint => _timeToStayOnPoint;

        protected Vector2 CurrentTargetPoint
        {
            get => _currentTargetPoint;
            set => _currentTargetPoint = value;
        }

        protected float StayOnPointTimer
        {
            get => _stayOnPointTimer;
            set => _stayOnPointTimer = value;
        }

        protected bool IsWaitingOnPoint
        {
            get => _isWaitingOnPoint;
            set => _isWaitingOnPoint = value;
        }

        #endregion Properties

        protected abstract void Move();
        
        protected abstract bool IsPointReached();

        protected void ChangeTargetPoint()
        {
            _currentTargetPoint = GetNextPoint();
        }
        
        
        protected Vector2 GetNextPoint()
        {
            // Here will be stored point to be returned.
            Vector2 nextPoint = Vector2.zero;

            // If current point index is equal to points amount,
            // reset point index to zero and set first point to return.
            if (_currentPointIndex == _patrolTrajectoryPoints.Count - 1)
            {
                _currentPointIndex = 0;
                nextPoint = _patrolTrajectoryPoints[_currentPointIndex].position;
            }
            // If it is not, than increment point index and set next point to return.
            else
            {
                nextPoint = _patrolTrajectoryPoints[_currentPointIndex + 1].position;
                _currentPointIndex++;
            }

            // return index.
            return nextPoint;
        }
        
        
        private bool IsPointsExist()
        {
            return _patrolTrajectoryPoints != null;
        }
        

        protected bool IsTargetPointToTheRight()
        {
            return transform.position.x < _currentTargetPoint.x;
        }


        protected bool IsTargetPointToTheLeft()
        {
            return transform.position.x > _currentTargetPoint.x;
        }


        private void Awake()
        {
            if (IsPointsExist())
            {
                _currentTargetPoint = _patrolTrajectoryPoints[0].position;
            }

            _stayOnPointTimer = _timeToStayOnPoint;
        }
    }
}
