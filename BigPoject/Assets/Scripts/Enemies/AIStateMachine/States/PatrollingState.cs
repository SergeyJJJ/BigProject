namespace Enemies.AIStateMachine.States
{
    public class PatrollingState : EnemyBaseState
    {
        public PatrollingState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {

        }
        
        
        public override void Enter()
        {

        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            EnemyAi.EnemyPatrol.PatrolArea();
        }


        public override void Exit()
        {

        }
    }
}
