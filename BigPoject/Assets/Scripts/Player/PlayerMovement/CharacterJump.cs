using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterJump : MonoBehaviour
{
    [Header("Jump controll")]
    [SerializeField] private float _jumpVelocity = 4;                      // Character jump force.
    [SerializeField] private float _afterFallingJumpTime = 0f;             // The time during which the player can jump if he no longer touches the ground.
    [SerializeField] private float _pressBeforeGroundTime = 0f;            // The time during which player can press jump button before touching the ground, and jump will be performed.
    [Range(0f, 1f)] [SerializeField] private float _cutJumpHeight = 1f;    // Cut jump height when player unpress the jump button. This variable allow us to controll jump height.

    [Space]
    [Header("Is on ground controll")]
    [SerializeField] private Transform _groundCheckPoint = null;           // A position marking where to check if the character is grounded.
    [SerializeField] private float _grounCheckRadius = 0.2f;               // Radius of the overlap circle to determine if character is grounded.
    [SerializeField] private LayerMask _whatIsGound = Physics2D.AllLayers; // A mask determine what is ground for the character.

    [Space]
    [Header("Jump button")]
    [SerializeField] private CustomMovementButton _jumpButton = null;

    // Events.
    public delegate void OnJump();                                         
    public static event OnJump onJump;                                     // Event that holds things to do when player is jumping.
    public delegate void OnFalling();
    public static event OnFalling onFalling;                               // Event that holds thing to do when character is falling.
    public delegate void OnLand();                                         
    public static event OnLand onLand;                                     // Event that holds things to do when character is landing.


    private bool _isJumpButtonWasReleased = true;                          // Check if button was released.
    private Rigidbody2D _characterRigidBody = null;                        // Hold character Rigidbody2d component.
    private float _afterFallingTimer = 0;                                  // Timer that count time after falling from the ground.
    private float _pressBeforeGroundTimer = 0;                             // Timer that count time during which player can press jump button before touching the ground, and jump will be performed.
    private bool _isGrounded = true;                                       // Determine if player is grounded now.


    private void Awake()
    {
        InitializeRigidbodyComponents();
    }


    private void FixedUpdate()
    {
        // If button is active and enabale allow player to jump.
        if (IsButtonEnabled())
        {
            CalculateJumpProcess();
        }
        
        // If player is falling invoke falling actions.
        if (IsFalling())
        {
            if (onFalling != null)
            {
                onFalling();  
            }
        }
    }


    // Initialize rigidbody components.
    private void InitializeRigidbodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Jump processing method
    private void CalculateJumpProcess()
    {
        // Decrement timers.
        _afterFallingTimer -= Time.deltaTime;
        _pressBeforeGroundTimer -= Time.deltaTime;

        // If player touches the ground reset "after falling timer".
        if (IsGrounded())
        {
            _afterFallingTimer = _afterFallingJumpTime;
        }

        // If jump button is pressed.
        if (_jumpButton.IsPressed)
        {   
            // Set that the button is now relseased.
            _isJumpButtonWasReleased = false;

            // Reset "press before gound" timer.
            _pressBeforeGroundTimer  = _pressBeforeGroundTime;
        }

        // If jump button is relesaed and it wasn`t released.
        if (!_jumpButton.IsPressed && !_isJumpButtonWasReleased)
        {
            // Perform button release actions
            // If the player is falling down. 
            if (IsFalling())
            {
                // Cut player Y-Axis velocity
                // by multiplying it to less then one coefficient.
                _characterRigidBody.velocity = new Vector2(_characterRigidBody.velocity.x, _characterRigidBody.velocity.y * _cutJumpHeight);

                // Set that the button was released.
                _isJumpButtonWasReleased = true;
            }
        }

        // If the time after the falling is enough
        // perform a jump.
        if (_afterFallingTimer > 0 && _pressBeforeGroundTimer > 0)
        {
            // Set timers to zero to prevent multipy jumping.
            _afterFallingTimer = 0;
            _pressBeforeGroundTimer = 0;

            // Invoke Jump actions when player takes a jump.
            if (onJump != null)
            {
                onJump.Invoke();
            }

            // Set proper velocity to the player tp perform a jump.
            _characterRigidBody.velocity = new Vector2(_characterRigidBody.velocity.x, _jumpVelocity);
        }
    }


    // Check if the player is grounded or not.
    private bool IsGrounded()
    {
        // Set that the player was or not grounded.
        bool wasGrounded = _isGrounded;

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

                    // If player wasn`t grounded before, Invoke landing actions.
                    if (!wasGrounded)
                    {
                        if (onLand != null)
                        {
                            onLand.Invoke();
                        }
                    }
                }
            }
        }
        
        // Return status of player. Grounded or not.
        return _isGrounded;
    }


    private bool IsFalling()
    {
        bool isFalling = false;

        if (_characterRigidBody.velocity.y < -2f)
        {
            isFalling = true;
        }

        return isFalling;
    }


    // Check is jump button is enabled
    private bool IsButtonEnabled()
    {
        return _jumpButton.isActiveAndEnabled;
    }
}