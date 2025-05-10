using UnityEngine;

public class EventTile : MonoBehaviour,IMouseSelectable
{
    [Header("Prefabi")]
    public bool zauzeto = false;
    public GameObject dropdown;
    public GameObject rostilj_prefab;
    public GameObject zurka_prefab;
    public GameObject bakine_kiflice_prefab;
    public GameObject himna_prefab;
    public GameObject fejkIndeksi_prefab;
    public GameObject parent_grid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
        if (selected == "Rostilj" ||
            selected == "Zurka" ||
            selected == "Kiflice")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void _OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
            if (selected == "Rostilj")
            {
                Instantiate(rostilj_prefab, this.transform.position, Quaternion.identity);
            }
            else if (selected == "Zurka")
            {
                Instantiate(zurka_prefab, this.transform.position, Quaternion.identity);
            }
            else if (selected == "Kiflice")
            {
                Instantiate(bakine_kiflice_prefab, this.transform.position, Quaternion.identity);
            }

            //placeBigTile;
            //lose money
            //caciji podju do te aktivnosti
            //smanji se sansa da budu uplaseni
            Debug.Log("big tile placed");
            zauzeto = true;
        }
    }

    private void _OnMouseExit()
    {
        string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
        if (selected == "Rostilj" ||
            selected == "Zurka" ||
            selected == "Kiflice")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

