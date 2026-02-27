public class PlayerStateMachine
{
    public BasePlayerState CurrentState { get; private set; }

    public void Initialize(BasePlayerState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void TransitionTo(BasePlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}