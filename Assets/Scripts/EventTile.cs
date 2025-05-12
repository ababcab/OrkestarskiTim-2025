using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventTile : MonoBehaviour,IMouseSelectable
{
    [Header("Prefabi")]
    public bool zauzeto = false;
    public GameObject dropdown;
    public GameObject rostilj_prefab;
    public GameObject bakine_kiflice_prefab;
    public GameObject fejkIndeksi_prefab;
    public GameObject parent_grid;
    public float boostToLoyalty;
    [SerializeField]
    private Plata plata;
    [Header("Time")]
    [SerializeField]
    private float eventDuration;
    private GameObject placedObject = null;

    public AudioSource soundSource;
    public float masterSoundVolume = 100;
    public AudioClip placementClip;
    [Range(0f, 1f)]
    public float volModifier1;
    public AudioClip rostiljSizzle;
    [Range(0f, 1f)]
    public float volModifier2;

    #region Boxcast Params

    private Vector3 halfExtents_rostilj = Vector3.one*4;
    private int layerMask_boxCast;
    List<Tile> hitTiles;
    #endregion

    void Start()
    {
        hitTiles = new List<Tile>();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        layerMask_boxCast = 1 << LayerMask.NameToLayer("Tile");
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

    public bool IndirectMouseOver()
    {
        return _OnMouseOver();
    }


    public void IndirectMouseClickedWhileSelected(IMouseSelectable returnInfo)
    {
        Debug.Log($"{gameObject} got info from {returnInfo}");
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private void _OnMouseEnter()
    {
        string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
        if (selected == "Rostilj" ||
            selected == "Kiflice" ||
            selected == "Fejk indeksi")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    IEnumerator DeleteAfter(float time, Vector3 halfExtents, int needToHit)
    {
        yield return new WaitForSeconds(time);
        placedObject.SetActive(false);
        Debug.Log($"I disabled {placedObject.name}");
        CastBox_Release(halfExtents, needToHit);
        this.zauzeto = false;
    }

    private bool _OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;

            if (selected == "Rostilj" && CastBox_Occupy(halfExtents_rostilj,4))
            {
                if (!plata.EnoughMoney(25))
                    return false;
                placedObject =Instantiate(rostilj_prefab, this.transform.position, Quaternion.identity);
                PlaySound(volModifier1, placementClip, 0);
                PlaySound(volModifier2, rostiljSizzle, 3);
                Debug.Log($"I placed {placedObject.name}");
                StartCoroutine(DeleteAfter(eventDuration, halfExtents_rostilj, 4));
            }
            else if (selected == "Kiflice" && CastBox_Occupy(halfExtents_rostilj, 4))
            {
                if (!plata.EnoughMoney(25))
                    return false;
                placedObject =Instantiate(bakine_kiflice_prefab, this.transform.position, Quaternion.identity);
                PlaySound(volModifier1, placementClip, 0);
                StartCoroutine(DeleteAfter(eventDuration, halfExtents_rostilj, 4));
            }
            else if (selected == "Fejk indeksi" && CastBox_Occupy(halfExtents_rostilj, 4))
            {
                if (!plata.EnoughMoney(25))
                    return false;
                placedObject = Instantiate(fejkIndeksi_prefab, this.transform.position, Quaternion.identity);
                PlaySound(volModifier1, placementClip, 0);
                StartCoroutine(DeleteAfter(eventDuration, halfExtents_rostilj, 4));
            }

            //placeBigTile;
            //lose money
            //caciji podju do te aktivnosti
            //smanji se sansa da budu uplaseni
            Debug.Log("big tile placed");
            zauzeto = true;
        }
        return false;
    }

    private void _OnMouseExit()
    {
        string selected = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
        if (selected == "Rostilj" ||
            selected == "Zurka" ||
            selected == "Kiflice" ||
            selected == "Himna" ||
            selected == "Fejk indeksi")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    

    private bool CastBox_Occupy(Vector3 halfExtents,int needToHit)
    {
        bool canBuild = true;
        RaycastHit[] array =
            Physics.BoxCastAll(transform.position, halfExtents,
            Vector3.up,
            Quaternion.identity,
            0.1f,
            layerMask: layerMask_boxCast);

        Debug.DrawLine(transform.position, transform.position + Vector3.up * 20, Color.blue, Time.deltaTime * 160f);
        int count = array.Length;
        string @string;
        if (needToHit != count)
        {
            @string = $"Pogodjeni su:  Didnt hit enough (neededto) {needToHit} vs {count}\n";
            for (int i = 0; i < count; i++)
            {
                Tile tile = array[i].collider.gameObject.GetComponent<Tile>();
                Debug.DrawLine(array[i].collider.transform.position, array[i].collider.transform.position + Vector3.up * 20, Color.magenta, Time.deltaTime * 160f);

                @string+=$"{tile.gameObject.name}  {tile.zauzeto == false}\n";
            }
           Debug.Log(@string);
            return false;
        }

        @string = $"Pogodjeni su: Hit enough (neededto) {needToHit} vs {count}\n";
        for (int i=0;i<count;i++)
        {
            Tile tile = array[i].collider.gameObject.GetComponent<Tile>();
            Debug.DrawLine(array[i].collider.transform.position, array[i].collider.transform.position + Vector3.up * 20, Color.magenta, Time.deltaTime * 160f);

            @string += $"{tile.gameObject.name}  {tile.zauzeto == false}\n";
            if (tile.zauzeto)
                canBuild = false;
            else
                hitTiles.Add(tile);
        }
        Debug.Log(@string);
        if (canBuild)
            for (int i = 0; i < count; i++)
            {
                hitTiles[i].zauzeto = true;
            }
        hitTiles.Clear();
        return canBuild;
    }

    private void CastBox_Release(Vector3 halfExtents, int needToHit)
    {

        RaycastHit[] array =
            Physics.BoxCastAll(transform.position, halfExtents,
            Vector3.up,
            Quaternion.identity,
            0.1f,
            layerMask: layerMask_boxCast);


        Debug.DrawLine(transform.position, transform.position + Vector3.up * 20, Color.blue, Time.deltaTime * 160f);
        int count = array.Length;
        string @string="";
        if(count != needToHit)
        {
            @string = $"Pogodjeni su:  Didnt hit enough (neededto) {needToHit} vs {count} |||| Brisanje\n";
            for (int i = 0; i < count; i++)
            {
                Tile tile = array[i].collider.gameObject.GetComponent<Tile>();
                Debug.DrawLine(array[i].collider.transform.position, array[i].collider.transform.position + Vector3.up * 20, Color.magenta, Time.deltaTime * 160f);

                @string += $"{tile.gameObject.name}  {tile.zauzeto == false}\n";
            }
            Debug.Log(@string);
            throw new System.Exception();
        }
        for(int i=0;i<count;i++)
        {
            @string = $"Pogodjeni su:  Hit enough (neededto) {needToHit} vs {count} |||| Brisanje\n";
            Tile tile = array[i].collider.gameObject.GetComponent<Tile>();
            Debug.DrawLine(array[i].collider.transform.position, array[i].collider.transform.position + Vector3.up * 20, Color.magenta, Time.deltaTime * 160f);

            @string += $"{tile.gameObject.name} bio zauzet:{tile.zauzeto == false}\n";
            tile.zauzeto = false;
        }

        Debug.Log(@string);
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

    private void PlaySound(float volModifier, AudioClip myClip, int delayInSeconds)
    {

        StartCoroutine(PlaySoundAfterDelay(volModifier, myClip, delayInSeconds));
    }
    IEnumerator PlaySoundAfterDelay(float volModifier, AudioClip myClip, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        soundSource.clip = myClip;
        soundSource.volume = masterSoundVolume * volModifier;
        soundSource.PlayOneShot(myClip);
    }
}

