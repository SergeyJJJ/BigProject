﻿using UnityEngine;

public class LandingState : BaseState
{
    public LandingState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
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
        _stateMachine.TransitionToState(_characterMovement.Jumping);
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

        if (Mathf.Approximately(_characterMovement.RigidBody.velocity.x, 0f))
        {
            _stateMachine.TransitionToState(_characterMovement.Idle);
        }
    }


    public override void Exit()
    {

    }
}