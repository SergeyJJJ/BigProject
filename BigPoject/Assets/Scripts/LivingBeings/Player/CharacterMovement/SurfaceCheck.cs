using UnityEngine;

namespace LivingBeings.Player.CharacterMovement
{
    public class SurfaceCheck : MonoBehaviour
    {
        [Header("Is on ground controll")]
        [SerializeField] private Transform _groundCheckPoint = null;            // A position marking where to check if the character is grounded.
        [SerializeField] private Vector2 _groundCheckBoxSize = Vector2.zero;    // Shape of the overlap box, that used to determine if character is grounded.
        [SerializeField] private LayerMask _whatIsGround = Physics2D.AllLayers; // A mask determine what is the ground for the character.
        private const float _groundCheckBoxRotation = 0f;                       // Rotation the overlap box, that used to determine if character is grounded.

        public bool IsCharacterIsOnSurface()
        {
            bool isGrounded = false;
            Collider2D[] colliders = null;

            colliders = GetEncouteredColliders();

            // If colliders list contain some colliders,
            // set that player is grounded to true.
            if (colliders != null)
            {
                isGrounded = true;
            }

            return isGrounded;
        }


        // Return last collider from the colliders list.
        // This last collider is surface on which player 
        // standing right now.
        public Collider2D GetSurfaceOnWhichPlayerStanding()
        {
            Collider2D surfaceCollider = null;
            Collider2D[] colliders = null;

            colliders = GetEncouteredColliders();

            // If colliders list contain some colliders,
            // set surface collider as a last collider from the list.
            if (colliders != null)
            {
                surfaceCollider = colliders[colliders.Length-1];
            }

            return surfaceCollider;
        }


        // Return colliders whith which player collides
        // except player collider by itself.
        private Collider2D[] GetEncouteredColliders()
        {
            Collider2D[] allColliders = null;
            
            // Get all colliders with wich we overlap around point with certain radius.
            allColliders = Physics2D.OverlapBoxAll(_groundCheckPoint.position, _groundCheckBoxSize, _groundCheckBoxRotation, _whatIsGround);

            // If player is not stay on something, return null.
            if (allColliders.Length == 0)
            {
                return null;
            }
        

            Collider2D[] filteredColliders = new Collider2D[allColliders.Length];
            int filteredCollidersIndex = 0;

            // Remove character colliders from the colliders list.
            for (var colliderIndex = 0; colliderIndex < allColliders.Length; colliderIndex++)
            {
                // If some collider is not belong to the player
                // add it to filtered colliders list.
                if ( allColliders[colliderIndex].gameObject != gameObject)
                {
                    filteredColliders[filteredCollidersIndex] = allColliders[colliderIndex];
                }
            }

            return filteredColliders;
        }
    }
}
