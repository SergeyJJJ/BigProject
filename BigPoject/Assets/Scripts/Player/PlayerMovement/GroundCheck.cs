using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Is on ground controll")]
    [SerializeField] private Transform _groundCheckPoint = null;           // A position marking where to check if the character is grounded.
    [SerializeField] private float _grounCheckRadius = 0f;                 // Radius of the overlap circle to determine if character is grounded.
    [SerializeField] private LayerMask _whatIsGound = Physics2D.AllLayers; // A mask determine what is the ground for the character.
    private bool _isGrounded = true;                                       // Determine if character is grounded.
    private bool _isOnPlatform = false;                                    // Determine if character is on the platform.

    public bool IsGrounded 
    {
        get
        {
            return _isGrounded;
        }
    }

    public bool IsOnPlatform
    {
        get
        {
            return _isOnPlatform;
        }
    }


    private void Update()
    {
        Collider2D[] colliders;

        colliders = GetEncouteredColliders();

        if(colliders != null)
        {
            _isGrounded = IsPlayerGrounded(colliders);
            _isOnPlatform = IsPlayerOnPlatform(colliders);
        }

    }


    // Get all colliders that the object encountered.
    private Collider2D[] GetEncouteredColliders()
    {
        Collider2D[] colliders = null;

        // If point around which we check colliders is exist.
        if (_groundCheckPoint != null)
        {
            // Get all colliders with wich we overlap around point with certain radius.
            colliders = Physics2D.OverlapCircleAll(_groundCheckPoint.position, _grounCheckRadius, _whatIsGound);
        }

        return colliders;
    }


    // Check if player is stand on some walkable surface.
    private bool IsPlayerGrounded(Collider2D[] colliders)
    {
        bool isGrounded = false;

        for (var colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++)
        {
            // If some collider is not belong to the player
            // Set that the player is Grounded.
            if ( colliders[colliderIndex].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        return isGrounded;
    }


    // Check if the player is stand on the moving platform.
    private bool IsPlayerOnPlatform(Collider2D[] colliders)
    {
        bool isOnPlatform = false;

        for (var colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++)
        {
            // If some collider is not belong to the player
            // Set that the player is Grounded.
            if ( colliders[colliderIndex].gameObject.CompareTag("MovingPlatform"))
            {
                isOnPlatform = true;
            }
        }

        return isOnPlatform;
    }
}
