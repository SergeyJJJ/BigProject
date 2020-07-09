using UnityEngine;

namespace Creatures.Enemies.AIStateMachine.States
{
    public class PatrollingState : EnemyBaseState
    {
        public PatrollingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {
            base.Enter();
            
            EnemyAi.EnemyPatrol.SetFacingDirection();
            EnemyAi.EnemyPatrol.StaySomeTime();
            Debug.Log("Patrol state");
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            EnemyAi.EnemyPatrol.PatrolArea(EnemyAi.TransformComponent, EnemyAi.RigidbodyComponent);
            
            if (EnemyAi.ChaseDetector.IsPlayerInDangerZone)
            {
                if (EnemyAi.IsAlwaysStanding)
                {
                    StateMachine.TransitionToState(EnemyAi.Attacking);
                }
                else
                {
                    StateMachine.TransitionToState(EnemyAi.Chasing);   
                }
            }
        }


        public override void Exit()
        {
            base.Exit();
            
            EnemyAi.EnemyPatrol.StopMove(EnemyAi.RigidbodyComponent);
        }
    }
}
