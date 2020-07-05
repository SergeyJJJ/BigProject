using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class StandingPatrolState : PatrollingState
    {
        private float _standingTimer = 0f;
        
        public StandingPatrolState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            base.Enter();
            
            if (!_enemyAI.IsAlwaysStanding)
            {
                _standingTimer = _enemyAI.StandingDuration;
            }
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsPlayerDetected())
            {
                _enemyStateMachine.TransitionToState(_enemyAI.Attacking);
            }

            if (!_enemyAI.IsAlwaysStanding)
            {
                _standingTimer -= Time.deltaTime;

                if (_standingTimer < 0)
                {
                    _enemyStateMachine.TransitionToState(_enemyAI.Wandering);
                }
            }
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}
