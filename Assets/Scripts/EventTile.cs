using UnityEngine;

public class EventTile : MonoBehaviour
{
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

    private void OnMouseEnter()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Rostilj" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Zurka" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Kiflice")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Rostilj")
            {
                Instantiate(rostilj_prefab, this.transform.position, Quaternion.identity);
            }
            else if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Zurka")
            {
                Instantiate(zurka_prefab, this.transform.position, Quaternion.identity);
            }
            else if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Kiflice")
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

    private void OnMouseExit()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Rostilj" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Zurka" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Kiflice")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

