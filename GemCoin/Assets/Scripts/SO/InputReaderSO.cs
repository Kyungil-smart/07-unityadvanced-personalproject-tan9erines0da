using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Scriptable Objects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, PlayerInputActions.IHubActions, PlayerInputActions.IRunningActions
{
    // 인풋 SO 에셋을 참조 중인 컴포넌트에서 구독 할 이벤트 목록
    
    // Hub 관련 이벤트
    public event UnityAction<bool> LeftClickEvent = delegate { };
    public event UnityAction CancelEvent = delegate { };
    public event UnityAction<Vector2> ScrollWheelEvent = delegate { };
    
    // Running 관련 이벤트
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction<bool> Skill1Event = delegate { };
    public event UnityAction<bool> Skill2Event = delegate { };
    public event UnityAction<bool> Skill3Event = delegate { };

    
    private PlayerInputActions _inputActions;
    
    public Vector2 PointerPosition { get; private set; }

    private void OnEnable()
    {
        Debug.Log("InputReaderSO : OnEnable");
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Running.SetCallbacks(this);
            _inputActions.Hub.SetCallbacks(this);
        }
        _inputActions.Running.Enable();
        _inputActions.Hub.Enable();
        
    }
    private void OnDisable()
    {
        Debug.Log("InputReaderSO : OnDisable 호출됨. 호출 스택: " + System.Environment.StackTrace);
        _inputActions.Running.Disable();
        _inputActions.Hub.Disable();
        
    }

    // 맵 활성화 제어 
    public void EnableRunningInput()
    {
        _inputActions.Hub.Disable();
        _inputActions.Running.Enable();
    }

    public void EnableHubInput()
    {
        //_inputActions.Running.Disable();
        //_inputActions.Hub.Enable();
    }
    //--------------Hub-----------------------------------------------
    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("OnClick:");
        if (context.performed) LeftClickEvent?.Invoke(true);
        else if (context.canceled) LeftClickEvent?.Invoke(false);
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        PointerPosition = context.ReadValue<Vector2>();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            ScrollWheelEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) CancelEvent?.Invoke();
    }
    //--------------Running-----------------------------------------------
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) JumpEvent?.Invoke();
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Skill1Event.Invoke(true); // 키 다운 (True)
        }
        else if (context.canceled)
        {
            Skill1Event.Invoke(false); // 키 업 (False)
        }
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Skill2Event.Invoke(true); // 키 다운 (True)
        }
        else if (context.canceled)
        {
            Skill2Event.Invoke(false); // 키 업 (False)
        }
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Skill3Event.Invoke(true); // 키 다운 (True)
        }
        else if (context.canceled)
        {
            Skill3Event.Invoke(false); // 키 업 (False)
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) PauseEvent?.Invoke();
    }
}