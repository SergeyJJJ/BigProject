﻿using UnityEngine;

namespace LivingBeings.Enemies.AIStateMachine.States
{
    // Class that processes enemy`s patrolling behaviour.
    // Inherits from EnemyBaseState.
    public class PatrollingState : EnemyBaseState
    {
        public PatrollingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            base.Enter();
            
            EnemyAi.EnemyPatrol.SetFacingDirection();
            EnemyAi.EnemyPatrol.SetMovingAndStandingBoolsToDefault();
            EnemyAi.EnemyPatrol.StaySomeTime();
            Debug.Log("Patrol state");
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            EnemyAi.EnemyPatrol.PatrolArea(EnemyAi.TransformComponent, EnemyAi.RigidbodyComponent);
            
            if (EnemyAi.ChaseDetector.IsPlayerInDangerZone)
            {
                if (EnemyAi.IsAlwaysStanding)
                {
                    StateMachine.TransitionToState(EnemyAi.Attacking);
                }
                else
                {
                    StateMachine.TransitionToState(EnemyAi.Chasing);   
                }
            }
        }


        public override void Exit()
        {
            base.Exit();
            
            EnemyAi.EnemyPatrol.StopMove(EnemyAi.RigidbodyComponent);
        }
    }
}
