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

        if (Mathf.Approximately(_characterMovement.RigidBody.velocity.x, 0f))
        {
            _stateMachine.TransitionToState(_characterMovement.Idle);
        }
        else 
        {
            _stateMachine.TransitionToState(_characterMovement.Running);
        }

        if (_characterMovement.UpMoveButton.IsPressed)
        {
            _stateMachine.TransitionToState(_characterMovement.Jumping);
        }
    }


    public override void Exit()
    {
        float afterGoundTouchTimer = _characterMovement.AfterGoundTouchTimer;
        TimerController.SetToValue(ref afterGoundTouchTimer, _characterMovement.AfterGroundTouchJumpTime);
        _characterMovement.AfterGoundTouchTimer = afterGoundTouchTimer;
    }
}
