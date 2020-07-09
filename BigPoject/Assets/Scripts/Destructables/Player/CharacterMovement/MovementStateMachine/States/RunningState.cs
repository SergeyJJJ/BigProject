using GameBahaviour;
using UnityEngine;

namespace Destructables.Player.CharacterMovement.MovementStateMachine.States
{
    public class RunningState : BaseState
    {
        public RunningState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
        {

        }


        public override void Enter()
        {
            EventSystem.TriggerEvent("OnRun");
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

            bool isStartFalling = _characterMovement.RigidBody.velocity.y < -1f;
            bool isStopRunning = (_characterMovement.RigidBody.velocity.x < 1f) &&
                                 (_characterMovement.RigidBody.velocity.x > -1);
            bool isUpButtonPressed = _characterMovement.UpMoveButton.IsPressed;

            if (isStartFalling)
            {
                _stateMachine.TransitionToState(_characterMovement.Falling);
            }

            if (isStopRunning)
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
