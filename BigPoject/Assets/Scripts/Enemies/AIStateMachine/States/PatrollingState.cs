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
        }


        public override void Exit()
        {

        }
        
        
        protected bool IsPlayerDetected()
        {
            return _enemyAI.PlayerCheck.IsPlayerDetected;
        }
    }
}
