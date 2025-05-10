using System;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool zauzeto = false;
    public GameObject sator_prefab;

    [SerializeField]
    private Plata plata;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log($"bruh {GameObject.Find("Game Logic")}");
        plata = GameObject.Find("Game Logic").GetComponent<Plata>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        //highlight
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            if (!plata.EnoughMoney())
                return;
            //placeSator();
            //lose money
            //+cacije
            Debug.Log("place sator");
            zauzeto = true;

            Instantiate(sator_prefab, this.transform.position, Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
