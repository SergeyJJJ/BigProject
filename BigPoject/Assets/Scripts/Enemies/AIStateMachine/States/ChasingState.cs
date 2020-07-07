using UnityEngine;

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

            if (!IsPlayerInChaseZone())
            {
                StateMachine.TransitionToState(EnemyAi.Patrolling);
            }
            else
            {
                EnemyAi.EnemyChase.ChasePlayer(EnemyAi.Player.transform);
            }
        }


        public override void Exit()
        {

        }
        
        
        private bool IsPlayerInChaseZone()
        {
            return EnemyAi.ChaseDetector.IsPlayerDetected;
        }
    }
}