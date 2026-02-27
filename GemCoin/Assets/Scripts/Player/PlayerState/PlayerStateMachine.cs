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
        if(CurrentState == newState) return;
        
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void Update()
    {
        CurrentState?.Update();
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}