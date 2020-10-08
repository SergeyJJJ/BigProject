namespace LivingBeings.Player.CharacterMovement.MovementStateMachine
{
    public class StateMachine
    {
        public BaseState CurrentState{ get; private set; }

        public void Initialization(BaseState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }


        public void TransitionToState(BaseState state)
        {
            CurrentState.Exit();
            CurrentState = state;
            state.Enter();
        }
    }
}
