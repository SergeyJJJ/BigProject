using UnityEngine;

namespace Living_beings.Player.CharacterMovement.MovementStateMachine.States
{
    public class JumpingState : BaseState
    {
        public JumpingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
        {
    
        }

        public override void Enter()
        {
            EventSystem.EventSystem.TriggerEvent("OnJump");

            _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x,
                _characterMovement.JumpHeight);
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
            bool isUpButtonPressed = _characterMovement.UpMoveButton.IsPressed;
            bool isStartFalling = _characterMovement.RigidBody.velocity.y < 1f;

            if (!isUpButtonPressed)
            {
                _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x,
                    _characterMovement.RigidBody.velocity.y *
                    _characterMovement.CutJumpHeight);
            }
            else if (isUpButtonPressed)
            {
                _characterMovement.PressButtonTimer = _characterMovement.PressBeforeGroundTime;
            }

            if(isStartFalling)
            {
                _stateMachine.TransitionToState(_characterMovement.Falling);
            }

            _characterMovement.AfterGroundTouchTimer -= Time.deltaTime;
        }


        public override void Exit()
        {

        }
    }
} 