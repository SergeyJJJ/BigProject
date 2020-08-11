using System;
using Living_beings.Player;
using UnityEngine;

namespace JetPackMiniGame
{
    public class JetPackCharacterMovement : MonoBehaviour
    {
        [Header("Horizontal movement control")]
        [SerializeField] private float _horizontalSpeed = 0f;                     // Character`s horizontal speed.
        [Range(0f, 0.4f), SerializeField] private float _movementSmoothing = 0f;  // Coefficient of character`s horizontal movement smoothing.
        [SerializeField] private CustomButton _leftMoveButton = null;             // Left movement button.     
        [SerializeField] private CustomButton _rightMoveButton = null;            // Right movement  button.

        private Vector2 _currentVelocity = Vector2.zero;                          // Current speed of change in characters velocity.
        private static bool _isFacingRight = false;                               // Check if player is facing right.

        private Rigidbody2D _rigidBody = null;                                    // Charecters Rigidbody2D component.

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            _isFacingRight = GetStartFacingDirection();
        }
        
        
        private void FixedUpdate()
        {
            Vector2 targetVelocity = new Vector2(0, _rigidBody.velocity.y);
            bool isLeftButtonPressed = _leftMoveButton.IsPressed;
            bool isRightButtonPressed = _rightMoveButton.IsPressed;

            // If player press left movement button and release right movement button.
            if (isLeftButtonPressed && !isRightButtonPressed)
            {
                // Set tarhet velocity.
                targetVelocity = new Vector2(-_horizontalSpeed, _rigidBody.velocity.y);

                if(_isFacingRight)
                {
                    Flip();
                }
            }
            // If player press right movement button and release left movement button.
            else if (isRightButtonPressed && !isLeftButtonPressed)
            {
                // Set tarhet velocity.
                targetVelocity = new Vector2(_horizontalSpeed, _rigidBody.velocity.y);

                if(!_isFacingRight)
                {
                    Flip();
                }
            }

            // Set smoothed velocity to the character.
            _rigidBody.velocity = Vector2.SmoothDamp(_rigidBody.velocity,
                targetVelocity, ref _currentVelocity,
                _movementSmoothing);
        }
        
        
        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        
        
        private bool GetStartFacingDirection()
        {
            return Mathf.Approximately(transform.rotation.y, 0f) ? true : false;
        }
    }
}
