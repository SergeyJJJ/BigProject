using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class StandingPatrolState : PatrollingState
    {
        public StandingPatrolState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {

        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        public override void Exit()
        {

        }
    }
}
