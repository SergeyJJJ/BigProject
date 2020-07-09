using UnityEngine;

namespace Creatures.Enemies.ChaseTypes
{
    public abstract class Chase : MonoBehaviour
    {
        [SerializeField] private float _chasingSpeed = 0f;                  // Chasing speed.
        private bool _isFacingRight = false;                                // Is Enemy is facing right.

        #region Properties

        protected float ChasingSpeed => _chasingSpeed;

        protected bool IsFacingRight => _isFacingRight;
        
        #endregion
        
        public abstract void ChasePlayer(Transform playerTransform, Rigidbody2D enemyRigidbody);
        
        
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
        
        
        protected bool IsPlayerToTheRight(Transform player)
        {
            return transform.position.x < player.position.x;
        }


        protected bool IsPlayerToTheLeft(Transform player)
        {
            return transform.position.x > player.position.x;
        }
    }
}
