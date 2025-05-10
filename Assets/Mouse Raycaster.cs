using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    private float distance = 400f;
    private Tile hitTile = null;

    private int layerMask;
    private void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Batinas") 
            | 1 << LayerMask.NameToLayer("Caci") 
            | 1 << LayerMask.NameToLayer("UI"));

    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        /*if (Physics.Raycast(ray, out RaycastHit hitShouldntHit, distance, ~layerMask))
        {
            Debug.Log($"hit something i shouldn't {hitShouldntHit.collider.gameObject.name}");
        }*/
        if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
        {
            //Debug.DrawLine(ray.origin, ray.origin + distance * ray.direction,Color.blue,Time.deltaTime*40f);
            //Debug.Log($"hit something {hit.collider.gameObject.name}");
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            if(tile == null)
            {
                if(hitTile != null)
                {
                    hitTile.IndirectMouseExit();
                    hitTile = null;
                }
                return;
            }

            if(tile != hitTile)
            {
                if(hitTile != null)
                {
                    hitTile.IndirectMouseExit();
                }
                hitTile = tile;
                hitTile.IndirectMouseEnter();
            }
            else
            {
                //Debug.Log($"_{hitTile}_ _{tile}_ {tile==null}");
                hitTile.IndirectMouseOver();
            }
        }
        else
        {
            //Debug.DrawLine(ray.origin, ray.origin + distance * ray.direction, Color.red, Time.deltaTime * 40f);
            //Debug.Log($"Didnt hit anything valid");
        }
    }
}
