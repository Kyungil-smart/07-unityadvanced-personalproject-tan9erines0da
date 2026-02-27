public interface IState
{
    void Enter();    // 상태 진입 시
    void Update();   // 업데이트 로직
    void FixedUpdate(); // 물리 연산
    void Exit();     // 상태 퇴장 시
}