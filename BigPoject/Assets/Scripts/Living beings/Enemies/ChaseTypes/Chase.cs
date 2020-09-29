using UnityEngine;

namespace Living_beings.Enemies.ChaseTypes
{
    // Base class for enemy`s chasing behaviour.
    public abstract class Chase : MonoBehaviour
    {
        [SerializeField] private float _chasingSpeed = 0f;            // Chasing speed.
        private Animator _animator = null;                            // Animator that used to control enemy`s animations.
        private bool _isFacingRight = false;                          // Is Enemy is facing right.
        private bool _isAlreadyMoving = false;                        // Determine if enemy is moving right now.
        private bool _isAlreadyStanding = false;                      // Determine if enemy is standing right now.


        #region Properties

        protected float ChasingSpeed => _chasingSpeed;

        protected Animator EnemyAnimator => _animator;
        
        protected bool IsFacingRight => _isFacingRight;
        
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
        
        #endregion
        
        public abstract void ChasePlayer(Transform enemyTransform, Transform playerTransform, Rigidbody2D enemyRigidbody);
        
        
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
        
        
        protected bool IsPlayerToTheRight(Transform player)
        {
            return transform.position.x < player.position.x;
        }


        protected bool IsPlayerToTheLeft(Transform player)
        {
            return transform.position.x > player.position.x;
        }
        
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
