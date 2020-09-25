﻿using System.Runtime.InteropServices;
using UnityEngine;

namespace Living_beings.Enemies.PatrolTypes
{
    // Class that provides functionality to perform walking patrol.
    public class WalkingPatrol : MovingPatrol
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null;   // Components that allow to detect if enemy is near platform edge.
        [SerializeField] private WallDetector _wallDetector = null;                 // Component that detect if wall is in front of player.
        
        public override void PatrolArea(Transform enemyTransform, Rigidbody2D enemyRigidbody)
        {
            if (IsWaitingOnPoint)
            {
                StayOnPointTimer -= Time.deltaTime;
                
                IsAlreadyMoving = false;
                
                if (!IsAlreadyStanding)
                {
                    EnemyAnimationsControl.StartIdleAnimation();
                    IsAlreadyStanding = true;
                }
                
                // When time for staying is out
                if (StayOnPointTimer < 0)
                {    
                    // Set that is time to go further.
                    IsWaitingOnPoint = false;
                    IsAlreadyStanding = false;
                    
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
                if (IsPointReached(enemyTransform) || IsPlatformEndReached() || IsWallInFrontOfEnemy())
                {
                    // Change current target point.
                    ChangeTargetPoint();
                    
                    // Set that is time for waiting.
                    IsWaitingOnPoint = true;
                    
                    StopMove(enemyRigidbody);
                    
                    // Set how long enemy will stay.
                    StayOnPointTimer = TimeToStayOnPoint;
                    
                    if (!IsAlreadyStanding)
                    {
                        EnemyAnimationsControl.StartIdleAnimation();
                        IsAlreadyStanding = true;
                    }
                }
                else
                {
                    if (!IsAlreadyMoving)
                    {
                        EnemyAnimationsControl.StartWalkingAnimation();
                        IsAlreadyMoving = true;
                    }
                    
                    // Move enemy to the target point.
                    Move(enemyTransform, enemyRigidbody);
                }
            }
        }
        
        
        protected override void Move(Transform enemyTransform, Rigidbody2D enemyRigidbody)
        {
            Vector2 currentPosition = enemyTransform.position;
            Vector2 targetPosition = new Vector2(CurrentTargetPoint.x, currentPosition.y);
            Vector2 targetDirection = (targetPosition - currentPosition).normalized;

            enemyRigidbody.velocity = targetDirection * (PatrolSpeed * Time.deltaTime);
        }


        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }


        private bool IsWallInFrontOfEnemy()
        {
            return _wallDetector.IsWallInFrontOfTheEnemy;
        }
        
        
        // Check if target point is reached by comparing x coordinate
        // of the enemy and the target point.
        protected override bool IsPointReached(Transform enemyTransform)
        {
            float threshold = 0.1f;
            
            // Lead Y-positions of both points to common value.
            Vector2 enemyPositionForComparison = new Vector2(enemyTransform.position.x, 0f);
            Vector2 targetPositionForComparison = new Vector2(CurrentTargetPoint.x, 0f);
     
            // If distance between enemy and target point is less than allowable threshold
            // than return that the enemy has reached the goal.
            return Vector2.Distance(enemyPositionForComparison, targetPositionForComparison) < threshold;
        }
    }
}