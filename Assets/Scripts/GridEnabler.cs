using UnityEngine;
using UnityEngine.UI;

public class GridEnabler : MonoBehaviour
{
    public GameObject dropdown;
    public GameObject smallGrid;
    public GameObject bigGrid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            Debug.Log("debug: " + dropdown.GetComponent<GetValueFromDropdown>().selectedOption);
            smallGrid.SetActive(true);
            bigGrid.SetActive(false);
        }
        else if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Rostilj" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Zurka" ||
            dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Kiflice")
        {
            bigGrid.SetActive(true);
           // smallGrid.SetActive(false);
        }
    }
}
