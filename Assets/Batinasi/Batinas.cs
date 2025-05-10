using UnityEngine;

public class Batinas : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField]
    private BatinasScrObj SO;

    private int layerMask;
    private void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Batinas") | 1 << LayerMask.NameToLayer("Caci"));
    }



    private void OnTriggerEnter(Collider other)
    {
        Caci caci = other.GetComponent<Caci>();
        if(caci != null)
            caci.bonusLoyalty += SO.boostToLoyalty;
    }

    private void OnTriggerExit(Collider other)
    {
        Caci caci = other.GetComponent<Caci>();
        if (caci != null)
            caci.bonusLoyalty -= SO.boostToLoyalty;
    }
    /*
    private void OnMouseEnter()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            Debug.Log($"Hit {tile.gameObject.name}");
            if(tile != null)
            {
                //tile.IndirectMouseEnter();
            }
        }
    }
    private void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                Debug.Log($"Hit {tile.gameObject.name}");
                //tile.IndirectMouseOver();
            }
        }
    }
    private void OnMouseExit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                Debug.Log($"Hit {tile.gameObject.name}");
                //tile.IndirectMouseExit();
            }
        }
    }

    */
}
