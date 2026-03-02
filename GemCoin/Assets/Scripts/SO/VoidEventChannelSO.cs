using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannelSO", menuName = "Scriptable Objects/Events/VoidEventChannelSO")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent() => OnEventRaised?.Invoke();
}
