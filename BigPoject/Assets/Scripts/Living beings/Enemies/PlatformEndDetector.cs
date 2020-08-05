using UnityEngine;

namespace Living_beings.Enemies
{
    public class PlatformEndDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsGround = Physics2D.DefaultRaycastLayers;   // Defines what is ground for enemy.
        [SerializeField] private float _platformCheckDistance = 0f;                         // Distance on which will be checked end of platform.
        private bool _isPlatformEndReached = false;                                          // Defines if reached end of the platform on which player is standing.
        

        public bool IsPlatformEndReached => _isPlatformEndReached;


        private void Update()
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, _platformCheckDistance, _whatIsGround);

            if (groundInfo.collider == null)
            {
                _isPlatformEndReached = true;
            }
            else
            {
                _isPlatformEndReached = false;
            }
        }
    }
}