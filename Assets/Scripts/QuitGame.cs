using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    [SerializeField]
    private Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(TaskOnClicked);
    }

    void TaskOnClicked()
    {
        Application.Quit();
    }
}