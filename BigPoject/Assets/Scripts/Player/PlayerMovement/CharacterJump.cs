using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterJump : MonoBehaviour
{
    [Header("Jump controll")]
    [SerializeField] private float _jumpVelocity = 0;                      // Character jump velocity.
    [SerializeField] private float _afterGroundTouchJumpTime = 0f;             // The time during which the player can jump if he no longer touches the ground.
    [SerializeField] private float _pressBeforeGroundTime = 0f;            // The time during which player can press jump button before touching the ground, and jump will be performed.
    [Range(0f, 1f), SerializeField] private float _cutJumpHeight = 0f;     // The multiplier by which is multiplied the length of the jump when falling.
                                                                           // Allows you to adjust the dependence of the height of the jump on the duration of pressing.

    [Space]
    [Header("Is on ground controll")]
    [SerializeField] private Transform _groundCheckPoint = null;           // A position marking where to check if the character is grounded.
    [SerializeField] private float _grounCheckRadius = 0f;                 // Radius of the overlap circle to determine if character is grounded.
    [SerializeField] private LayerMask _whatIsGound = Physics2D.AllLayers; // A mask determine what is the ground for the character.

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


    private Rigidbody2D _characterRigidBody = null;                        // Hold character Rigidbody2d component.
    private float _afterGoundTouchTimer = 0;                                  // Timer that count time after falling from the ground.
    private float _pressButtonTimer = 0;                                   // Timer that count time during which player can press jump button before touching the ground, and jump will be performed.
    private bool _isGrounded = true;                                       // Determine if player is grounded now.
    private bool _isFalling = false;                                       // Determine if player is falling.
    private bool _isButtonAlreadyReleased = true;                          // Check if button was released.
    


    private void Awake()
    {
        InitializeRigidbodyComponents();
    }


    private void FixedUpdate()
    {
        //if button is isActiveAndEnabled and enabled.
        if (IsButtonEnabled())
        {
            //Decrease timers: time after last touch of the ground, 
            //and time during which player can press jump button 
            //the jump button and jump will be performed.
            TimerController.DecrementByDeltaTime(ref _afterGoundTouchTimer);
            TimerController.DecrementByDeltaTime(ref _pressButtonTimer);


            //If the conditions for the jump are met.
            if (IsCanJump(_afterGoundTouchTimer, _pressButtonTimer))
            {
                //Set timers to zero to prevent multiply jumping.
                TimerController.SetToZero(ref _afterGoundTouchTimer);
                TimerController.SetToZero(ref _pressButtonTimer);

                //Invoke jump event.
                InvokeOnJump();

                // Perform jump.
                PerformJump();
            }
            
            bool wasGrounded = _isGrounded;
            //If character is on ground.
            if (IsGrounded())
            {
                //Set the last ground touch timer to the initial value.
                TimerController.SetToValue(ref _afterGoundTouchTimer, _afterGroundTouchJumpTime);
                
                //Invoke landing event if character was not touch the ground before.
                if (!wasGrounded)
                {
                    InvokeOnLand();
                }
            }

            bool wasFalling = _isFalling;
            //If character is falling
            if (IsFalling())
            {
                //Invoke falling event if character was not in the fall before.
                if (!wasFalling)
                {
                    InvokeOnFalling();
                }
            }
            else
            {
                //If the button is released and it wasn`t released before.
                if (!IsButtonPressed() && !_isButtonAlreadyReleased)
                {

                    //Set that the button is now was released.
                    _isButtonAlreadyReleased = true;

                    //Cut the jump.
                    CutJump();
                }
            }

            //If button is pressed.
            if(IsButtonPressed())
            {
                //Reset timer that holds time, during which player can
                //press the jump button and jump will be performed.
                TimerController.SetToValue(ref _pressButtonTimer, _pressBeforeGroundTime);

                //Set that the button is now wasn`t released.
                _isButtonAlreadyReleased = false;
            }
        }
    }


    // Initialize rigidbody components.
    private void InitializeRigidbodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Perfom character jump.
    private void PerformJump()
    {
        // Set proper velocity to the player tp perform a jump.
        _characterRigidBody.velocity = new Vector2(_characterRigidBody.velocity.x, _jumpVelocity);
    }


    // Cut the jump by cutting player Y-Axis velocity
    // by multiplying it to less then one coefficient.
    private void CutJump()
    {
        _characterRigidBody.velocity = new Vector2(_characterRigidBody.velocity.x, _characterRigidBody.velocity.y * _cutJumpHeight);
    }


    // Invoke jump event.
    private void InvokeOnJump()
    {
        if (onJump != null)
        {
            onJump.Invoke();
        }
    }


    // Invoke landing event.
    private void InvokeOnLand()
    {
        if (onLand != null)
        {
            onLand.Invoke();
        }
    }


    // Invoke falling event.
    private void InvokeOnFalling()
    {
        if (onFalling != null)
        {
            onFalling.Invoke();
        }
    }


    // Check if the player is grounded or not.
    private bool IsGrounded()
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


    // Check if player is falling now.
    private bool IsFalling()
    {
        _isFalling = false;

        if (_characterRigidBody.velocity.y < -4f)
        {
            _isFalling = true;
        }

        return _isFalling;
    }



    // Check if player can perform jump.
    private bool IsCanJump(float afterFallingTimer, float pressButtonTimer)
    {
        return (afterFallingTimer > 0f) && (pressButtonTimer > 0f);
    }


    // Check if jump button is active and enabled.
    private bool IsButtonEnabled()
    {
        return _jumpButton.isActiveAndEnabled;
    }


    // Check if jump button is pressed.
    private bool IsButtonPressed()
    {
        return _jumpButton.IsPressed;
    }
}