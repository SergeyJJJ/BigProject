using UnityEngine;

public class RunningState : BaseState
{
    public RunningState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
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
        if (direction == 1)
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

        if (_characterMovement.RigidBody.velocity.y < -1f)
        {
            _stateMachine.TransitionToState(_characterMovement.Falling);
        }

        if (Mathf.Approximately(_characterMovement.RigidBody.velocity.x, 0f))
        {
            _stateMachine.TransitionToState(_characterMovement.Idle);
        }
    }


    public override void Exit()
    {
        float afterGoundTouchTimer = _characterMovement.AfterGoundTouchTimer;
        TimerController.SetToValue(ref afterGoundTouchTimer, _characterMovement.AfterGroundTouchJumpTime);
        _characterMovement.AfterGoundTouchTimer = afterGoundTouchTimer;
    }
}
