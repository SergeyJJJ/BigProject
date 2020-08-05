using UnityEngine;

namespace Living_beings.Enemies.PatrolTypes
{
    // Class that provides functionality to perform flying patrol.
    public class FlyingPatrol : MovingPatrol
    {
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
                if (IsPointReached(enemyTransform))
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

    
        protected override void Move(Transform enemyTransform, Rigidbody2D enemyRigidbody)
        {
            Vector2 currentPosition = enemyTransform.position;
            Vector2 targetDirection = (CurrentTargetPoint - currentPosition).normalized;

            enemyRigidbody.velocity = targetDirection * (PatrolSpeed * Time.deltaTime);
        }


        protected override bool IsPointReached(Transform enemyTransform)
        {
            float threshold = 0.1f;

            // If distance between enemy and target point is less than allowable threshold
            // than return that the enemy has reached the goal.
            return Vector2.Distance(enemyTransform.position, CurrentTargetPoint) < threshold;
        }
    }
}
