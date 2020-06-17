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


    public override void RaisePlayerUpInput(bool raiseUp)
    {
        if (raiseUp)
        {
            float pressButtonTimer = _characterMovement.PressButtonTimer;
            TimerController.SetToValue(ref pressButtonTimer, _characterMovement.PressBeforeGroundTime);
            _characterMovement.PressButtonTimer = pressButtonTimer;

            if (_characterMovement.AfterGoundTouchTimer > 0f)
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

        float pressButtonTimer = _characterMovement.PressButtonTimer;
        TimerController.DecrementByDeltaTime(ref pressButtonTimer);
        _characterMovement.PressButtonTimer = pressButtonTimer;
    }


    public override void Exit()
    {

    }
}
