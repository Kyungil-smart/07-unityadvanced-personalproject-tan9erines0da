using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Chunk[] _chunkPrefabs; // 사용할 청크 프리팹들
    [SerializeField] private Transform _playerTransform; // 플레이어 트랜스폼

    [Header("Settings")]
    [SerializeField] private int _initialChunks = 5;      // 처음에 미리 생성할 개수
    [SerializeField] private float _spawnThreshold = 30f; // 플레이어로부터 이 거리 안에 들어오면 생성

    private Vector3 _nextSpawnPoint = Vector3.zero;       // 다음 청크가 생성될 위치
    private Queue<Chunk> _activeChunks = new Queue<Chunk>();  // 활성화된 청크 관리 큐

    private void Start()
    {
        // 초기 맵 구성
        for (int i = 0; i < _initialChunks; i++)
        {
            SpawnChunk();
        }
    }

    private void Update()
    {
        // 플레이어 위치를 체크하여 실시간 생성/삭제
        CheckAndGenerate();
    }

    private void CheckAndGenerate()
    {
        // 플레이어와 다음 생성 지점 사이의 거리가 임계값보다 작아지면 생성
        if (Vector3.Distance(_playerTransform.position, _nextSpawnPoint) < _spawnThreshold)
        {
            SpawnChunk();
            RemoveOldChunk();
        }
    }

    private void SpawnChunk()
    {
        // 무작위 프리팹 선택 (추후 상태패턴이나 비헤비어트리를 활용하여 생성 규칙 변경 가능)
        Chunk prefab = _chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)];

        // PoolManager를 통해 오브젝트 활성
        Chunk newChunk = PoolManager.Instance.Get(prefab);

        // 위치 설정 및 앵커 기반 좌표 갱신
        newChunk.transform.position = _nextSpawnPoint;
        _nextSpawnPoint = newChunk.ExitPosition;

        _activeChunks.Enqueue(newChunk);
    }

    private void RemoveOldChunk()
    {
        // 화면 뒤로 많이 밀려난 청크 반환 (여유분 2~3개 유지)
        if (_activeChunks.Count > _initialChunks + 2)
        {
            Chunk oldChunk = _activeChunks.Dequeue();
            
            // [고향 풀로 반환]
            if (oldChunk.OriginPool != null)
            {
                oldChunk.OriginPool.Release(oldChunk);
            }
            else
            {
                // 혹시라도 풀이 없으면 파괴 (예외 처리)
                Destroy(oldChunk.gameObject);
            }
        }
    }
}