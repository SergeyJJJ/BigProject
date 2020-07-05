namespace Enemies.AIStateMachine.States
{
    public abstract class PatrollingState : EnemyBaseState
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
        }


        public override void Exit()
        {

        }
    }
}
