using UnityEngine;

namespace Creatures.Enemies.PatrolTypes
{
    public abstract class Patrol : MonoBehaviour
    {
        private bool _isFacingRight = true;                           // Check if player is facing right.
        private bool _isWaitingOnPoint = false;                                  // Check if enemy is now waiting.
        
        #region Properties
        
        protected bool IsFacingRight
        {
            get => _isFacingRight;
            set => _isFacingRight = value;
        }
        
        protected bool IsWaitingOnPoint
        {
            get => _isWaitingOnPoint;
            set => _isWaitingOnPoint = value;
        }

        #endregion Properties
        
        public abstract void PatrolArea();
        
        
        public void SetFacingDirection()
        {
            _isFacingRight = transform.forward == Vector3.forward ? true : false;
        }


        public void StayOnPoint()
        {
            _isWaitingOnPoint = true;
        }
        
        
        protected void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
