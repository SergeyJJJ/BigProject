using UnityEngine;

namespace Enemies.AIStateMachine
{
    public abstract class EnemyBaseState
    {
        protected EnemyAI _enemyAI = null;                       // Reference to enemy`s AI script.
        protected EnemyStateMachine _enemyStateMachine = null;   // Reference to enemy`s state machine.


        protected EnemyBaseState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine)
        {
            _enemyAI = enemyAI;
            _enemyStateMachine = enemyStateMachine;
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
