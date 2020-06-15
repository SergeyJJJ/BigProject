using UnityEngine;

public class LadderClimdingState : BaseState
{
    public LadderClimdingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {

    }


    public override void HorizontalInput(int direction)
    {
        base.HorizontalInput(direction);
    }


    public override void RaisePlayerUp()
    {
        Debug.Log("Climb");
        _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x, _characterMovement.ClimbUpSpeed);
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            if (_characterMovement.RigidBody.velocity.y < 0)
            {
                _stateMachine.TransitionToState(_characterMovement.Falling);
            }
            else if (_characterMovement.RigidBody.velocity.y > 0)
            {
                _stateMachine.TransitionToState(_characterMovement.Jumping);
            }
            else
            {
                _stateMachine.TransitionToState(_characterMovement.Idle);
            }
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void Exit()
    {

    }
}
