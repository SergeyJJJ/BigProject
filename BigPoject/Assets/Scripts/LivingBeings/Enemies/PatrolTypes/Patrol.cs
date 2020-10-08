using UnityEngine;

namespace LivingBeings.Enemies.PatrolTypes
{
    // Base class for all patrol types.
    public abstract class Patrol : MonoBehaviour
    {
        private Animator _animator = null;                            // Animator that used to control enemy`s animations.
        
        private bool _isFacingRight = true;                           // Check if player is facing right.
        private bool _isWaitingOnPoint = false;                       // Check if enemy is now waiting.
        private bool _isAlreadyMoving = false;                                   // Determine if enemy is moving right now.
        private bool _isAlreadyStanding = false;                                 // Determine if enemy is standing right now.


        #region Properties

        protected Animator EnemyAnimator => _animator;

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
        
        protected bool IsAlreadyMoving
        {
            get => _isAlreadyMoving;
            set => _isAlreadyMoving = value;
        }

        protected bool IsAlreadyStanding
        {
            get => _isAlreadyStanding;
            set => _isAlreadyStanding = value;
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
        
        
        public void SetMovingAndStandingBoolsToDefault()
        {
            _isAlreadyMoving = false;
            _isAlreadyStanding = false;
        }
        

        protected void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }


        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}