using UnityEngine;
using UnityEngine.Pool;

public class ObstacleController : MonoBehaviour, IPoolable<ObstacleController>, IDestructible
{
    
    public IObjectPool<ObstacleController> OriginPool { get; set; }
    public void OnSpawn()
    {
        throw new System.NotImplementedException();
    }

    public void OnDespawn()
    {
        throw new System.NotImplementedException();
    }

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }
}
