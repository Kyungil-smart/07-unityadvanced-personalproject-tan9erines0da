using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatSO statData;
    
    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 boxSize = new Vector2(0.5f, 0.1f); // 발바닥 너비와 두께
    [SerializeField] private float castDistance = 0.2f; // 감지 거리
    [SerializeField] private Vector2 castOffset = new Vector2(0, -0.5f); // 발밑 위치 보정

    public bool IsGrounded { get; private set; }
    
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
    
    private void CheckGroundStatus()
    {
        Vector2 origin = (Vector2)transform.position + castOffset;
        RaycastHit2D hit = Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, castDistance, groundLayer);
        IsGrounded = hit.collider;
    }
    private void OnDrawGizmos()
    {
        // 기즈모의 색상을 상태에 따라 변경
        Gizmos.color = IsGrounded ? Color.green : Color.red;

        // 현재 위치를 기준으로 기즈모 위치 계산
        Vector3 origin = transform.position + (Vector3)castOffset;
        Vector3 direction = Vector3.down * castDistance;

        // 1. 박스의 시작 위치 (WireCube)
        Gizmos.DrawWireCube(origin, new Vector3(boxSize.x, boxSize.y, 0));

        // 2. 박스가 이동한 끝 위치 (WireCube)
        Vector3 endPosition = origin + direction;
        Gizmos.DrawWireCube(endPosition, new Vector3(boxSize.x, boxSize.y, 0));

        // 3. 시작과 끝을 잇는 선 (연결성 확인)
        Gizmos.DrawLine(origin + new Vector3(-boxSize.x / 2, 0), endPosition + new Vector3(-boxSize.x / 2, 0));
        Gizmos.DrawLine(origin + new Vector3(boxSize.x / 2, 0), endPosition + new Vector3(boxSize.x / 2, 0));
    }

    private void FixedUpdate()
    {
        // 기본 달리기 로직
        _rb.linearVelocity = new Vector2(RuntimeStat.CurrentMoveSpeed, _rb.linearVelocity.y);
        // 그라운드 체크
        CheckGroundStatus();
    }
}