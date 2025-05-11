using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [Header("Caci params")]
    [SerializeField]
    private Transform boundBotLeft;
    [SerializeField]
    private Transform boundBotRight;
    [SerializeField]
    private Transform boundTopLeft;
    [SerializeField]
    private Transform boundTopRight;
    public Vector3 GetRandomDestination()
    {
        float randomLeftRight = Random.Range(0, 1f);
        float randomBotTop= Random.Range(0, 1f);
        Vector3 dest = boundBotLeft.position
            + (boundTopRight.position- boundTopLeft.position)* randomBotTop
            + (boundTopLeft.position - boundBotLeft.position) * randomLeftRight;



        return dest;
    }

}
