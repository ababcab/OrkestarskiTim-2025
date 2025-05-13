using UnityEngine;
using UnityEngine.UI;

public class GridEnabler : MonoBehaviour
{
    public GameObject dropdown;
    public GameObject smallGrid;
    public GameObject bigGrid;

    void Update()
    {
        string @string = dropdown.GetComponent<GetValueFromDropdown>().selectedOption;
        if (@string == "Sator")
        {
            //Debug.Log("debug: " + dropdown.GetComponent<GetValueFromDropdown>().selectedOption);
            smallGrid.SetActive(true);
            bigGrid.SetActive(false);
        }
        else if (@string == "Rostilj" ||
            @string == "Zurka" ||
            @string == "Kiflice" ||
            @string == "Himna" ||
            @string == "F. index")
        {
            bigGrid.SetActive(true);
            //smallGrid.SetActive(false);
        }
    }
}
