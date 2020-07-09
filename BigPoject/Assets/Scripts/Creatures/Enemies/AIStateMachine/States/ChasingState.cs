using UnityEngine;

namespace Creatures.Enemies.AIStateMachine.States
{
    public class ChasingState : EnemyBaseState
    {
        public ChasingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {
            
        }
        
        
        public override void Enter()
        {    
            base.Enter();
            
            EnemyAi.EnemyChase.SetFacingDirection();
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
                EnemyAi.EnemyChase.ChasePlayer(EnemyAi.Player.transform, EnemyAi.RigidbodyComponent);
            }
            else
            {
                StateMachine.TransitionToState(EnemyAi.Patrolling);
            }
        }


        public override void Exit()
        {
            base.Exit();
            
            EnemyAi.EnemyChase.StopMove(EnemyAi.RigidbodyComponent);
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