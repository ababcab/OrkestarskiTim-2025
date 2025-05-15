using TMPro;
using UnityEngine;

public class Uputstvo : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    public GameObject ui0;
    [SerializeField]
    public GameObject ui1;
    [SerializeField]
    public GameObject ui2;
    [SerializeField]
    private bool on = false;
    private Transform gameplayScreenParent;
    //private Vector3 gameplayPosition;
    private Transform uputstvoScreenParent;

    private void Start()
    {

        if(!on)
        {

            ui0 = GameObject.Find("Canvas (cross-scene svrhe)");
            ui1 = GameObject.Find("Dropdown").transform.parent.gameObject;
            // uputstvoScreenParent = GameObject.Find("Uputstvo Helper").transform;
            // gameplayPosition = transform.localPosition;
            ui2 = GameObject.Find("Uputstvo Helper").transform.GetChild(0).gameObject;


            Uputstvo uputstvo = ui2.GetComponentInChildren<Uputstvo>();
            uputstvo.ui0 = ui0;
            uputstvo.ui1 = ui1;
            uputstvo.ui2 = ui2;
        }
        else
        {

        }

    }

    public void UkljuciUputstvo()
    {
        if (!on)
        {
            ui2.SetActive(true);
            ui0.SetActive(false);
            ui1.SetActive(false);
            //transform.SetParent(uputstvoScreenParent,false);
           // transform.localPosition = gameplayPosition;
        }
        else
        {
            //transform.SetParent(gameplayScreenParent, false);
           // transform.localPosition = gameplayPosition;
            ui0.SetActive(true);
            ui1.SetActive(true);
            ui2.SetActive(false);
        }
    }


}
