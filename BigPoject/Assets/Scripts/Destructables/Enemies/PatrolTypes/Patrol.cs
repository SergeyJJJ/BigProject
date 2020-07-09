using UnityEngine;

namespace Destructables.Enemies.PatrolTypes
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
        
        public abstract void PatrolArea(Transform enemyTransform, Rigidbody2D enemyRigidbody);

        public abstract void StaySomeTime();

        public void StopMove(Rigidbody2D enemyRigidbody)
        {
            enemyRigidbody.velocity = Vector2.zero;
        }
        
        
        public void SetFacingDirection()
        {
            _isFacingRight = transform.forward == Vector3.forward ? true : false;
        }


        protected void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
