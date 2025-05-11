using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    private float distance = 400f;
    private IMouseSelectable hitSelectable = null;

    private int layerMask;
    private int layerMask_select;
    private void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Batinas") 
            | 1 << LayerMask.NameToLayer("Caci") 
            | 1 << LayerMask.NameToLayer("UI"));
        layerMask_select = 1 << LayerMask.NameToLayer("Selectable")
            | 1 << LayerMask.NameToLayer("Caci")
            | 1 << LayerMask.NameToLayer("Tile");


    }
    IMouseSelectable selected = null;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (selected == null)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
            {
                IMouseSelectable selectable = hit.collider.gameObject.GetComponent<IMouseSelectable>();


                if (selectable == null)
                {
                    if (hitSelectable != null)
                    {
                        hitSelectable.IndirectMouseExit();
                        hitSelectable = null;
                    }
                    return;
                }


                if (selectable != hitSelectable)
                {
                    if (hitSelectable != null)
                    {
                        hitSelectable.IndirectMouseExit();
                    }
                    hitSelectable = selectable;
                    hitSelectable.IndirectMouseEnter();
                }
                else //selectable i hitselectable su isti object
                {
                    if (hitSelectable.IndirectMouseOver())
                    {
                        selected = hitSelectable;
                        Debug.Log($"{selected} is new selected");
                        hitSelectable.IndirectMouseExit();
                    }
                }
            }
            else //Nije pogodilo nista, dok nije nista selektovano
            {

            }
        }
        else // nesto jeste selektovano
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask_select) && Input.GetMouseButtonDown(0))
            {
                IMouseSelectable toSendInfoToSelected = hit.collider.gameObject.GetComponent<IMouseSelectable>();
                Debug.Log($"{hit.collider.gameObject} is gonna send info to {selected}");
                selected.IndirectMouseClickedWhileSelected(toSendInfoToSelected);
                selected = null;
            }
        }
        
    }
}
