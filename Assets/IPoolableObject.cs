using UnityEngine;

public interface IPoolableObject
{
    public void ReturnToPool();
    public void SetPool(ObjectPool parentPool);
}
