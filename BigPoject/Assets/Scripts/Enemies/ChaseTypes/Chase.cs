using System;
using UnityEngine;

namespace Enemies.ChaseTypes
{
    public abstract class Chase : MonoBehaviour
    {
        [SerializeField] private float _chasingSpeed = 0f;                  // Chasing speed.
        private BoxCollider2D _chaseZoneCollider = null;                       // Collider that used to define chase zone.
        
        #region Properties

        protected float ChasingSpeed => _chasingSpeed;

        #endregion
        
        public abstract void ChasePlayer(Transform player);

        private void Start()
        {
            _chaseZoneCollider = GetComponent<BoxCollider2D>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawCube(_chaseZoneCollider.offset, );
        }
    }
}
