using UnityEngine;

namespace Creatures.Enemies.PatrolTypes
{
    public class WalkingPatrol : MovingPatrol
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null;   // Components that allow to detect if enemy is near platform edge.
        
        public override void PatrolArea(Transform enemyTransform, Rigidbody2D enemyRigidbody)
        {
            if (IsWaitingOnPoint)
            {
                StayOnPointTimer -= Time.deltaTime;

                // When time for staying is out
                if (StayOnPointTimer < 0)
                {    
                    // Set that is time to go further.
                    IsWaitingOnPoint = false;
                    
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
            else
            {
                // If platform reached the target point.
                if (IsPointReached(enemyTransform) || IsPlatformEndReached())
                {
                    // Change current target point.
                    ChangeTargetPoint();
                    
                    // Set that is time for waiting.
                    IsWaitingOnPoint = true;
                    
                    StopMove(enemyRigidbody);
                    
                    // Set how long enemy will stay.
                    StayOnPointTimer = TimeToStayOnPoint;
                }
                else
                {
                    // Move enemy to the target point.
                    Move(enemyTransform, enemyRigidbody);
                }
            }
        }
        
        
        protected override void Move(Transform playerTransform, Rigidbody2D enemyRigidbody)
        {
            Vector2 currentPosition = playerTransform.position;
            Vector2 targetPosition = new Vector2(CurrentTargetPoint.x, currentPosition.y);
            Vector2 targetDirection = (targetPosition - currentPosition).normalized;

            enemyRigidbody.velocity = targetDirection * (PatrolSpeed * Time.deltaTime);
        }
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
        
        
        // Check if target point is reached by comparing x coordinate
        // of the enemy and the target point.
        protected override bool IsPointReached(Transform playerTransform)
        {
            float threshold = 0.1f;
            
            // Lead Y-positions of both points to common value.
            Vector2 playerPositionForComparison = new Vector2(playerTransform.position.x, 0f);
            Vector2 targetPositionForComparison = new Vector2(CurrentTargetPoint.x, 0f);
     
            // If distance between enemy and target point is less than allowable threshold
            // than return that the enemy has reached the goal.
            return Vector2.Distance(playerPositionForComparison, targetPositionForComparison) < threshold;
        }
    }
}
