using System.CodeDom;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Enemies.ChaseTypes
{
    public class WalkingChase : Chase
    {
        [SerializeField] private PlatformEndDetector _platformEndDetector = null; // Component that detect end of platform.
        
        public override void ChasePlayer(Transform player, Transform enemy, float chasingSpeed)
        {
            if (!_platformEndDetector.IsPlatformEndReached)
            {
                float step = chasingSpeed * Time.deltaTime;
                Vector2 enemyPosition = enemy.position;
                Vector2 target = new Vector2(player.position.x, enemyPosition.y);
                Vector2 desiredPosition = Vector2.MoveTowards(enemyPosition, target, step);

                // Move enemy to the desired position.
                enemy.position = desiredPosition;
            }
        }
    }
}
