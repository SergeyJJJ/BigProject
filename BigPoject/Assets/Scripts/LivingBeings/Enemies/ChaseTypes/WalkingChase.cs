using System;
using UnityEngine;

namespace LivingBeings.Enemies.ChaseTypes
{
    // Class that provides functionality to perform walking chase.
    public class WalkingChase : Chase
    {
        [SerializeField] private float _stoppingDistanceToPlayer = 0;             // Distance to the Player at which the enemy stop chasing.
        [SerializeField] private PlatformEndDetector _platformEndDetector = null; // Component that detect end of platform.
        [SerializeField] private WallDetector _wallDetector = null;               // Component that detect if wall is in front of player.
        
        public override void ChasePlayer(Transform enemyTransform, Transform playerTransform, Rigidbody2D enemyRigidbody)
        {
            if (IsPlatformEndReached() || IsWallInFrontOfEnemy())
            {
                if (!IsAlreadyStanding)
                {
                    EnemyAnimator.SetBool("Chase", false);
                    EnemyAnimator.SetBool("Patrol", false);
                    EnemyAnimator.SetBool("Idle", true);
                    
                    IsAlreadyStanding = true;
                    IsAlreadyMoving = false;
                }
                
                enemyRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if (IsEnoughDistance(playerTransform))
                {
                    if (!EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        if (!IsAlreadyMoving)
                        {
                            EnemyAnimator.SetBool("Chase", true);
                            EnemyAnimator.SetBool("Idle", false);
                            EnemyAnimator.SetBool("Patrol", false);

                            IsAlreadyMoving = true;
                            IsAlreadyStanding = false;
                        }

                        Vector2 enemyPosition = enemyTransform.position;
                        Vector2 targetPosition = new Vector2(playerTransform.position.x, enemyPosition.y);
                        Vector2 chaseDirection = (targetPosition - enemyPosition).normalized;

                        //enemyRigidbody.velocity = chaseDirection * (ChasingSpeed * Time.deltaTime);
                        enemyRigidbody.velocity = new Vector2(chaseDirection.x * (ChasingSpeed * Time.deltaTime),
                                                              enemyRigidbody.velocity.y);
                    }
                }
                else
                {
                    if (!IsAlreadyStanding)
                    {
                        EnemyAnimator.SetBool("Chase", false);
                        EnemyAnimator.SetBool("Patrol", false);
                        EnemyAnimator.SetBool("Idle", true);
                        
                        IsAlreadyStanding = true;
                        IsAlreadyMoving = false;
                    }
                    
                    enemyRigidbody.velocity = Vector2.zero;
                }
            }

            if (IsEnoughDistance(playerTransform))
            {
                // Change facing direction if needed.
                if (IsPlayerToTheRight(playerTransform) && !IsFacingRight)
                {
                    Flip();
                }
                else if (IsPlayerToTheLeft(playerTransform) && IsFacingRight)
                {
                    Flip();
                }
            }
        }


        // Check is there some distance between enemy and player.
        // It`s important to prevent enemy from endless flipping.
        private bool IsEnoughDistance(Transform playerTransform)
        {
            return Vector2.Distance(playerTransform.position, transform.position) > _stoppingDistanceToPlayer;
        }
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
        
        
        private bool IsWallInFrontOfEnemy()
        {
            return _wallDetector.IsWallInFrontOfTheEnemy;
        }
    }
}
