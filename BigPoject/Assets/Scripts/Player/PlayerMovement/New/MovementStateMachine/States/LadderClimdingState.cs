using UnityEngine;

public class LadderClimdingState : BaseState
{
    public LadderClimdingState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {
         
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

        if (_characterMovement.UpMoveButton.IsPressed)
        {
            _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x, _characterMovement.ClimbUpSpeed);
        }
        else if (!_characterMovement.UpMoveButton.IsPressed)
        {
            _characterMovement.RigidBody.velocity = new Vector2(_characterMovement.RigidBody.velocity.x, -_characterMovement.ClimbDownSpeed);
        }
    }


    public override void Exit()
    {

    }
}
