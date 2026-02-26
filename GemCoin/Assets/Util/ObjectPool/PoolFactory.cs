using UnityEngine;
using UnityEngine.Pool;

public static class PoolFactory
{
    public static IObjectPool<T> CreatePool<T>(T prefab, Transform container, int defaultSize = 5, int maxSize = 20) where T : Component
    {
        IObjectPool<T> pool = null; // 풀 참조를 담을 변수 선언
        
        pool = new ObjectPool<T>(
            createFunc: () => {
                T instance = Object.Instantiate(prefab, container);
                if (instance is IPoolable<T> poolable) {
                    poolable.OriginPool = pool;
                }
                return instance;
            },
            actionOnGet: (obj) => {
                obj.gameObject.SetActive(true);
                if (obj is IPoolable<T> p) p.OnSpawn();
            },
            actionOnRelease: (obj) => {
                if (obj is IPoolable<T> p) p.OnDespawn();
                obj.gameObject.SetActive(false);
            },
            actionOnDestroy: (obj) => Object.Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: defaultSize,
            maxSize: maxSize
        );
        return pool;
    }
}