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
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void _OnMouseOver()
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

    private void _OnMouseExit()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    
}
