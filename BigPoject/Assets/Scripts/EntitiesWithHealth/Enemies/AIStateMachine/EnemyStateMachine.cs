namespace EntitiesWithHealth.Enemies.AIStateMachine
{
    // Class that controls enemy state machine.
    // Can be used for state machine initialization
    // and for transition between states.
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
