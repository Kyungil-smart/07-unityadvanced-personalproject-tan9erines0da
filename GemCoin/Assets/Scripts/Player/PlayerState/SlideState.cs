using UnityEngine;

public class SlideState : BasePlayerState
{
    public SlideState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Slide State: 슬라이딩 시작");
        // 캐릭터의 피격 판정을 낮추는 로직 호출
        // player.SetColliderHeight(0.5f); 
    }

    public override void Update()
    {
        // 1. 지면 체크: 슬라이딩 중 바닥이 끊기면 즉시 공중 상태로
        if (!player.IsGrounded)
        {
            stateMachine.TransitionTo(player.AirborneState);
            return;
        }

        // 2. 물리적 이동 유지 (슬라이딩 전용 속도 적용 등)
        player.HandleMovement();
    }

    public override void OnJump()
    {
        // 3. 슬라이딩 중 점프 입력 시: 즉시 슬라이드를 캔슬하고 튀어오름
        Debug.Log("Slide State: 슬라이딩 중 점프 캔슬");
        
        if (player.RuntimeStat.LeftJumpCount > 0)
        {
            player.ExecuteJump();
            player.RuntimeStat.LeftJumpCount--;
            stateMachine.TransitionTo(player.AirborneState);
        }
    }

    public override void OnSlide(bool isPressed)
    {
        // 4. 입력 이벤트에서 false(해제)를 받았을 때: Run 상태로 복귀
        if (!isPressed)
        {
            stateMachine.TransitionTo(player.RunState);
        }
    }

    public override void Exit()
    {
        Debug.Log("Slide State: 슬라이딩 종료");
        // 축소했던 콜라이더를 다시 원래대로 복구
        // player.SetColliderHeight(1.0f);
    }
}
