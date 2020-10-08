namespace LivingBeings.Enemies.AIStateMachine
{
    // Base class for all states in enemy state machine.
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
        
        
        // Contains actions to be performed when 
        // we just entered to the some state.
        public virtual void Enter()
        {

        }


        // Contains actions that must be repeated during
        // specific state is working.
        public virtual void PhysicsUpdate()
        {
            
        }


        // Contains actions to be performed when we go out
        // from the specific state.
        public virtual void Exit()
        {

        }
    }
}
