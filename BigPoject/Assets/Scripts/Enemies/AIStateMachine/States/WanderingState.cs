using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class WanderingState : EnemyBaseState
    {
        public WanderingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        public override void Enter()
        {
            base.Enter();
        }


        public virtual void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        public virtual void Exit()
        {
            base.Exit();
        }
    }
}