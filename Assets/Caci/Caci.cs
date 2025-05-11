using UnityEngine;
using UnityEngine.AI;

public class Caci : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField]
    private CaciScrObj SO;

    [Header("Caci params")]
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

    [Header("Caci Agent")]
    [SerializeField]
    private NavMeshAgent agent;
    
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
        NewDestination();
    }

    /// <summary>
    /// Returns if Caci wants to defect
    /// </summary>
    /// <param name="studenti">Bolsheviks</param>
    /// <returns></returns>
    public bool Affect(float studenti)
    {
        if (studenti < SO.baseLoyalty)
            return false;
        return Random.Range(0, 1f)*100f > loyalty + bonusLoyalty;
    }

    public void NewDestination()
    {
        //destination = new Vector3(transform.position.x + Random.Range(-destinationOffset, destinationOffset), 0, transform.position.z + Random.Range(-4f, 4));
        destination = GameObject.FindWithTag("Path Finding").GetComponent<PathFinding>().GetRandomDestination();
        Debug.DrawLine(destination, destination + Vector3.up * 7,Color.red, Time.deltaTime*2000);
        agent.destination = destination;
    }

    public void GoToDestination(float deltaTime)
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            NewDestination();
        }
    }
}
