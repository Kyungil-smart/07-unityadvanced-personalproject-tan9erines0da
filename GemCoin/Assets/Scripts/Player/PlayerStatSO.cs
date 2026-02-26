using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "Scriptable Objects/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    [Header("Identity")]
    public PlayerCharType charType;
    public string charName;

    [Header("Health & Survival")]
    public int maxHp = 100;
    
    [Tooltip("캐릭터 고유의 기본 체력 감소율 (스테이지 계수와 합산됨)")]
    public float baseHpDrainRate = 1.0f; 

    [Header("Movement (Consistency)")]
    public float moveSpeed = 8f;
    
    [Tooltip("모든 캐릭터 공통 점프력 (레벨 디자인 기준점)")]
    public const float JumpForce = 12f;
    
    public int maxJumpCount = 2;

    [Header("Utility")]
    public float magneticRange = 3f;
    
    [Tooltip("추후 캐릭터별 전용 버프 풀 참조용")]
    public int characterUniqueId;
}
