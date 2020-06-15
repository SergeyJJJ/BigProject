using UnityEngine;

public class LadderClimdingState : BaseState
{
    public LadderClimdingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
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

    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        
    }


    public override void PhysicsUpdate()
    {
        
    }


    public override void Exit()
    {

    }
}
