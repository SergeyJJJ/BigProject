using UnityEngine;

public class IdleState : BaseState
{
    public IdleState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {

    }


    public override void HorizontalMovement(int direction)
    {
        base.HorizontalMovement(direction);
    }


    public override void RaisePlayerUp()
    {
        _stateMachine.TransitionToState(_characterMovement.Jumping);
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
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
        if (_characterMovement.RigidBody.velocity.y < -1f)
        {
            _stateMachine.TransitionToState(_characterMovement.Falling);
        }

        if (_characterMovement.RigidBody.velocity.x != 0)
        {
            _stateMachine.TransitionToState(_characterMovement.Running);
        }
    }


    public override void Exit()
    {

    }
} 