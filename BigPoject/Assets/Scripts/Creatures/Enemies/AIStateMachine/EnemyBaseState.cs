namespace Creatures.Enemies.AIStateMachine
{
    public abstract class EnemyBaseState
    {
        private EnemyAI _enemyAI = null;                       // Reference to enemy`s AI script.
        private EnemyStateMachine _enemyStateMachine = null;   // Reference to enemy`s state machine.

        #region Properies

        protected EnemyAI EnemyAi => _enemyAI;

        public EnemyStateMachine StateMachine => _enemyStateMachine;
        
        #endregion
        

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
