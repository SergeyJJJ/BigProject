using UnityEngine;

namespace LivingBeings.Player.CharacterMovement.MovementStateMachine
{
    public abstract class BaseState
    {
        protected CharacterMovement _characterMovement = null;                    // Reference to character movement script.
        protected StateMachine _stateMachine = null;                              // Reference to state machine.

        private Vector2 _currentVelocity = Vector2.zero;                          // Current speed of change in characters velocity.

        protected BaseState(CharacterMovement characterMovement, StateMachine stateMachine)
        {
            _characterMovement = characterMovement;
            _stateMachine = stateMachine;
        }


        public virtual void Enter()
        {
            
        }


        public virtual void OnTriggerEnter2D(Collider2D other)
        {
        
        }


        public virtual void OnTriggerExit2D(Collider2D other)
        {

        }


        public virtual void PhysicsUpdate()
        {
            Vector2 targetVelocity = new Vector2(0, _characterMovement.RigidBody.velocity.y);
            bool isLeftButtonPressed = _characterMovement.LeftMoveButton.IsPressed;
            bool isRightButtonPressed = _characterMovement.RightMoveButton.IsPressed;

            // If player press left movement button and release right movement button.
            if (isLeftButtonPressed && !isRightButtonPressed)
            {
                // Set tarhet velocity.
                targetVelocity = new Vector2(-_characterMovement.HorizontalSpeed, _characterMovement.RigidBody.velocity.y);

                if(_characterMovement.IsFacingRight)
                {
                    Flip();
                }
            }
            // If player press right movement button and release left movement button.
            else if (isRightButtonPressed && !isLeftButtonPressed)
            {
                // Set tarhet velocity.
                targetVelocity = new Vector2(_characterMovement.HorizontalSpeed, _characterMovement.RigidBody.velocity.y);

                if(!_characterMovement.IsFacingRight)
                {
                    Flip();
                }
            }

            // Set smoothed velocity to the character.
            _characterMovement.RigidBody.velocity = Vector2.SmoothDamp(_characterMovement.RigidBody.velocity,
                targetVelocity, ref _currentVelocity,
                _characterMovement.MovementSmoothing);
        }


        public virtual void Exit()
        {

        }

        // Flip player to another side(left or right).
        private void Flip()
        {
            _characterMovement.IsFacingRight = !_characterMovement.IsFacingRight;

            _characterMovement.Transform.Rotate(0f, 180f, 0f);
        }
    }
}
