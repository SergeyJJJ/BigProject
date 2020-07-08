using UnityEngine;

namespace Creatures.Enemies.PatrolTypes
{
    public class WalkingPatrol : MovingPatrol
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null;   // Components that allow to detect if enemy is near platform edge.
        
        public override void PatrolArea()
        {
            if (IsWaitingOnPoint)
            {
                StayOnPointTimer -= Time.deltaTime;

                // When time for staying is out
                if (StayOnPointTimer < 0)
                {    
                    // Set that is time to go further.
                    IsWaitingOnPoint = false;
                }
            }
            else
            {
                // If platform reached the target point.
                if (IsPointReached() || IsPlatformEndReached())
                {
                    // Change current target point.
                    ChangeTargetPoint();
                    
                    // Set that is time for waiting.
                    IsWaitingOnPoint = true;
                    
                    // Set how long enemy will stay.
                    StayOnPointTimer = TimeToStayOnPoint;
                }
                else
                {
                    // Move enemy to the target point.
                    Move();
                    
                    // Change facing direction if needed.
                    if (IsTargetPointToTheRight() && !IsFacingRight)
                    {
                        Flip();
                    }
                    else if (IsTargetPointToTheLeft() && IsFacingRight)
                    {
                        Flip();
                    }
                }
            }
        }
        
        
        protected override void Move()
        {
            float step = PatrolSpeed * Time.deltaTime;
            Vector2 currentPosition = transform.position;
            Vector2 moveToPositon = new Vector2(CurrentTargetPoint.x, currentPosition.y);
            Vector2 desiredPosition = Vector2.MoveTowards(currentPosition, moveToPositon, step);

            // Move platform to the desired position.
            transform.position = desiredPosition;
        }
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
        
        
        // Check if target point is reached by comparing x coordinate
        // of the enemy and the target point.
        protected override bool IsPointReached()
        {
            return Mathf.Approximately(transform.position.x, CurrentTargetPoint.x);
        }
    }
}
