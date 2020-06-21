using UnityEngine;

namespace Assets.Scripts.Player.CharacterMovement.MovementStateMachine.States
{
    public class FallingState : BaseState
    {
        public FallingState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
        {

        }


        public override void Enter()
        {
            CharacterEventSystem.TriggerEvent("OnFall");
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

            _characterMovement.PressButtonTimer -= Time.deltaTime;

            if (isUpButtonPressed)
            {
                _characterMovement.PressButtonTimer = _characterMovement.PressBeforeGroundTime;

                if (_characterMovement.AfterGroundTouchTimer > 0f)
                {
                    _stateMachine.TransitionToState(_characterMovement.Jumping);
                }
            }

            if (_characterMovement.SurfaceCheck.IsCharecterIsOnSurface())
            {
                _stateMachine.TransitionToState(_characterMovement.Landing);
            }

            _characterMovement.AfterGroundTouchTimer -= Time.deltaTime;
        }


        public override void Exit()
        {

        }
    }
}
