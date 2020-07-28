using UnityEngine;

namespace EntitiesWithHealth.Enemies.AIStateMachine.States
{
    // Class that processes enemy`s attack behaviour.
    // Inherits from EnemyBaseState
    public class AttackingState : EnemyBaseState
    {
        public AttackingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            base.Enter();
            EnemyAi.EnemyAttack.SetTimeBeforeAttackEqualZero();
            Debug.Log("Attack state");
            
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsPlayerInAttackZone())
            {
                EnemyAi.EnemyAttack.AttackPlayer();
            }
            else
            {
                if (EnemyAi.IsAlwaysStanding)
                {
                    StateMachine.TransitionToState(EnemyAi.Patrolling);
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
        }
        
        
        private bool IsPlayerInAttackZone()
        {
            return EnemyAi.AttackDetector.IsPlayerInAttackZone;
        }
    }
}