using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
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
    public float boostToLoyalty;

    #region Boxcast Params

    private Vector3 halfExtents_rostilj = Vector3.one*4;
    private int layerMask_boxCast;
    List<Tile> hitTiles;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitTiles = new List<Tile>();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        layerMask_boxCast = 1 << LayerMask.NameToLayer("Tile");
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
            selected == "Kiflice" ||
            selected == "Himna" ||
            selected == "Fejk indeksi")

        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Rostilj" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Zurka" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Kiflice" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Himna" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Fejk indeksi")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }


    private void _OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;

            if (selected == "Rostilj" && CastBox(4))
            {
                throw new System.Exception("Nisi implementovao BoxCast all");
                Instantiate(rostilj_prefab, this.transform.position, Quaternion.identity);
            }
            else if (selected == "Zurka")
            {
                throw new System.Exception("Nisi implementovao BoxCast all");
                Instantiate(zurka_prefab, this.transform.position, Quaternion.identity);
            }
            else if (selected == "Kiflice")
            {
                throw new System.Exception("Nisi implementovao BoxCast all");
                Instantiate(bakine_kiflice_prefab, this.transform.position, Quaternion.identity);
            }
            else if (selected == "Himna")
            {   
                throw new System.Exception("Nisi implementovao BoxCast all");
                Instantiate(himna_prefab, this.transform.position, Quaternion.identity);
            }
            else if (selected == "Fejk indeksi")
            {   
                throw new System.Exception("Nisi implementovao BoxCast all");
                Instantiate(fejkIndeksi_prefab, this.transform.position, Quaternion.identity);
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
            selected == "Kiflice" ||
            selected == "Himna" ||
            selected == "Fejk indeksi")
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Rostilj" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Zurka" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Kiflice" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Himna" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Fejk indeksi")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    

    private bool CastBox(int needToHit)
    {
        bool canBuild = true;
        RaycastHit[] array =
            Physics.BoxCastAll(transform.position, halfExtents_rostilj,
            Vector3.up,
            Quaternion.identity,
            0.1f,
            layerMask: layerMask_boxCast);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 20, Color.blue, Time.deltaTime * 160f);
        int count = array.Length;
        if (needToHit != count)
        {
            Debug.Log($"Didnt hit enought (neededto){needToHit} vs {count}");
            return false;
        }
        Debug.Log($"Hit {count}");

        for (int i=0;i<count;i++)
        {
            Tile tile = array[i].collider.gameObject.GetComponent<Tile>();
            Debug.DrawLine(array[i].collider.transform.position, array[i].collider.transform.position + Vector3.up * 20, Color.magenta, Time.deltaTime * 160f);

            Debug.Log($"{tile.gameObject.name} je slobodan {tile.zauzeto == false}");
            if (tile.zauzeto)
                canBuild = false;
            else
                hitTiles.Add(tile);
        }
        if(canBuild)
            for (int i = 0; i < count; i++)
            {
                hitTiles[i].zauzeto = true;
            }
        hitTiles.Clear();
        return canBuild;
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.ToString() == "Caci")
        {
            Caci caci = other.gameObject.GetComponent<Caci>();
            if (caci != null)
            {
                caci.bonusLoyalty += boostToLoyalty;
                Debug.Log("caci boost to loyalty: " + boostToLoyalty);
            }
        }
    }
}

