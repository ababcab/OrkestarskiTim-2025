using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UputstvoController : MonoBehaviour
{

    [Header("Paneli")]
    [SerializeField]
    private List<GameObject> panel1;
    [SerializeField]
    private List<GameObject> panel2;
    [SerializeField]
    private List<GameObject> panel3;
    [SerializeField]
    private List<GameObject> panel4;
    [SerializeField]
    private List<GameObject> panel5;

    private List<List<GameObject>> paneli;
    private const int count = 5;
    private void Start()
    {
        paneli = new List<List<GameObject>>();
        paneli.Add(panel1);
        paneli.Add(panel2);
        paneli.Add(panel3);
        paneli.Add(panel4);
        paneli.Add(panel5);

        foreach (GameObject gameObject in paneli[0])
        {
            gameObject.SetActive(true);
        }
    }


    private int index = 0;
    public void Sledeci()
    {
        int sledeci = (index + 1) % count;
        foreach(GameObject gameObject in paneli[index])
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in paneli[sledeci])
        {
            gameObject.SetActive(true);
        }
        index= sledeci;
    }

    public void Prethodni()
    {

        int prethodni = (index + count - 1) % count;
        foreach (GameObject gameObject in paneli[index])
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in paneli[prethodni])
        {
            gameObject.SetActive(true);
        }
        index = prethodni;
    }
}
