using System.Collections.Generic;
using UnityEngine;

namespace Enemies.PatrolTypes
{
    public abstract class MovingPatrol : Patrol
    {
        [SerializeField] protected float _patrolSpeed = 0f;                       // Enemy patrolling speed.
        [SerializeField] private List<Transform> _patrolTrajectoryPoints = null;  // Contains path points along which the enemy moves.
        protected Vector2 _currentTargetPoint = Vector2.zero;                     // Current target point.
        private int _currentPointIndex = 0;                                       // Target point index.
        private Rigidbody2D _rigidbody2D = null;                                  // Enemy Rigidbody2D component.                   
        
        protected abstract void Move();


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
        
        
        protected bool IsPointsExist()
        {
            return _patrolTrajectoryPoints != null;
        }


        protected bool IsReachedPoint()
        {
            float threshold = 0.1f;

            // If distance between platform and target point is less than allowable threshold
            // than return that the platform reached the goal.
            return Vector2.Distance(transform.position, _currentTargetPoint) < threshold;
        }

        protected bool IsMovingRight()
        {
            return transform.position.x < _currentTargetPoint.x;
        }


        protected bool IsMovingLeft()
        {
            return transform.position.x > _currentTargetPoint.x;
        }


        private void Awake()
        {
            if (IsPointsExist())
            {
                _currentTargetPoint = _patrolTrajectoryPoints[0].position;
            }

            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
