using System;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool zauzeto = false;
    public GameObject dropdown;
    public GameObject sator_prefab;
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
        //highlight
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
            {
                //placeSator();
                //lose money
                //+cacije
                Debug.Log("place sator");
                zauzeto = true;

                Instantiate(sator_prefab, this.transform.position, Quaternion.identity);
            } 
        }
    }

    private void OnMouseExit()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
