using System;
using UnityEngine;

namespace LivingBeings.Player.CharacterMovement
{
    public class SurfaceCheck : MonoBehaviour
    {
        [Serializable]
        public struct GroundCheckPoints
        {
            public Transform _leftGroundCheckPoint;
            public Transform _rightGroundCheckPoint;
        }
        
        [SerializeField] private GroundCheckPoints _groundCheckPoints;          // A points that define from where will be thrown rays that check if character is grounded.
        [SerializeField] private float _groundCheckDepth = 0;                   // How deep we must check to define is character standing on ground now.
        [SerializeField] private LayerMask _whatIsGround = Physics2D.AllLayers; // A mask determine what is the ground for the character.
        private GameObject _onWhatIsStanding = null;                            // On what surface is player standing right now.

        #region Properties

        public GameObject OnWhatIsStanding => _onWhatIsStanding;

        #endregion Properties

        public bool IsCharacterIsOnSurface()
        {
            //Throw two rays from groundCheck points(left and right).
            RaycastHit2D leftRay = ThrowRayFromPoint(_groundCheckPoints._leftGroundCheckPoint.position);
            RaycastHit2D rightRay = ThrowRayFromPoint(_groundCheckPoints._rightGroundCheckPoint.position);
            
            //If one or both rays is hitting ground, we say that character is grounded.
            bool isLeftRayHitGround = IsRayCollidedWithGround(leftRay);
            bool isRightRayHitGround = IsRayCollidedWithGround(rightRay);

            if (isRightRayHitGround)
            {
                _onWhatIsStanding = rightRay.collider.gameObject;
            }
            else if (isLeftRayHitGround)
            {
                _onWhatIsStanding = leftRay.collider.gameObject;
            }
            else
            {
                _onWhatIsStanding = null;
            }

            return isLeftRayHitGround || isRightRayHitGround;
        }


        public bool IsCharacterIsOnInclinedSurface()
        {
            //Throw two rays from groundCheck points(left and right).
            RaycastHit2D leftRay = ThrowRayFromPoint(_groundCheckPoints._leftGroundCheckPoint.position);
            RaycastHit2D rightRay = ThrowRayFromPoint(_groundCheckPoints._rightGroundCheckPoint.position);

            //If only one ray is hitting ground we say that character is on stairs.
            bool isLeftRayHitGround = IsRayCollidedWithGround(leftRay);
            bool isRightRayHitGround = IsRayCollidedWithGround(rightRay);

            if (isRightRayHitGround)
            {
                _onWhatIsStanding = rightRay.collider.gameObject;
            }
            else if (isLeftRayHitGround)
            {
                _onWhatIsStanding = leftRay.collider.gameObject;
            }
            else
            {
                _onWhatIsStanding = null;
            }
            
            return isLeftRayHitGround ^ isRightRayHitGround;
        }


        private RaycastHit2D ThrowRayFromPoint(Vector2 throwFormPoint)
        {
            return Physics2D.Raycast(throwFormPoint, -transform.up, _groundCheckDepth, _whatIsGround);
        }


        private bool IsRayCollidedWithGround(RaycastHit2D thorwnRay)
        {
            if (thorwnRay.collider != null)
            {
                return ((1 << thorwnRay.collider.gameObject.layer) & _whatIsGround) != 0;   
            }
            else
            {
                return false;
            }
        }

        #region OldSrcipt
        /*public bool IsCharacterIsOnSurface()
        {
            bool isGrounded = false;
            Collider2D[] colliders = null;

            colliders = GetEncouteredColliders();

            // If colliders list contain some colliders,
            // set that player is grounded to true.
            if (colliders != null)
            {
                isGrounded = true;
                _onWhatIsStanding = colliders[0].gameObject;
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
        }*/

        #endregion OldScript
    }
}
