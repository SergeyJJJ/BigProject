using UnityEngine;

namespace Enemies.ChaseTypes
{
    public abstract class Chase : MonoBehaviour
    {
        [SerializeField] private float _chasingSpeed = 0f;                        // Chasing speed.

        #region Properties

        protected float ChasingSpeed => _chasingSpeed;

        #endregion
        
        public abstract void ChasePlayer(Transform player);
    }
}
