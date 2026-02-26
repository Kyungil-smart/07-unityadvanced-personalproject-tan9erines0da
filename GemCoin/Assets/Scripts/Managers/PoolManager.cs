using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    // 프리팹 인스턴스 ID를 키로 사용하여 풀 보관
    private Dictionary<int, object> _pools = new();

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IObjectPool<T> GetPool<T>(T prefab) where T : Component
    {
        int id = prefab.gameObject.GetInstanceID();

        if (!_pools.TryGetValue(id, out var pool))
        {
            // 해당 프리팹을 위한 풀이 없으면 팩토리로 생성
            var newPool = PoolFactory.CreatePool(prefab, transform);
            _pools.Add(id, newPool);
            pool = newPool;
        }

        return (IObjectPool<T>)pool;
    }

    public T Get<T>(T prefab) where T : Component
    {
        // 프리팹을 키로 풀 탐색
        var pool = GetPool(prefab);
        // 풀에서 겟(UnityEngine.Pool으로 구현)
        T obj = pool.Get();
        
        // 인터페이스 주입 (생성 시점에 못했을 경우를 대비한 확정 주입)
        if (obj is IPoolable<T> p) p.OriginPool = pool;
        
        return obj;
    }
}