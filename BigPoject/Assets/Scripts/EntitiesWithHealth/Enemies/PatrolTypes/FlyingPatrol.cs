using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using EntitiesWithHealth.Enemies.PatrolTypes;
using UnityEngine;

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
        throw new System.NotImplementedException();
    }
}
