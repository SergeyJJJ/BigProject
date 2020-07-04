using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class AttackingState : EnemyBaseState
    {
        public AttackingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public virtual void Enter()
        {

        }


        public virtual void PhysicsUpdate()
        {
            
        }


        public virtual void Exit()
        {

        }
    }
}