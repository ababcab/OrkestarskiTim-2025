using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Caci : MonoBehaviour, IPoolableObject
{
    [Header("Scriptable Object")]
    [SerializeField]
    private CaciScrObj SO;

    [Header("Caci params")]
    [SerializeField]
    private int scaredShitless = 0;
    [SerializeField]
    private float loyalty;
    [SerializeField]
    public float bonusLoyalty;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private float destinationOffset;
    [SerializeField]
    private Animator animator; 
    [Header("Caci Agent")]
    [SerializeField]
    private NavMeshAgent agent;

    [Header("Caci ")]
    [SerializeField]
    public ObjectPool pool;

    private PathFinding pathFinding;
    private void Start()
    {
        
    }

    public void SetSO(CaciScrObj c)
    {
        SO = c;
    }
    public void SetUp(Transform parent)
    {
        loyalty = SO.baseLoyalty;
        speed = SO.baseSpeed;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        pathFinding = GameObject.FindWithTag("Path Finding").GetComponent<PathFinding>();
        NewDestination();
    }

    /// <summary>
    /// Returns if Caci wants to defect
    /// </summary>
    /// <param name="studenti">Bolsheviks</param>
    /// <returns></returns>
    public bool Affect(float studenti)
    {
        if (scaredShitless > 0)
            return false;
        if (studenti < SO.baseLoyalty)
            return false;
        if( Random.Range(0, 100f) > loyalty + bonusLoyalty)
        {
            agent.SetDestination(pathFinding.GetEscapeRoute());
            scaredShitless++;
            return true;
        }
        return false;
    }

    public void NewDestination()
    {
        //destination = new Vector3(transform.position.x + Random.Range(-destinationOffset, destinationOffset), 0, transform.position.z + Random.Range(-4f, 4));
        destination = pathFinding.GetRandomDestination();
        Debug.DrawLine(destination, destination + Vector3.up * 7,Color.red, Time.deltaTime*2000);
        agent.destination = destination;
    }

    public bool GoToDestination(float deltaTime)
    {
        //Debug.Log($"{agent.remainingDistance} {agent.stoppingDistance}");

        if (scaredShitless > 0)
        {
            if (agent.remainingDistance <= 2)
            {
                ReturnToPool();
                return true;
            }
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            NewDestination();
        }
        return false;
    }
    public void ReturnToPool()
    {
        Debug.Log($"trying to return to pool {gameObject.name}");
        pool.ReturnObject(gameObject);
    }
}
