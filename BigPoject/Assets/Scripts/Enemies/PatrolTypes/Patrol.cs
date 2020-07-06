using Enemies.AIStateMachine;
using UnityEngine;

namespace Enemies.PatrolTypes
{
    public abstract class Patrol : MonoBehaviour
    {
        private static bool _isFacingRight = true;                           // Check if player is facing right.
        
        
        public abstract void PatrolArea(Transform enemy);
        
        
        protected void Flip(Transform enemy)
        {
            _isFacingRight = !_isFacingRight;
            enemy.Rotate(0f, 180f, 0f);
        }
    }
}
