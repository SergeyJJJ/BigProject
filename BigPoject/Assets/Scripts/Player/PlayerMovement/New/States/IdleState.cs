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
        _stateMachine.TransitionToState(_characterMovement.JumpingState);
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            //_stateMachine.TransitionToState(_characterMovement.LadderClimbingState);
        }
    }


    public override void Exit()
    {

    }
} 