using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;

    public string selectedOption;

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        OnDropdownValueChanged(dropdown.value); // Initialize with current value
    }

    void OnDropdownValueChanged(int index)
    {
        selectedOption = dropdown.options[index].text;
        Debug.Log("Dropdown changed to: " + selectedOption);
    }
}
