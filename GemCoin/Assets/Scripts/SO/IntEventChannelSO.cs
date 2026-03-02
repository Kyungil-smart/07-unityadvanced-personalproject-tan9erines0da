using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannelSO", menuName = "Scriptable Objects/Events/IntEventChannelSO")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value) => OnEventRaised?.Invoke(value);    
}
