using UnityEngine;
using UnityEngine.AI;

public class Student : MonoBehaviour, IPoolableObject
{
    [SerializeField]
    public NavMeshAgent agent;
    [SerializeField]
    public Animator animator;

    private ObjectPool pool = null;

    public void ReturnToPool()
    {
        pool.ReturnObject(gameObject);
    }

    public void SetPool(ObjectPool parentPool)
    {
        pool = parentPool;
    }
}
