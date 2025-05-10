using UnityEngine;

public class Caci : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField]
    private CaciScrObj SO;

    [Header("Caci params")]
    [SerializeField]
    private float loyalty;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private float destinationOffset;


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
        return Random.Range(0, 1f)*100f > loyalty;
    }

    public void NewDestination()
    {
        destination = new Vector3(transform.position.x + Random.Range(-destinationOffset, destinationOffset), 0, transform.position.z + Random.Range(-4f, 4));
    }

    public void GoToDestination(float deltaTime)
    {
        Vector3 direction = (destination - transform.position);
        float sqrDistance = direction.sqrMagnitude;
        if(sqrDistance < speed* speed * deltaTime * deltaTime)
        {
            transform.position = destination;
            NewDestination();
        }
        else
            transform.position += direction * speed;
    }

}
