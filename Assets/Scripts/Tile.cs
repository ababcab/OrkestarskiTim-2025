using UnityEngine;

public class Tile : MonoBehaviour, IMouseSelectable
{
    public bool zauzeto = false;
    public GameObject dropdown;
    public GameObject sator_prefab;
    public GameObject parent_grid;

    [SerializeField]
    private Plata plata;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log($"bruh {GameObject.Find("Game Logic")}");
        plata = GameObject.Find("Game Logic").GetComponent<Plata>();
    }


    public void IndirectMouseEnter()
    {
        _OnMouseEnter();
    }

    public void IndirectMouseExit()
    {
        _OnMouseExit(); 
    }

    public void IndirectMouseOver()
    {
        _OnMouseOver();
    }

    
    private void _OnMouseEnter()
    {
        //highlight
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void _OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {

            if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
            {
            if (!plata.EnoughMoney())
                return;
                //placeSator();
                //lose money
                //+cacije
                Debug.Log("place sator");
                zauzeto = true;

                GameObject new_sator = Instantiate(sator_prefab, this.transform.position, Quaternion.identity);
                new_sator.GetComponent<Placement>().parentTile = this;
            } 
        }
    }

    private void _OnMouseExit()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
}
