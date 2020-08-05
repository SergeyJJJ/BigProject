using UnityEngine;

namespace Living_beings.Player.CharacterMovement.MovementStateMachine.States
{
    public class ClimbingState : BaseState
    {
        public ClimbingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
        {
    
        }

        public override void Enter()
        {
            EventSystem.EventSystem.TriggerEvent("OnStartClimb");
        }


        public override void OnTriggerEnter2D(Collider2D other)
        {
        
        }


        public override void OnTriggerExit2D(Collider2D other)
        {
            bool isStartFalling = _characterMovement.RigidBody.velocity.y < -1;
            bool isStartJumping = _characterMovement.RigidBody.velocity.y > 1;
            bool isStartRunning = (_characterMovement.RigidBody.velocity.x > 1) ||
                                       (_characterMovement.RigidBody.velocity.x < -1);
            
            if (other.CompareTag("Ladder"))
            {
                if (isStartFalling)
                {
                    _stateMachine.TransitionToState(_characterMovement.Falling);
                }
                else if (isStartJumping)
                {
                    _stateMachine.TransitionToState(_characterMovement.Jumping);
                }
                else if (isStartRunning)
                {
                    _stateMachine.TransitionToState(_characterMovement.Running);
                }
            }
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        
            bool isUpButtonPressed = _characterMovement.UpMoveButton.IsPressed;

            if (isUpButtonPressed)
            {
                _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x,
                    _characterMovement.ClimbUpSpeed);
            }
            else if (!isUpButtonPressed)
            {
                _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x,
                    -_characterMovement.ClimbDownSpeed);
            }
        }


        public override void Exit()
        {
            EventSystem.EventSystem.TriggerEvent("OnStopClimb");
        }
    }
}