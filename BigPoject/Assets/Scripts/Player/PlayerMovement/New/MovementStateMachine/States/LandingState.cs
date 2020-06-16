using UnityEngine;

public class LandingState : BaseState
{
    public LandingState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
    {
        
    }


    public override void Enter()
    {
        if (_characterMovement.PressButtonTimer > 0)
        {
            _stateMachine.TransitionToState(_characterMovement.Jumping);
        }
    }


    public override void HorizontalMovementInput(int direction)
    {
        base.HorizontalMovementInput(direction);
    }


    public override void RaisePlayerUpInput(int direction)
    {
        if (direction == 1)
        {
            _stateMachine.TransitionToState(_characterMovement.Jumping);
        }
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (Mathf.Approximately(_characterMovement.RigidBody.velocity.x, 0f))
        {
            _stateMachine.TransitionToState(_characterMovement.Idle);
        }
        else 
        {
            _stateMachine.TransitionToState(_characterMovement.Running);
        }
    }


    public override void Exit()
    {
        float afterGoundTouchTimer = _characterMovement.AfterGoundTouchTimer;
        TimerController.SetToValue(ref afterGoundTouchTimer, _characterMovement.AfterGroundTouchJumpTime);
        _characterMovement.AfterGoundTouchTimer = afterGoundTouchTimer;
    }
}
