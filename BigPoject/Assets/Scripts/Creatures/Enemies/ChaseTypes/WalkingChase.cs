using System.CodeDom;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Enemies.ChaseTypes
{
    public class WalkingChase : Chase
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null; // Component that detect end of platform.
        
        public override void ChasePlayer(Transform player)
        {
            if (!IsPlatformEndReached())
            {
                float step = ChasingSpeed * Time.deltaTime;
                Vector2 enemyPosition = transform.position;
                Vector2 target = new Vector2(player.position.x, enemyPosition.y);
                Vector2 desiredPosition = Vector2.MoveTowards(enemyPosition, target, step);

                // Move enemy to the desired position.
                transform.position = desiredPosition;
            }
            
            // Change facing direction if needed.
            if (IsPlayerToTheRight(player) && !IsFacingRight)
            {
                Flip();
            }
            else if (IsPlayerToTheLeft(player) && IsFacingRight)
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
