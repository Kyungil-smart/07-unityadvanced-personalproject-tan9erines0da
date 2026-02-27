using UnityEngine;

public class DieState : BasePlayerState
{
    public DieState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Die State: 캐릭터 사망");

        // 1. 모든 속도 정지
        player.StopMovement();
        
        // 2. 입력 이벤트 무시를 위해 별도 처리 (혹은 가만히 둠)
        // 3. 사망 애니메이션 재생
        // player.Animator.SetTrigger("Die");

        // 4. 게임 오버 UI 호출 (이벤트 채널이나 게임 매니저 활용)
        // GameEventManager.Instance.TriggerGameOver();
    }

    public override void Update()
    {
        // 사망 상태에서는 아무런 전이도, 이동도 하지 않음
    }

    // 모든 입력 메서드를 비워두어 조작 차단
    public override void OnJump() { }
    public override void OnSlide(bool isPressed) { }
}
