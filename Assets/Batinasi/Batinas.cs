using UnityEngine;
using UnityEngine.AI;

public class Batinas : MonoBehaviour, IMouseSelectable
{
    [Header("Scriptable Object")]
    [SerializeField]
    private BatinasScrObj SO;

    [Header("Batinas Refs")]
    [SerializeField]
    private SkinnedMeshRenderer strelica;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;


    private int layerMask;
    private void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Batinas") | 1 << LayerMask.NameToLayer("Caci"));
    }
    private float speedInWhichIdle = 0.25f;

    private void Update()
    {
        Debug.Log($"sqrVel{agent.velocity.sqrMagnitude}  speedIdleSqr{speedInWhichIdle * speedInWhichIdle} Walk {animator.GetBool("Walk")} Idle {animator.GetBool("Idle")}");
        if (agent.velocity.sqrMagnitude >= speedInWhichIdle * speedInWhichIdle && animator.GetBool("Walk") == false)
        {
            //Debug.Log($"sqrVel{agent.velocity.sqrMagnitude}  speedIdleSqr{speedInWhichIdle * speedInWhichIdle} Turning Walk on");

            animator.SetBool("Walk", true);
            animator.SetBool("Idle", false);
        }
        else if(agent.velocity.sqrMagnitude < speedInWhichIdle * speedInWhichIdle && animator.GetBool("Idle") == false)
        {
            //Debug.Log($"sqrVel{agent.velocity.sqrMagnitude}  speedIdleSqr{speedInWhichIdle * speedInWhichIdle} Turning Idle on");
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Caci caci = other.GetComponent<Caci>();
        if (caci != null)
            caci.bonusLoyalty += SO.boostToLoyalty;
    }

    private void OnTriggerExit(Collider other)
    {
        Caci caci = other.GetComponent<Caci>();
        if (caci != null)
            caci.bonusLoyalty -= SO.boostToLoyalty;
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
        Debug.Log("Looked at batinas");
    }

    private bool _OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked on batinas");
            strelica.enabled = true;
            return true;
        }
        return false;
    }

    private void _OnMouseExit()
    {
        Debug.Log("Stopped locking at batinas");
    }

    public void IndirectMouseClickedWhileSelected(IMouseSelectable info)
    {
        //Tile gO = (Tile)info;
        Debug.Log($"{gameObject.name} was selected; Received info from {info}");
        GameObject gO = info.GetGameObject();
        agent.SetDestination(gO.transform.position);
        strelica.enabled = false;
    }
}
