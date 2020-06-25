using UnityEngine;

namespace Player.CharacterMovement.MovementStateMachine.States
{
    public class LandingState : BaseState
    {
        public LandingState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
        {
        
        }


        public override void Enter()
        {
            EventSystem.TriggerEvent("OnLand");

            if (_characterMovement.PressButtonTimer > 0)
            {
                _stateMachine.TransitionToState(_characterMovement.Jumping);
            }
        }


        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ladder"))
            {
                _stateMachine.TransitionToState(_characterMovement.Climbing);
            }
        }


        public override void OnTriggerExit2D(Collider2D other)
        {
        
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            bool isRunning = (_characterMovement.RigidBody.velocity.x < -1f) || (_characterMovement.RigidBody.velocity.x > 1f);
            bool isUpButtonPressed = _characterMovement.UpMoveButton.IsPressed;

            if (isRunning)
            {
                _stateMachine.TransitionToState(_characterMovement.Running);
            }
            else 
            {
                _stateMachine.TransitionToState(_characterMovement.Idle);   
            }

            if (isUpButtonPressed)
            {
                _stateMachine.TransitionToState(_characterMovement.Jumping);
            }
        }


        public override void Exit()
        {
            _characterMovement.AfterGroundTouchTimer = _characterMovement.AfterGroundTouchJumpTime;
        }
    }
}
