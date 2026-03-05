using UnityEngine;

[CreateAssetMenu(fileName = "BaseBuffSO", menuName = "Scriptable Objects/BaseBuffSO")]
public abstract class BaseBuffSO : ScriptableObject
{
    public string buffName;
    public BuffGrade grade;
    [TextArea] public string description;
    
    [Header("Stacking")]
    public int currentStack = 0; // 세션 종료, 시작 시 초기화 필수

    // 버프 로직 실행 (개수/중첩을 고려하여 내부 로직 작성)
    public abstract void OnEquip(BuffController controller);  // 필요한 이벤트 채널 구독
    public abstract void OnUnequip(BuffController controller);// 필요한 이벤트 채널 해제
    
    // 이벤트가 터졌을 때 실행될 공통 메서드 (중첩 수치 반영)
    public abstract void ExecuteEffect(BuffController controller);
    
    // 초기화 시 호출
    public void Init()
    {
        currentStack = 0;
    }
}
