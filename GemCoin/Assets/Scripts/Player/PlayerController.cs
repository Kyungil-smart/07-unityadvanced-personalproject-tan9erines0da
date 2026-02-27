using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatSO statData;
    
    public PlayerStat RuntimeStat { get; private set; }
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        if (statData != null)
        {
            RuntimeStat = new PlayerStat(statData);
        }
    }

    private void OnEnable()
    {
        // 직접 할당 방식의 인풋 이벤트 구독
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpInput += HandleJump;
            InputManager.Instance.OnSkill1Input += HandleSlide;
        }
    }

    private void OnDisable()
    {
        // 메모리 누수 방지를 위한 해제 (중요!)
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpInput -= HandleJump;
            InputManager.Instance.OnSkill1Input -= HandleSlide;
        }
    }

    private void HandleJump()
    {
        if (RuntimeStat.LeftJumpCount > 0)
        {
            // 상수로 선언한 JumpForce 사용
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, PlayerStatSO.JumpForce);
            // RuntimeStat.LeftJumpCount--;
            Debug.Log($"점프! 남은 횟수: {RuntimeStat.LeftJumpCount}");
        }
    }

    private void HandleSlide(bool isPressed)
    {
        if (isPressed)
            Debug.Log("슬라이딩 시작");
        else
            Debug.Log("슬라이딩 종료");
    }

    private void FixedUpdate()
    {
        // 기본 달리기 로직
        _rb.linearVelocity = new Vector2(RuntimeStat.CurrentMoveSpeed, _rb.linearVelocity.y);
    }
}