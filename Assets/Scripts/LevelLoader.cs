using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public SceneManager SceneManager;

    void Start()
    {
        SceneManager.LoadScene("Scenes/M1", LoadSceneMode.Additive); // Main
        SceneManager.LoadScene("Scenes/SS1", LoadSceneMode.Additive); // SampleScene
        SceneManager.LoadScene("Scenes/OBS1", LoadSceneMode.Additive); // ObservationRoom
    }
}
