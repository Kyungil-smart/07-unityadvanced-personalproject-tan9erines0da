using Unity.Behavior;
using UnityEngine;
public class MainHubUIManger : MonoBehaviour
{
    // [Header("Input")]
    // [SerializeField] private InputReaderSO _inputReader;
    
    [Header("References")]
    [SerializeField] private BehaviorGraphAgent _agent; 
    [SerializeField] private GameObject[] _layers;

    void Awake()
    {
        // Debug.Log("MainHubUIManger : Awake");
        // _inputReader.EnableHubInput();
    }

    // 버튼에서 호출할 함수
    public void OnClickChangeState(int newStateIndex)
    {
        Debug.Log("Click");
        if(_agent == null) return;
        _agent.SetVariableValue("TargetUI", (HubUIState)newStateIndex);
        //BT의 TargetUI 변경
    }

    // BT의 Action 노드에서 호출할 실제 로직
    public void SetLayerActive(HubUIState state)
    {
        for (int i = 0; i < _layers.Length; i++)
        {
            // 0=Title, 1=Character, 2=Way, 3=Event, 4=Shop
            // UI를 장면 단위로 구분하고 장면에 맞는 UI그룹만 활성
            // 목표하는 레이어 제외 비활성화, 목표레이어만 활성
            _layers[i].SetActive(i == (int)state);
        }
    }
}
