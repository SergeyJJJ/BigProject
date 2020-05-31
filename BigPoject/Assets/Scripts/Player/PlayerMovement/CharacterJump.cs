using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 4;                      // Character jump force.
    [SerializeField] private float _availableJumpTimeAfterFalling = 0f;
    [SerializeField] private Transform _groundCheckPoint = null;           // A position marking where to check if the player is grounded.
    [SerializeField] private float _grounCheckRadius = 0.2f;               // Radius of the overlap circle to determine if grounded.
    [SerializeField] private LayerMask _whatIsGound = Physics2D.AllLayers; // A mask determine what is ground for the player.
    [SerializeField] private CustomMovementButton _jumpButton = null;
    private bool _isGrounded = false;                                      // Determine if player is grounded or not.
    private Rigidbody2D _characterRigidBody = null;                        // Hold character Rigidbody2d component.
    private const short StopMovementSpeed = 0;                             // Value indicating that the object is not moving in some direction.
    private float _lastGroundedTime = 0;

    private void Start()
    {
        InitializeRigidBodyComponents();
    }


    private void FixedUpdate()
    {
        if (IsJumpButtonEnabled() && IsJumpButtonPressed())
        {
            Jump();
        }
    }


    // Method that initialize RigidBodyComponents.
    private void InitializeRigidBodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Method that allows the character to jump.
    private void Jump()
    {
        ButtonPressedJumpActions();
    }


    private void ButtonPressedJumpActions()
    {
        float zeroForce = 0f;

        // Direction in which raise the character.
        Vector2 jumpDirection = new Vector2(zeroForce, _jumpVelocity);

        // If player is grounded chamnge his vertical velocity
        // to raise him up.
        if (IsGrounded() || IsStillCanJump())
        {
            _characterRigidBody.velocity = new Vector2(StopMovementSpeed, _jumpVelocity);
            _isGrounded = false;
        }
    }


    // Check if the player is grounded or not.
    private bool IsGrounded()
    {
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

                    // Set time when player was grounded last time.
                    _lastGroundedTime = Time.time;
                }
            }
        }
        
        // Return status of player. Grounded or not.
        return _isGrounded;
    }

    private bool IsStillCanJump()
    {
        // Calculate how much time pass after player was
        // grounded last time.
        float timePass = Time.time - _lastGroundedTime;

        // Return true if player still have time to jump
        // false if not.
        return timePass < _availableJumpTimeAfterFalling;
    }


    //Check if jump button is pressed.
    private bool IsJumpButtonPressed()
    {
        return _jumpButton.IsPressed;
    }


    // Check if jump button is enabled.
    private bool IsJumpButtonEnabled()
    {
        return _jumpButton.isActiveAndEnabled;
    }
}