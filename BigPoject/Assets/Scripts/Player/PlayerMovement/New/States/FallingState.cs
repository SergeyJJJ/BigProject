using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public FallingState (CharacterMovement characterMovement, StateMachine stateMachine) : base (characterMovement, stateMachine)
    {

    }


    public override void Enter()
    {

    }


    public override void HorizontalMovementInput(int direction)
    {
        base.HorizontalMovementInput(direction);
    }


    public override void RaisePlayerUpInput(int direction)
    {
        
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

        Collider2D[] _colliders = null;
        _colliders = GetEncouteredColliders();

        if (_colliders != null)
        {
            _stateMachine.TransitionToState(_characterMovement.Landing);
        }
    }


    public override void Exit()
    {

    }


    private Collider2D[] GetEncouteredColliders()
    {
        Collider2D[] allColliders = null;

        // If point around which we check colliders is exist.
        if (_characterMovement.GroundCheckPoint != null)
        {
            // Get all colliders with wich we overlap around point with certain radius.
            allColliders = Physics2D.OverlapCircleAll(_characterMovement.GroundCheckPoint.position,
                                                      _characterMovement.GrounCheckRadius, _characterMovement.WhatIsGround);
        }

        // If player is not stay on something, return null.
        if (allColliders.Length == 0)
        {
            return null;
        }
        

        Collider2D[] filteredColliders = new Collider2D[allColliders.Length];
        int filteredCollidersIndex = 0;

        // Remove character colliders from the colliders list.
        for (var colliderIndex = 0; colliderIndex < allColliders.Length; colliderIndex++)
        {
            // If some collider is not belong to the player
            // add it to filtered colliders list.
            if ( allColliders[colliderIndex].gameObject != _characterMovement.gameObject)
            {
                filteredColliders[filteredCollidersIndex] = allColliders[colliderIndex];
            }
        }

        return filteredColliders;
    }
}
