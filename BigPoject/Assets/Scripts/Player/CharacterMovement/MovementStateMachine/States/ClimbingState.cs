using System;
using UnityEngine;

namespace Assets.Scripts.Player.CharacterMovement.MovementStateMachine.States
{
    public class ClimbingState : BaseState
    {
        public ClimbingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
        {
    
        }

        public override void Enter()
        {
            CharacterEventSystem.TriggerEvent("OnStartClimb");
        }


        public override void OnTriggerEnter2D(Collider2D other)
        {
        
        }


        public override void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Ladder"))
            {
                if (_characterMovement.RigidBody.velocity.y < 0)
                {
                    _stateMachine.TransitionToState(_characterMovement.Falling);
                }
                else if (_characterMovement.RigidBody.velocity.y > 0)
                {
                    _stateMachine.TransitionToState(_characterMovement.Jumping);
                }
                else
                {
                    _stateMachine.TransitionToState(_characterMovement.Idle);
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
            CharacterEventSystem.TriggerEvent("OnStopClimb");
        }
    }
}