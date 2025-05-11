using UnityEngine;
using UnityEngine.AI;

public class Tile : MonoBehaviour, IMouseSelectable
{
    public bool zauzeto = false;
    public GameObject dropdown;
    public GameObject sator_prefab;
    public GameObject batinas_prefab;
    public GameObject parent_grid;

    [SerializeField]
    private Plata plata;

    public AudioSource soundSource;
    public float masterSoundVolume = 1;
    public AudioClip satorClip;
    [Range(0f, 1f)]
    public float volModifier;

    private PathFinding pathFinding;
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log($"bruh {GameObject.Find("Game Logic")}");
        plata = GameObject.Find("Game Logic").GetComponent<Plata>();
        pathFinding = GameObject.FindWithTag("Path Finding").GetComponent<PathFinding>();
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
                if (!plata.EnoughMoney())
                 return false;
                //placeSator();
                //lose money
                //+cacije
                Debug.Log("place sator");
                zauzeto = true;

                GameObject new_sator = Instantiate(sator_prefab, this.transform.position, Quaternion.identity);
                new_sator.GetComponent<Placement>().parentTile = this;
                PlaySound(volModifier, satorClip);
            
            }else if (@string == "Batinas")
            {
                if (!plata.EnoughMoney())
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

}
