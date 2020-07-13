namespace EntitiesWithHealth.Enemies.AIStateMachine
{
    public class EnemyStateMachine
    {
        public EnemyBaseState CurrentState{ get; private set; }

        public void Initialization(EnemyBaseState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }


        public void TransitionToState(EnemyBaseState state)
        {
            CurrentState.Exit();
            CurrentState = state;
            state.Enter();
        }
    }
}
