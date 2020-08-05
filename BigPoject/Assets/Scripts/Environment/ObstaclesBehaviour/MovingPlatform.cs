using System.Collections.Generic;
using UnityEngine;

namespace Environment.ObstaclesBehaviour
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private List<Transform> _trajectoryPoints = null;         // Contains path points along which the platform moves.
        [SerializeField] private float _movementSpeed = 0f;                        // Contains Platform movement speed.
        private Transform _currentTargetPoint = null;                        // Current target point.
        private int _currentPointIndex = 0;                                        // Current target point index.
        private Vector3 _moveDelta = Vector3.zero;                                 // Change in position of platform.
        private Rigidbody2D _characterRigidbody = null;                            // Player`s Rigidbody2D component.                               

        private void Start()
        {
            // If list is containing at least one point
            // set that first point to current target point.
            if (IsPointsExist())
            {
                _currentTargetPoint = _trajectoryPoints[0].transform;
            }
        }


        private void Update()
        {
            // If list is containing at least one point.
            if (IsPointsExist())
            {
                // Move platform to the target point.
                Move();

                // If platform reached the target point.
                if (IsReachedPoint())
                {
                    // Change current target point.
                    ChangeTargetPoint();
                }
            }
        }


        private void LateUpdate()
        {
            // If character Rigidbody2D component is exist
            if (IsRigidbodyExist())
            {
                ShiftCharacter();
            }
        }
        

        // Move the platform towards the target point.
        private void Move()
        {
            float step = _movementSpeed * Time.deltaTime;
            Vector2 desiredPosition = Vector2.MoveTowards(transform.position, _currentTargetPoint.position, step);

            // Store change in position of platform.
            _moveDelta = new Vector3(desiredPosition.x, desiredPosition.y, 0f) - transform.position;

            // Move platform to the desired position.
            transform.position = desiredPosition;
        }


        // Change the position the platform is moving to.
        private void ChangeTargetPoint()
        {
            _currentTargetPoint = GetNextPoint();
        }
        
        
        private Transform GetNextPoint()
        {
            // Here will be stored point to be returned.
            Transform nextPoint = null;

            // If current point index is equal to points amount,
            // reset point index to zero and set first point to return.
            if (_currentPointIndex == _trajectoryPoints.Count - 1)
            {
                _currentPointIndex = 0;
                nextPoint = _trajectoryPoints[_currentPointIndex];
            }
            // If it is not, than increment point index and set next point to return.
            else
            {
                nextPoint = _trajectoryPoints[_currentPointIndex + 1];
                _currentPointIndex++;
            }

            // return index.
            return nextPoint;
        }


        // Check if platform is reached point.
        private bool IsReachedPoint()
        {
            float threshold = 0.1f;

            // If distance between platform and target point is less than allowable threshold
            // than return that the platform reached the goal.
            return Vector2.Distance(gameObject.transform.position, _currentTargetPoint.position) < threshold;
        }


        // Change character position to the position with
        // the offset(change in position of platform).
        private void ShiftCharacter()
        {
            Vector2 characterBody = _characterRigidbody.position;
            _characterRigidbody.transform.position = new Vector3(characterBody.x, characterBody.y) + _moveDelta;
        }


        private bool IsPointsExist()
        {
            return _trajectoryPoints != null;
        }


        private bool IsRigidbodyExist()
        {
            return _characterRigidbody != null;
        }


        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _characterRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            }
        }
 
        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _characterRigidbody = null;
            }
        }
    }
}
