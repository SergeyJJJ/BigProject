using UnityEngine;

namespace Enemies.PatrolTypes
{
    public class WalkingPatrol : MovingPatrol
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null;   // Components that allow to detect if enemy is near platform edge.
        
        public override void PatrolArea()
        {
            // Move enemy to the target point.
            Move();

            /*
            if (IsMovingLeft() && _isFacingRight)
            {
                Flip();
            }
            else if (IsMovingRight() && !_isFacingRight)
            {
                Flip();   
            }*/

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
            Vector2 currentPosition = transform.position;
            Vector2 moveToPositon = new Vector2(_currentTargetPoint.x, currentPosition.y);
            Vector2 desiredPosition = Vector2.MoveTowards(currentPosition, moveToPositon, step);

            // Move platform to the desired position.
            transform.position = desiredPosition;
        }
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
    }
}
