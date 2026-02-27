using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatSO statData;
    
    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 boxSize = new Vector2(0.5f, 0.1f); // 발바닥 너비와 두께
    [SerializeField] private float castDistance = 0.2f; // 감지 거리
    [SerializeField] private Vector2 castOffset = new Vector2(0, -0.5f); // 발밑 위치 보정
    
    [Header("States")]
    public PlayerStateMachine StateMachine { get; private set; }
    public EntryState EntryState { get; private set; }
    public RunState RunState { get; private set; }
    public AirborneState AirborneState { get; private set; }
    public SlideState SlideState { get; private set; }
    
    [Header("Hit Effects")]
    [SerializeField] private float invincibilityDuration = 1.5f;
    [SerializeField] private float flashInterval = 0.1f;
    
    private SpriteRenderer _spriteRenderer;
    public bool IsInvincible { get; private set; }
    

    public bool IsGrounded { get; private set; }
    
    public PlayerStat RuntimeStat { get; private set; }
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        if (statData != null)
        {
            RuntimeStat = new PlayerStat(statData);
        }
        
        StateMachine = new PlayerStateMachine();
        EntryState = new EntryState(this, StateMachine);
        RunState = new RunState(this, StateMachine);
        AirborneState = new AirborneState(this, StateMachine);
        SlideState = new SlideState(this, StateMachine);
        StateMachine.Initialize(EntryState);
        
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
            StateMachine.CurrentState?.OnJump();
        }
    }
    public void ExecuteJump()
    {
        // 속도를 초기화하고 점프력을 가해 일관된 점프 높이 유지
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0); 
        _rb.AddForce(Vector2.up * PlayerStatSO.JumpForce, ForceMode2D.Impulse);
    }

    private void HandleSlide(bool isPressed)
    {
        StateMachine.CurrentState?.OnSlide(isPressed);
    }

    public void HandleMovement()
    {
        // 기본 이동 로직
        _rb.linearVelocity = new Vector2(RuntimeStat.CurrentMoveSpeed, _rb.linearVelocity.y);
    }
    public void ApplyGravityScaling()
    {
        // 하강 시 중력 배율 적용 (낙하감을 더 묵직하게)
        if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = 1.5f;
        }
        else
        {
            _rb.gravityScale = 1f; // 상승 중이거나 평상시 기본 중력
        }
    }

    public void ResetGravityScale()
    {
        // 상태 탈출(Exit) 시 중력을 원래대로 복구하는 안전장치
        _rb.gravityScale = 1f;
    }
    public void StopMovement()
    {
        _rb.linearVelocity = Vector2.zero;
    }
    
    // 장애물과 충돌 시 호출될 메서드------------------------
    public void OnObstacleHit()
    {
        if (IsInvincible) return;

        // 1. 카메라 흔들림 (Cinemachine Impulse 등 활용)
        // CameraShake.Instance.Shake();

        // 2. 무적 및 점멸 효과 시작
        StartCoroutine(HitResponseRoutine());

        // 3. (필요 시) 점수 감점이나 HP 차감 로직만 수행
        // RuntimeStat.Health--;
    }

    private IEnumerator HitResponseRoutine()
    {
        IsInvincible = true;
        float timer = 0;

        // 무적 시간 동안 스프라이트 점멸
        while (timer < invincibilityDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        _spriteRenderer.enabled = true; // 마지막엔 반드시 켜주기
        IsInvincible = false;
    }
    //-----------------------------------------------
    
    // 지면 확인용 박스캐스트
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
        
        // 그라운드 체크
        CheckGroundStatus();
        StateMachine.FixedUpdate();
    }
    private void Update()
    {
        StateMachine.Update();
    }
}