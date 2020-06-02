﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{    
    [Header("Movement controll")]
    [SerializeField] private float _horizontalSpeed = 0f;                     // How fast character can run horizontally.
    [SerializeField] private float _verticalSpeed = 0f;                       // How fast character can run vertically.
    [Range(0f, 0.4f)] [SerializeField] private float _movementSmoothing = 0f; // How mouch to smouth out character movement.  

    [Space]
    [Header("Controll buttons")]
    [SerializeField] private CustomMovementButton _leftButton = null;
    [SerializeField] private CustomMovementButton _rightButton = null;
    [SerializeField] private CustomMovementButton _upButton = null;
    [SerializeField] private CustomMovementButton _downButton = null;
    
    private const short StopMovementSpeed = 0;                                // Value indicating that the object is not moving in some direction.
    private Vector3 _currentVelocity = Vector2.zero;                          // Hold curent velocity from Vector3.SmoothDump().
    private Rigidbody2D _characterRigidBody;                                  // Hold character Rigidbody2d component.
    private bool _isFacingRight = false;                                       // To determine which way the player is currently facing.

    private void Start()
    {
        InitializeRigidBodyComponents();
    }


    private void FixedUpdate()
    {
        // If appropriate buttons is enabled allow horizontal movement.
        if (IsHorizontalMovementButtonsEnabled())
        {
            HorizontalMovement();
        }

        // If appropriate buttons is enabled allow vertical movement.
        if (IsVerticalMovementButtonsEnabled())
        {
            VerticalMovement();
        }

        Flip();
    }


    // Method that initialize RigidBodyComponents.
    private void InitializeRigidBodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Method that perform character movement calculation,
    // and actually move character in a horizontal direction.
    private void HorizontalMovement()
    {
        // Find target character movement velocity.
        Vector2 targetVelocity = GetHorizontalTargetVelocity();

        SetSmoothedVelocity(targetVelocity);                             
    }


    // Method that perform character movement calculation,
    // and actually move character in a vertical direction.
    private void VerticalMovement()
    {
        // Find target character movement velocity.
        Vector2 targetVelocity = GetVerticalTargetVelocity();

        SetSmoothedVelocity(targetVelocity);                                
    }


    // Snooth out velocity and apply it to the character.
    private void SetSmoothedVelocity(Vector2 targetVelocity)
    {
        _characterRigidBody.velocity = Vector3.SmoothDamp(_characterRigidBody.velocity, targetVelocity,
                                                          ref _currentVelocity, _movementSmoothing);
    }


    // Method that return horizontal target velocity based on button input.
    private Vector2 GetHorizontalTargetVelocity()
    {
        Vector2 targetVelocity = Vector2.zero;

        // Check what button is pressed and set appropriate velocity.
        if (_leftButton.IsPressed)
        {
            targetVelocity = new Vector2(-_horizontalSpeed, _characterRigidBody.velocity.y);
        }
        else if (_rightButton.IsPressed)
        {
            targetVelocity = new Vector2(_horizontalSpeed, _characterRigidBody.velocity.y);
        }
        else
        {
            targetVelocity = new Vector2(StopMovementSpeed, _characterRigidBody.velocity.y);
        }

        // Return needed velocity.
        return targetVelocity;
    }


    // Method that return vertival target velocity based on button input.
    private Vector2 GetVerticalTargetVelocity()
    {
        Vector2 targetVelocity = Vector2.zero;

        // Check what button is pressed and set appropriate velocity.
        if (_upButton.IsPressed)
        {
            targetVelocity = new Vector2(_characterRigidBody.velocity.x, _verticalSpeed);
        }
        else if (_downButton.IsPressed)
        {
            targetVelocity = new Vector2(_characterRigidBody.velocity.x, -_verticalSpeed);
        }
        else
        {
            targetVelocity = new Vector2(_characterRigidBody.velocity.x, StopMovementSpeed);
        }

        // Return needed velocity.
        return targetVelocity;
    }


    private void Flip()
    {
        if (_characterRigidBody.velocity.x > 0 && _isFacingRight)
        {
            RevertCharacter();
        }
        else if (_characterRigidBody.velocity.x < 0 && !_isFacingRight)
        {
            RevertCharacter();
        }
    }


    private void RevertCharacter()
    {
        // Switch the way the player is labelled as facing.
		_isFacingRight = !_isFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }


    private bool IsHorizontalMovementButtonsEnabled()
    {
        return _leftButton.isActiveAndEnabled && _rightButton.isActiveAndEnabled;
    }

    private bool IsVerticalMovementButtonsEnabled()
    {
        return _upButton.isActiveAndEnabled && _downButton.isActiveAndEnabled;
    }
}