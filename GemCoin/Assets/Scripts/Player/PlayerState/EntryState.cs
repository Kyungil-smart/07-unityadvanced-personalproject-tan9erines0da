using UnityEngine;

public class EntryState : BasePlayerState
{
    public EntryState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        // 대기 애니메이션 실행 등
        Debug.Log("Entry State: 준비 중...");
    }

    public override void Update()
    {
        // 게임 매니저의 'Start' 신호 대기 if(isStart)
        
        stateMachine.TransitionTo(player.RunState);
    }
}
