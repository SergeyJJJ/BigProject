using UnityEngine;

public class IdleState : BaseState
{
    public IdleState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {

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
        _characterMovement.AfterGoundTouchTimer = _characterMovement.AfterGroundTouchJumpTime;
    }
} 