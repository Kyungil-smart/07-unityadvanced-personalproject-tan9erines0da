using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Chunk : MonoBehaviour, IPoolable<Chunk>
{
    // 끝 점 표시 (다음 청크 시작점)
    [SerializeField] private Transform _exitAnchor;
    public Vector3 ExitPosition => _exitAnchor.position;

    public IObjectPool<Chunk> OriginPool { get; set; } // 풀 반환을 위한 키
    
    // 오브젝트 생성을 위한 필드
    [SerializeField] TreasureSpawnAnchor[] _treasureSpawnAnchors; // 인스펙터에서 할당
    [SerializeField] ObstacleSpawnAnchor[] obstacleSpawnPoints;
    

    public void OnSpawn() 
    {
        
    }

    public void ObjectPositionSet()
    {
        foreach (var spawnPoint in _treasureSpawnAnchors)
        {
            var obj = PoolManager.Instance.Get(spawnPoint.prefab);
            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = spawnPoint.transform.rotation;
        }

        foreach (var spawnPoint in obstacleSpawnPoints)
        {
            var obj = PoolManager.Instance.Get(spawnPoint.prefab);
            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = spawnPoint.transform.rotation;
        }
    }

    public void OnDespawn() // 청크 반환 시 생성 오브젝트도 같이 반환
    {
        
    }
}