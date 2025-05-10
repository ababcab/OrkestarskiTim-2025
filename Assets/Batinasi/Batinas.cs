using UnityEngine;

public class Batinas : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField]
    private BatinasScrObj SO;

    private void OnTriggerEnter(Collider other)
    {
        Caci caci = other.GetComponent<Caci>();
        if(caci != null)
            caci.bonusLoyalty += SO.boostToLoyalty;
    }

    private void OnTriggerExit(Collider other)
    {
        Caci caci = other.GetComponent<Caci>();
        if (caci != null)
            caci.bonusLoyalty -= SO.boostToLoyalty;
    }
}
