using UnityEngine;

namespace Creatures.Enemies.ChaseTypes
{
    public class WalkingChase : Chase
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null; // Component that detect end of platform.
        
        public override void ChasePlayer(Transform playerTransform, Rigidbody2D enemyRigidbody)
        {
            if (!IsPlatformEndReached())
            {
                Vector2 enemyPosition = transform.position;
                Vector2 targetPosition = new Vector2(playerTransform.position.x, enemyPosition.y);
                Vector2 chaseDirection = (targetPosition - enemyPosition).normalized;
                //Vector2 desiredPosition = Vector2.MoveTowards(enemyPosition, target, step);
                
                enemyRigidbody.velocity = chaseDirection * (ChasingSpeed * Time.deltaTime);

                // Move enemy to the desired position.
                //transform.position = desiredPosition;
            }
            
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
        
        
        
        private bool IsPlatformEndReached()
        {
            return _platformEndDetector.IsPlatformEndReached;
        }
    }
}
