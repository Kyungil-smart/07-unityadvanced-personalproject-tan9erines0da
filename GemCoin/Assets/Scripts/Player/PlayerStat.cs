using UnityEngine;

public class PlayerStat
{
    private readonly PlayerStatSO _baseStat;

    // 실시간으로 변하는 현재 수치들
    public int CurrentHp { get; private set; }
    public int LeftJumpCount { get; set; }
    public float CurrentMoveSpeed { get; private set; }
    
    // 외부에서 접근할 기본 정보들
    public PlayerCharType CharType => _baseStat.charType;
    public int MaxHp => _baseStat.maxHp;

    public PlayerStat(PlayerStatSO so)
    {
        _baseStat = so;
        ResetStat();
    }

    // 게임 시작 시 혹은 부활 시 스탯 초기화
    public void ResetStat()
    {
        CurrentHp = _baseStat.maxHp;
        CurrentMoveSpeed = _baseStat.moveSpeed;
        LeftJumpCount = _baseStat.maxJumpCount;
    }

    // 데미지 처리 (나중에 버프/방어력 로직 추가 가능)
    public void ChangeHp(int amount)
    {
        CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, MaxHp);
        
        if (CurrentHp <= 0)
        {
            // TODO: 사망 이벤트 호출
        }
    }
}