using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour, IMouseSelectable
{
    public bool zauzeto = false;
    public GameObject dropdown;
    [Header("PRefavs")]
    public List<GameObject> sator_prefabs;
    public GameObject batinas_prefab;
    public GameObject parent_grid;
    [Header("Costs")]
    public int sator_cost = 10;
    public int batinas_cost = 5;
    [SerializeField]
    private float eventDuration;
    [SerializeField]
    private Plata plata;

    private GameObject placedObject = null;

    public AudioSource soundSource;
    public float masterSoundVolume = 1;
    public AudioClip satorClip;
    [Range(0f, 1f)]
    public float volModifier;

    private PathFinding pathFinding;
    private GameLogic gameLogic;
    private Vector3 halfExtents_sator = Vector3.one;

    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log($"bruh {GameObject.Find("Game Logic")}");
        plata = GameObject.Find("Game Logic").GetComponent<Plata>();
        pathFinding = GameObject.FindWithTag("Path Finding").GetComponent<PathFinding>();
        gameLogic = GameObject.FindWithTag("Game Logic").GetComponent<GameLogic>();
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


    public GameObject GetGameObject()
    {
        return gameObject;
    }



    private void _OnMouseEnter()
    {
        //highlight
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private bool _OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && zauzeto == false)
        {
            string @string = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
            if (@string == "Sator")
            {
                if (!plata.EnoughMoney(sator_cost))
                 return false;
                //placeSator();
                //lose money
                //+cacije
                Debug.Log("place sator");
                zauzeto = true;


                placedObject = Instantiate(sator_prefabs[Random.Range(0, sator_prefabs.Count)], this.transform.position, Quaternion.identity);
                gameLogic.ChangeSator(+1);
                
                //placedObject.GetComponent<Placement>().parentTile = this;
                StartCoroutine(DeleteAfter(eventDuration, halfExtents_sator, 1));
                PlaySound(volModifier, satorClip);
            
            }else if (@string == "Batinas")
            {
                if (!plata.EnoughMoney(batinas_cost))
                    return false;
                GameObject new_batinas = Instantiate(batinas_prefab, pathFinding.GetEscapeRoute(), Quaternion.identity);
                new_batinas.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
            //return true;
        }
        return false;
    }

    private void _OnMouseExit()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void PlaySound(float volModifier, AudioClip myClip)
    {
        soundSource.clip = myClip;
        soundSource.volume = masterSoundVolume * volModifier;
        soundSource.Play();
    }
    public void IndirectMouseClickedWhileSelected(IMouseSelectable returnInfo)
    {
        Debug.Log($"{gameObject} got info from {returnInfo}");
    }

    IEnumerator DeleteAfter(float time, Vector3 halfExtents, int needToHit)
    {
        yield return new WaitForSeconds(time);
        placedObject.SetActive(false);

        gameLogic.ChangeSator(-1);
        Debug.Log($"I disabled {placedObject.name}");
        this.zauzeto = false;
    }
}
