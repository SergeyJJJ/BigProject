using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class AttackingState : EnemyBaseState
    {
        public AttackingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            Debug.Log("Attack state");
        }


        public override void PhysicsUpdate()
        {
            
        }


        public override void Exit()
        {

        }
    }
}