using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [Header("Jump controll")]
    [SerializeField] private float _jumpVelocity = 4;                      // Character jump force.
    [SerializeField] private float _availableJumpTimeAfterFalling = 0f;    // The time during which the player can jump if he no longer touches the ground.
    [Range(0f, 1f)] [SerializeField] private float _cutJumpHeight = 1f;    // Cut jump height when player unpress the jump button. This variable allow us to controll jump height.

    [Space]
    [Header("Is on ground controll")]
    [SerializeField] private Transform _groundCheckPoint = null;           // A position marking where to check if the player is grounded.
    [SerializeField] private float _grounCheckRadius = 0.2f;               // Radius of the overlap circle to determine if grounded.
    [SerializeField] private LayerMask _whatIsGound = Physics2D.AllLayers; // A mask determine what is ground for the player.

    [Space]
    [Header("Jump button")]
    [SerializeField] private CustomMovementButton _jumpButton = null;

    private bool _isJumpbuttonWasReleased = true;                         // Check if button was released.
    private bool _isGrounded = false;                                      // Determine if player is grounded or not.
    private Rigidbody2D _characterRigidBody = null;                        // Hold character Rigidbody2d component.
    private float _lastGroundedTime = 0;                                   // Time when player touch the ground last time.


    private void Start()
    {
        InitializeRigidBodyComponents();
    }


    private void FixedUpdate()
    {
        // If button is is active and enabale
        if (_jumpButton.isActiveAndEnabled)
        {
            // If jump button is pressed
            if (_jumpButton.IsPressed)
            {
                // Perform jump Actions
                ButtonPressedJumpActions();
            }
            // Else if jump nuuton is relesaed and it wasn`t released
            else if (!_jumpButton.IsPressed && !_isJumpbuttonWasReleased)
            {
                // perform button release actions
                ButtonReleasedJumpActions();
            }
        }
    }


    // Method that initialize RigidBodyComponents.
    private void InitializeRigidBodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Actions and calculations to do when player press jump button.
    private void ButtonPressedJumpActions()
    {
        // If player is grounded and he still have time to jump
        // change his vertical velocity to raise him up.
        if (IsGrounded() || IsStillCanJump())
        {
            // Set player velocity.
            _characterRigidBody.velocity = new Vector2(_characterRigidBody.velocity.x, _jumpVelocity);

            // Set that the button wasn`t relesed.
            _isJumpbuttonWasReleased = false;
        }
    }


    // Actions and calculations to do when player releases jump button.
    private void ButtonReleasedJumpActions()
    {
        // If the player is falling down. 
        if (_characterRigidBody.velocity.y > 0)
        {
            // Reduce player Y-Axis velocity
            // by multiplying it to less then one coefficient.
            _characterRigidBody.velocity = new Vector2(_characterRigidBody.velocity.x, _characterRigidBody.velocity.y * _cutJumpHeight);

            // Set that the button was released.
            _isJumpbuttonWasReleased = true;
        }
    }


    // Check if the player is grounded or not.
    private bool IsGrounded()
    {
        // Set that the player is not on the ground.
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
}