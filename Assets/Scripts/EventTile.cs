using UnityEngine;

public class EventTile : MonoBehaviour
{
    public bool zauzeto = false;
    public GameObject rostilj_prefab;
    public GameObject zurka_prefab;
    public GameObject kiflice_stand_prefab;
    public GameObject himna_prefab;
    public GameObject fejkIndeksi_prefab;

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
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {

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

