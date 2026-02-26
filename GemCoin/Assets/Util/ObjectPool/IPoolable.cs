using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable<T> where T : Component
{
    // 나를 관리하는 풀을 기억 (자신이 스스로 반환하기 위함)
    IObjectPool<T> OriginPool { get; set; }
    void OnSpawn();    // 꺼낼 때 호출
    void OnDespawn();  // 넣을 때 호출
}