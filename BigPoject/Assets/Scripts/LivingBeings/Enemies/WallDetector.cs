using UnityEngine;

namespace LivingBeings.Enemies
{
    public class WallDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsGround = Physics2D.DefaultRaycastLayers;  // Defines what is ground for enemy.
        [SerializeField] private float _platformCheckDistance = 0f;                         // Distance on which will be checked end of platform.
        private bool _isWallInFrontOfTheEnemy = false;                                      //detect if wall is in front of player. 

        #region Properites

        public bool IsWallInFrontOfTheEnemy => _isWallInFrontOfTheEnemy;

        #endregion Properties

        private void FixedUpdate()
        {
            RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right, _platformCheckDistance, _whatIsGround);

            if (wallInfo.collider != null)
            {
                _isWallInFrontOfTheEnemy = true;
            }
            else
            {
                _isWallInFrontOfTheEnemy = false;
            }
        }
    }
}
