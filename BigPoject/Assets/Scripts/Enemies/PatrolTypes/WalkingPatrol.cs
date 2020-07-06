using UnityEngine;

namespace Enemies.PatrolTypes
{
    public class WalkingPatrol : MovingPatrol
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null;   // Components that allow to detect if enemy is near platform edge.
        
        public override void PatrolArea(Transform enemy)
        {
            // Move enemy to the target point.
            Move();

            // If platform reached the target point.
            if (IsReachedPoint() || IsPlatformEndReached())
            {
                // Change current target point.
                ChangeTargetPoint();
            }
        }
        
        
        protected override void Move()
        {
            float step = _patrolSpeed * Time.deltaTime;
            Vector2 desiredPosition = Vector2.MoveTowards(transform.position, _currentTargetPoint, step);

            // Move platform to the desired position.
            transform.position = desiredPosition;
        }
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
    }
}
