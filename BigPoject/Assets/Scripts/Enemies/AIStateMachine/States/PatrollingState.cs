using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class PatrollingState : EnemyBaseState
    {
        public PatrollingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            Debug.Log("Patrol state");
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            EnemyAi.EnemyPatrol.PatrolArea();

            
            if (EnemyAi.PlayerCheck.IsPlayerDetected)
            {
                if (EnemyAi.IsAlwaysStanding)
                {
                    
                }
                else
                {
                    StateMachine.TransitionToState(EnemyAi.Chasing);   
                }
            }
        }


        public override void Exit()
        {

        }
    }
}
