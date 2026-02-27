public abstract class BasePlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;

    public BasePlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
    
    public virtual void OnJump() { }
    public virtual void OnSlide(bool isPressed) { }
}