using UnityEngine;
using TMPro;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;

    public string selectedOption;

    public void GetDropdownValue()
    {
        int pickedEntryIndex = dropdown.value;
        selectedOption = dropdown.options[pickedEntryIndex].text;
        Debug.Log(selectedOption);
    }
}
