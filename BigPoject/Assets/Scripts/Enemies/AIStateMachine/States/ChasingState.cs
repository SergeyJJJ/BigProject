﻿using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class ChasingState : EnemyBaseState
    {
        public ChasingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            Debug.Log("Chase state");
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsPlayerInAttackZone())
            {
                StateMachine.TransitionToState((EnemyAi.Attacking));
            }
            
            if (IsPlayerInChaseZone())
            {
                EnemyAi.EnemyChase.ChasePlayer(EnemyAi.Player.transform);
            }
            else
            {
                StateMachine.TransitionToState(EnemyAi.Patrolling);
            }
        }


        public override void Exit()
        {

        }
        
        
        private bool IsPlayerInChaseZone()
        {
            return EnemyAi.ChaseDetector.IsPlayerInDangerZone;
        }


        private bool IsPlayerInAttackZone()
        {
            return EnemyAi.AttackDetector.IsPlayerInAttackZone;
        }
    }
}