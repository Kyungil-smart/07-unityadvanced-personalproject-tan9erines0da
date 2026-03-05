using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class TreasureController : MonoBehaviour, IPoolable<TreasureController>, IPickupable
{
    [SerializeField] TreasureProcessor treasure;
    [SerializeField] TreasureDataSO treasureData;
    SpriteRenderer _renderer;
    CircleCollider2D _collider;
    
    // 자성 이동 관련 필드
    private Transform _playerTarget;
    private bool _isMagnetized;
    private float _currentSpeed;
    private Coroutine _magnetRoutine;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();

        _renderer.sprite = treasureData.image;
        transform.localScale = treasureData.localScale;
        _collider.radius = treasureData.size;
        
        _isMagnetized = false;
    }

    public IObjectPool<TreasureController> OriginPool { get; set; }
    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        if (_magnetRoutine != null)
        {
            StopCoroutine(_magnetRoutine);
            _magnetRoutine = null;
        }
        _playerTarget = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Pickup();
        }
        else if (collision.CompareTag("MagnetField"))
        {
            _playerTarget = collision.transform;
            // 이미 돌고 있는 루틴이 있다면 중복 실행 방지
            if (_magnetRoutine == null)
            {
                _magnetRoutine = StartCoroutine(MagnetMoveRoutine());
            }
        }
    }

    // 획득 시 호출
    public void Pickup()
    {
        treasure.Pickup(treasureData);
        this.OriginPool.Release(this);
    }
    
    //자력에 반응
    private IEnumerator MagnetMoveRoutine()
    {
        float currentSpeed = treasureData.magnetSpeed;
    
        while (true) // 먹히거나 반납될 때까지 반복
        {
            if (!_playerTarget) break;

            Vector3 dir = (_playerTarget.position - transform.position).normalized;
            currentSpeed += treasureData.acceleration * Time.deltaTime;
            transform.position += dir * (currentSpeed * Time.deltaTime);

            yield return null; // 다음 프레임까지 대기
        }
    }
}
