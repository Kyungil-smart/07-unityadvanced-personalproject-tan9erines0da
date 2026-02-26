using UnityEngine;
using UnityEngine.Pool;

public class Chunk : MonoBehaviour, IPoolable<Chunk>
{
    // 끝 점 표시 (다음 청크 시작점)
    [SerializeField] private Transform _exitAnchor;
    public Vector3 ExitPosition => _exitAnchor.position;

    public IObjectPool<Chunk> OriginPool { get; set; }

    public void OnSpawn() { /* 장애물 활성화 등 */ }
    public void OnDespawn() { /* 데이터 초기화 */ }
}