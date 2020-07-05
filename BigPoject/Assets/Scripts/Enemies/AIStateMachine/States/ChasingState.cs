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

        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsPlayerDetected())
            {
                _enemyStateMachine.TransitionToState(_enemyAI.Wandering);
            }
        }


        public override void Exit()
        {

        }
        
        
        private bool IsPlayerDetected()
        {
            return _enemyAI.Detector.IsPlayerDetected;
        }
    }
}