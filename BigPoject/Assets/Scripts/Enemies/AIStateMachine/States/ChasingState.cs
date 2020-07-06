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
                _enemyStateMachine.TransitionToState(_enemyAI.Patrol);
            }
            else
            {
                _enemyAI.ChaseAction.ChasePlayer(_enemyAI.Player.transform, _enemyAI.TransformComponent,
                    _enemyAI.ChasingSpeed);
            }
        }


        public override void Exit()
        {

        }
        
        
        private bool IsPlayerDetected()
        {
            return _enemyAI.PlayerCheck.IsPlayerDetected;
        }
    }
}