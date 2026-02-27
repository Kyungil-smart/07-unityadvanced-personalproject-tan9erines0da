using UnityEngine;

public class RunState : BasePlayerState
{
    public RunState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.RuntimeStat.LeftJumpCount = player.RuntimeStat.MaxJumpCount;
        Debug.Log("Run State: 달리기 시작");
        // 달리기 애니메이션 재생
    }

    public override void Update()
    {
        // 1. 지면 체크 (바닥이 사라지면 즉시 Airborne으로)
        if (!player.IsGrounded)
        {
            stateMachine.TransitionTo(player.AirborneState);
            return;
        }

        // 2. 가로 이동 로직 (PlayerController에 구현된 메서드 호출)
        player.HandleMovement();
    }

    public override void OnJump()
    {
        // 달리기 중 점프 입력 시 Airborne으로 전이
        if (player.RuntimeStat.LeftJumpCount > 0)
        {
            player.ExecuteJump();
            player.RuntimeStat.LeftJumpCount--;
            stateMachine.TransitionTo(player.AirborneState);
        }
    }

    public override void OnSlide(bool isPressed)
    {
        // 슬라이드 버튼이 눌리면 Slide 상태로 전이
        if (isPressed)
        {
            stateMachine.TransitionTo(player.SlideState);
        }
    }
}
