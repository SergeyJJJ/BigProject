using Enemies.AIStateMachine;
using UnityEngine;

namespace Enemies.PatrolTypes
{
    public abstract class Patrol : MonoBehaviour
    {
        private static bool _isFacingRight = true;                           // Check if player is facing right.

        #region Properties
        
        protected static bool IsFacingRight
        {
            get => _isFacingRight;
            set => _isFacingRight = value;
        }

        #endregion Properties
        
        public abstract void PatrolArea();
        
        
        protected void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
