using Enemies.AIStateMachine;
using UnityEngine;

namespace Enemies.PatrolTypes
{
    public abstract class Patrol : MonoBehaviour
    {
        protected static bool _isFacingRight = true;                           // Check if player is facing right.
        
        
        public abstract void PatrolArea();
        
        
        protected void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
