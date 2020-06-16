using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public FallingState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
    {

    }


    public override void Enter()
    {

    }


    public override void HorizontalMovementInput(int direction)
    {
        base.HorizontalMovementInput(direction);
    }


    public override void RaisePlayerUpInput(int direction)
    {
        if (_characterMovement.AfterGoundTouchTimer > 0f)
        {
            if (direction == 1)
            {
                _stateMachine.TransitionToState(_characterMovement.Jumping);
            }
        }
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _stateMachine.TransitionToState(_characterMovement.LadderClimbing);
        }
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_characterMovement.SurfaceCheck.IsCharecterIsOnSurface())
        {
            _stateMachine.TransitionToState(_characterMovement.Landing);
        }

        float afterGoundTouchTimer = _characterMovement.AfterGoundTouchTimer;
        TimerController.DecrementByDeltaTime(ref afterGoundTouchTimer);
        _characterMovement.AfterGoundTouchTimer = afterGoundTouchTimer;
    }


    public override void Exit()
    {

    }
}
