using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannelSO", menuName = "Scriptable Objects/Events/IntEventChannelSO")]
public class IntEventChannelSO : ScriptableObject
{
    // 구독할 함수
    public UnityAction<int> OnEventRaised;

    // 호출 메서드
    public void RaiseEvent(int value) => OnEventRaised?.Invoke(value);    
}
