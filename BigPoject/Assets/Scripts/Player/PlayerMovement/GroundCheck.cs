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
        CheckIfGrounded();
    }


    // Check if the player is grounded or not.
    private bool CheckIfGrounded()
    {
        // Set that the player is now not on the ground.
        _isGrounded = false;

        //Check if groundCheckPoint is not a null.
        if (_groundCheckPoint != null)
        {
            // Get all colliders with which player collides.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckPoint.position, _grounCheckRadius, _whatIsGound);

            // Check all colliders to know if at least one of them 
            // is not owned by player.
            for (var colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++)
            {
                // If some collider is not belong to the player
                // Set that the player is Grounded.
                if ( colliders[colliderIndex].gameObject != gameObject)
                {
                    _isGrounded = true;
                }
            }
        }
        
        // Return status of player. Grounded or not.
        return _isGrounded;
    }
}
