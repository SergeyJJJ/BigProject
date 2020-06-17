using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {
        _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x,
                                                            _characterMovement.JumpHeight);
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

        if (!_characterMovement.UpMoveButton.IsPressed)
        {
            _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x,
                                                                _characterMovement.RigidBody.velocity.y *
                                                                _characterMovement.CutJumpHeight);
            _stateMachine.TransitionToState(_characterMovement.Falling);
        }
        else if (_characterMovement.UpMoveButton.IsPressed)
        {
            float pressButtonTimer = _characterMovement.PressButtonTimer;
            TimerController.SetToValue(ref pressButtonTimer, _characterMovement.PressButtonTimer);
            _characterMovement.PressButtonTimer = pressButtonTimer;
        }

        float afterGoundTouchTimer = _characterMovement.AfterGoundTouchTimer;
        TimerController.DecrementByDeltaTime(ref afterGoundTouchTimer);
        _characterMovement.AfterGoundTouchTimer = afterGoundTouchTimer;
    }


    public override void Exit()
    {

    }
} 