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
        bool isStopRunning = _characterMovement.RigidBody.velocity.x < 1f;
        bool isUpButtonPressed = _characterMovement.UpMoveButton.IsPressed;

        if (isStopRunning)
        {
            _stateMachine.TransitionToState(_characterMovement.Idle);
        }
        else 
        {
            _stateMachine.TransitionToState(_characterMovement.Running);
        }

        if (isUpButtonPressed)
        {
            _stateMachine.TransitionToState(_characterMovement.Jumping);
        }

        if (_characterMovement.SurfaceCheck.IsCharecterIsOnSurface())
        {
            if (_characterMovement.SurfaceCheck.GetSurfaceOnWhichPlayerStanding().CompareTag("MovingPlatform"))
            {
                _stateMachine.TransitionToState(_characterMovement.PlatformInteraction);
            }
        }
    }


    public override void Exit()
    {
        float afterGoundTouchTimer = _characterMovement.AfterGoundTouchTimer;
        TimerController.SetToValue(ref afterGoundTouchTimer, _characterMovement.AfterGroundTouchJumpTime);
        _characterMovement.AfterGoundTouchTimer = afterGoundTouchTimer;
    }
}
