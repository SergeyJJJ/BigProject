﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterJump : MonoBehaviour
{
    [Header("Jump controll")]
    [SerializeField] private float _jumpVelocity = 0;                      // Character jump velocity.
    [SerializeField] private float _afterGroundTouchJumpTime = 0f;         // The time during which the player can jump if he no longer touches the ground.
    [SerializeField] private float _pressBeforeGroundTime = 0f;            // The time during which player can press jump button before touching the ground, and jump will be performed.
    [Range(0f, 1f), SerializeField] private float _cutJumpHeight = 0f;     // The multiplier by which is multiplied the length of the jump when falling.
    
    [Space]
    [Header("Jump button")]
    [SerializeField] private CustomMovementButton _jumpButton = null;

    // Events.
    public delegate void OnJump();                                         
    public static event OnJump onJump;                                     // Event that contains things to do when player is jumping.
    public delegate void OnFalling();
    public static event OnFalling onFalling;                               // Event that contains thing to do when character is falling.
    public delegate void OnLand();                                         
    public static event OnLand onLand;                                     // Event that contains things to do when character is landing.

    private SurfaceCheck _surfaceCheck = null;
    private Rigidbody2D _characterRigidBody = null;                        // Contains character Rigidbody2d component.
    private float _afterGoundTouchTimer = 0;                               // Timer that count time after falling from the ground.
    private float _pressButtonTimer = 0;                                   // Timer that count time during which player can press jump button before touching the ground, and jump will be performed.
    private bool _wasGrounded = true;
    private bool _isFalling = false;                                       // Determine if player is falling.
    private bool _wasFalling = false;                                      // Determine if player was falling. 
    private bool _isJumping = false;                                       // Determine if player is already jumping.
    private bool _isButtonAlreadyReleased = true;                          // Check if button was released.
    


    private void Awake()
    {
        InitializeCharacterComponents();
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

            if (!_isJumping)
            {
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

                    _isJumping = true;
                }
            }
            
         
            //If character is on ground.
            if (_surfaceCheck.IsCharecterIsOnSurface())
            {
                //Set the last ground touch timer to the initial value.
                TimerController.SetToValue(ref _afterGoundTouchTimer, _afterGroundTouchJumpTime);
                
                //Invoke landing event if character was not touch the ground before.
                if (!_wasGrounded)
                {
                    InvokeOnLand();
                    _wasGrounded = true;
                }

            }

            //If character is falling
            if (IsFalling())
            {
                //Invoke falling event if character was not in the fall before.
                if (!_wasFalling)
                {
                    InvokeOnFalling();
                    _wasFalling = true;
                }
                _isJumping = false;
                _wasGrounded = false;
            }
            else
            {
                _wasFalling = false;

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
                //Reset timer that contains time, during which player can
                //press the jump button and jump will be performed.
                TimerController.SetToValue(ref _pressButtonTimer, _pressBeforeGroundTime);

                //Set that the button is now wasn`t released.
                _isButtonAlreadyReleased = false;
            }
        }
    }


    // Initialize rigidbody components.
    private void InitializeCharacterComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        _surfaceCheck = gameObject.GetComponent<SurfaceCheck>();
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


    // Check if player is falling now.
    private bool IsFalling()
    {
        _isFalling = false;

        if (_characterRigidBody.velocity.y < -2f)
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