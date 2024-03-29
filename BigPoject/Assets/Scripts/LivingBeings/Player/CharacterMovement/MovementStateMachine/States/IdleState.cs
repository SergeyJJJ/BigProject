using GameBehaviour;
using UnityEngine;

namespace LivingBeings.Player.CharacterMovement.MovementStateMachine.States
{
    public class IdleState : BaseState
    {
        public IdleState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
        {
    
        }

        public override void Enter()
        {
            base.Enter();
            
            EventSystem.TriggerEvent("OnStop");
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
            
            bool isStartRunning = (_characterMovement.RigidBody.velocity.x > 1) ||
                                  (_characterMovement.RigidBody.velocity.x < -1);
            bool isStartFalling = _characterMovement.RigidBody.velocity.y < -1f;
            bool isUpButtonPressed = _characterMovement.UpMoveButton.IsPressed;

            if (_characterMovement.SurfaceCheck.IsCharacterIsOnInclinedSurface())
            {
                _characterMovement.RigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                _characterMovement.RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (isStartFalling)
            {
                _stateMachine.TransitionToState(_characterMovement.Falling);
            }

            if (isStartRunning)
            {
                _stateMachine.TransitionToState(_characterMovement.Running);
            }

            if (isUpButtonPressed)
            {
                _stateMachine.TransitionToState(_characterMovement.Jumping);
            }
        }


        public override void Exit()
        {
            _characterMovement.AfterGroundTouchTimer = _characterMovement.AfterGroundTouchJumpTime;
            _characterMovement.RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
} 