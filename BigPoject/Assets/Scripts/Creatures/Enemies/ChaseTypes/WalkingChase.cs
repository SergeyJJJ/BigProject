﻿using System.Data.Common;
using UnityEngine;

namespace Creatures.Enemies.ChaseTypes
{
    public class WalkingChase : Chase
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null; // Component that detect end of platform.

        public override void ChasePlayer(Transform playerTransform, Rigidbody2D enemyRigidbody)
        {
            if (IsPlatformEndReached())
            {
                enemyRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if (IsEnoughDistance(playerTransform))
                {
                    Vector2 enemyPosition = transform.position;
                    Vector2 targetPosition = new Vector2(playerTransform.position.x, enemyPosition.y);
                    Vector2 chaseDirection = (targetPosition - enemyPosition).normalized;

                    enemyRigidbody.velocity = chaseDirection * (ChasingSpeed * Time.deltaTime);
                }
                else
                {
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
            float threshold = 1.9f;

            return Vector2.Distance(playerTransform.position, transform.position) > threshold;
        }
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
    }
}
