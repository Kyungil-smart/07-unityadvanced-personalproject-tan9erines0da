using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonMono<InputManager>
{
    [Header("Input Events")]
    public Action OnJumpInput;
    public Action<bool> OnSkill1Input;
    public Action<bool> OnSkill2Input;
    public Action<bool> OnSkill3Input;

    private PlayerInput _playerInput;

    private void Awake()
    {
        SingletonInit();
        _playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        _playerInput.actions.Enable();

        // 점프 (단발성)
        _playerInput.actions["Jump"].performed += OnJump;

        // 스킬의 경우 캐릭별로 방식이 다르기 때문에 performed,canceled 모두 등록
        // 스킬 1 
        _playerInput.actions["Skill1"].performed += OnSkill1Performed;
        _playerInput.actions["Skill1"].canceled += OnSkill1Canceled;

        // 스킬 2
        _playerInput.actions["Skill2"].performed += OnSkill2Performed;
        _playerInput.actions["Skill2"].canceled += OnSkill2Canceled;

        // 스킬 3
        _playerInput.actions["Skill3"].performed += OnSkill3Performed;
        _playerInput.actions["Skill3"].canceled += OnSkill3Canceled;
    }

    private void OnDestroy()
    {
        if (_playerInput == null) return;
        
        _playerInput.actions["Jump"].performed -= OnJump;

        _playerInput.actions["Skill1"].performed -= OnSkill1Performed;
        _playerInput.actions["Skill1"].canceled -= OnSkill1Canceled;

        _playerInput.actions["Skill2"].performed -= OnSkill2Performed;
        _playerInput.actions["Skill2"].canceled -= OnSkill2Canceled;

        _playerInput.actions["Skill3"].performed -= OnSkill3Performed;
        _playerInput.actions["Skill3"].canceled -= OnSkill3Canceled;
    }

    // 인풋 시스템 입력을 이벤트에 연결
    private void OnJump(InputAction.CallbackContext ctx) => OnJumpInput?.Invoke();

    private void OnSkill1Performed(InputAction.CallbackContext ctx) => OnSkill1Input?.Invoke(true);
    private void OnSkill1Canceled(InputAction.CallbackContext ctx) => OnSkill1Input?.Invoke(false);

    private void OnSkill2Performed(InputAction.CallbackContext ctx) => OnSkill2Input?.Invoke(true);
    private void OnSkill2Canceled(InputAction.CallbackContext ctx) => OnSkill2Input?.Invoke(false);

    private void OnSkill3Performed(InputAction.CallbackContext ctx) => OnSkill3Input?.Invoke(true);
    private void OnSkill3Canceled(InputAction.CallbackContext ctx) => OnSkill3Input?.Invoke(false);
}