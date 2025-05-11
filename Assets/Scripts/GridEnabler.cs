using UnityEngine;
using UnityEngine.UI;

public class GridEnabler : MonoBehaviour
{
    public GameObject dropdown;
    public GameObject smallGrid;
    public GameObject bigGrid;

    void Update()
    {
        if (dropdown.GetComponent<GetValueFromDropdown>().selectedOption == "Sator")
        {
            //Debug.Log("debug: " + dropdown.GetComponent<GetValueFromDropdown>().selectedOption);
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
