using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {
        _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x, _characterMovement.JumpHeight);
    }


    public override void HorizontalMovement(int direction)
    {
        base.HorizontalMovement(direction);
    }


     public override void RaisePlayerUp()
    {

    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            //_stateMachine.TransitionToState(LadderClimbingState);
        }
    }


    public override void Exit()
    {

    }
} 