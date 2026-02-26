using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private Chunk[] _chunkPrefabs; // 무작위로 선택될 청크들
    [SerializeField] private int _initialChunkCount = 5; // 처음에 깔아둘 청크 개수
    [SerializeField] private Transform _playerTransform; // 플레이어 위치 추적용
    [SerializeField] private float _spawnDistance = 50f; // 플레이어로부터 얼마나 앞에서 생성할지

    private Vector3 _nextSpawnPoint = Vector3.zero;
    private Queue<Chunk> _activeChunks = new Queue<Chunk>();

    
    void Start()
    {
        // 초기 청크 생성 로직
        for (int i = 0; i < _initialChunkCount; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        // 플레이어 거리에 따른 생성/삭제 체크
        if (Vector3.Distance(_playerTransform.position, _nextSpawnPoint) < _spawnDistance)
        {
            SpawnChunk();
            RemoveOldChunk();
        }
    }

    private void SpawnChunk()
    {
        var prefab = _chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)];

        Chunk newChunk = PoolManager.Instance.Get(prefab);

        newChunk.transform.position = _nextSpawnPoint;
        _nextSpawnPoint = newChunk.ExitPosition;
        _activeChunks.Enqueue(newChunk);
    }

    private void RemoveOldChunk()
    {
        if (_activeChunks.Count > _initialChunkCount + 2)
        {
            Chunk oldChunk = _activeChunks.Dequeue();

            // 이식 포인트: Destroy 대신 풀로 반환
            oldChunk.OriginPool.Release(oldChunk);
        }
    }
}