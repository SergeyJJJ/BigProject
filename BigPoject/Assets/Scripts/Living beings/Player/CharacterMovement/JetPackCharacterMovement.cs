using UnityEngine;

namespace Living_beings.Player.CharacterMovement
{
    public class JetPackCharacterMovement : MonoBehaviour
    {
        [Header("Horizontal movement control")]
        [SerializeField] private float _horizontalSpeed = 0f;                     // Character`s horizontal speed.
        [SerializeField] private float _speedLimit = 0f;                          // Character`s speed limit.
        [SerializeField] private CustomButton _leftMoveButton = null;             // Left movement button.     
        [SerializeField] private CustomButton _rightMoveButton = null;            // Right movement  button.
        
        private static bool _isFacingRight = false;                               // Check if player is facing right.

        private Rigidbody2D _rigidbody = null;                                    // Characters Rigidbody2D component.

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            _isFacingRight = GetStartFacingDirection();
        }
        
        
        private void FixedUpdate()
        {
            bool isLeftButtonPressed = _leftMoveButton.IsPressed;
            bool isRightButtonPressed = _rightMoveButton.IsPressed;

            // If player press left movement button and release right movement button.
            if (isLeftButtonPressed && !isRightButtonPressed)
            {
                Vector2 leftDirectionSpeed = _horizontalSpeed * Vector2.left;

                _rigidbody.AddForce(leftDirectionSpeed, ForceMode2D.Impulse);

                if (_rigidbody.velocity.x < -_speedLimit)
                {
                    // Clamp characters speed between current speed and speed limit.
                    _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speedLimit);
                }

                if(_isFacingRight)
                {
                    Flip();
                }
            }
            // If player press right movement button and release left movement button.
            else if (isRightButtonPressed && !isLeftButtonPressed)
            {
                Vector2 rightDirectionSpeed = _horizontalSpeed * Vector2.right;

                _rigidbody.AddForce(rightDirectionSpeed, ForceMode2D.Impulse);

                if (_rigidbody.velocity.x > _speedLimit)
                {
                    // Clamp characters speed between current speed and speed limit.
                    _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speedLimit);
                }

                if(!_isFacingRight)
                {
                    Flip();
                }
            }
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
