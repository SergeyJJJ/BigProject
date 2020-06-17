using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInteractionState : BaseState
{
    public PlatformInteractionState (CharacterMovement characterMovement, StateMachine stateMachine) : base(characterMovement, stateMachine)
    {
    
    }

    public override void Enter()
    {
        _characterMovement.gameObject.transform.SetParent(_characterMovement.SurfaceCheck.GetSurfaceOnWhichPlayerStanding().transform);
    }



    public override void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
      
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        bool isStartJumping = _characterMovement.RigidBody.velocity.y > 1;
        bool isStartFalling = _characterMovement.RigidBody.velocity.y < -1;
        bool isStartRunning = (_characterMovement.RigidBody.velocity.x < -1) &&
                              (_characterMovement.RigidBody.velocity.x > 1);

        if (_characterMovement.SurfaceCheck.GetSurfaceOnWhichPlayerStanding() == null)
        {
            if (isStartJumping)
            {
                _stateMachine.TransitionToState(_characterMovement.Jumping);
            }
            else if (isStartFalling)
            {
                _stateMachine.TransitionToState(_characterMovement.Falling);
            }
            else if (isStartRunning)
            {
                _stateMachine.TransitionToState(_characterMovement.Running);
            }
            else
            {
                _stateMachine.TransitionToState(_characterMovement.Idle);   
            }
        } 
    }


    public override void Exit()
    {
        _characterMovement.gameObject.transform.SetParent(null);
    }

}
