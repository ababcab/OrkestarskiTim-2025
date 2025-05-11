using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [Header("Bounds")]
    [SerializeField]
    private Transform boundBotLeft;
    [SerializeField]
    private Transform boundBotRight;
    [SerializeField]
    private Transform boundTopLeft;
    [SerializeField]
    private Transform boundTopRight;
    [SerializeField]
    private Transform escapeRoute;
    public Vector3 GetRandomDestination()
    {
        float randomLeftRight = Random.Range(0, 1f);
        float randomBotTop= Random.Range(0, 1f);
        Vector3 dest = boundBotLeft.position
            + (boundTopRight.position- boundTopLeft.position)* randomBotTop
            + (boundTopLeft.position - boundBotLeft.position) * randomLeftRight;



        return dest;
    }
    public Vector3 GetEscapeRoute()
    {
        return escapeRoute.position;
    }

}
