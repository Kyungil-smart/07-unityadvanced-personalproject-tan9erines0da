using UnityEngine;
public class AirborneState : BasePlayerState
{
    public AirborneState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log($"공중 상태 진입 - 남은 점프 횟수: {player.RuntimeStat.LeftJumpCount}");
    }

    public override void Update()
    {
        // 착지 체크
        if (player.IsGrounded)
        {
            stateMachine.TransitionTo(player.RunState);
        }
    }

    public override void OnJump()
    {
        // Stat에 저장된 남은 점프 횟수 확인
        if (player.RuntimeStat.LeftJumpCount > 0)
        {
            Debug.Log($"공중 점프 실행! (남은 횟수: {player.RuntimeStat.LeftJumpCount - 1})");
            
            // 점프 물리 실행
            player.ExecuteJump();
            
            // 데이터 차감 (Stat 클래스 내부에 차감 로직이 있다면 호출)
            player.RuntimeStat.LeftJumpCount--; 
        }
        else
        {
            Debug.Log("점프 횟수 부족");
        }
    }
    
    public override void FixedUpdate()
    {
        player.HandleMovement(); // 공중 이동 유지

        // 하강 중일 때 중력을 더 강하게 적용하여 조작감 개선
        player.ApplyGravityScaling();
    }

    public override void Exit()
    {
        // 하강시 증가한 중력 배율 초기화
        player.ResetGravityScale();
    }
}
