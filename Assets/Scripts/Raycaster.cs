using UnityEngine;

public class Raycaster : MonoBehaviour
{
    int layerMask;

    private void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Batinas") | 1 << LayerMask.NameToLayer("Caci"));
    }


    Ray ray;
    RaycastHit hit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,100f,layerMask ))
        {
            if(hit.collider.gameObject.tag == "Tile")
            {
                //print(hit.collider.name);
            }
        }
    }
}
