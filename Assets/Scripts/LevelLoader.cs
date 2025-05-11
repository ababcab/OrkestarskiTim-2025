using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public SceneManager SceneManager;

    void Start()
    {
        SceneManager.LoadScene("Scenes/Main", LoadSceneMode.Additive); // Main
        SceneManager.LoadScene("Scenes/SampleScene", LoadSceneMode.Additive); // SampleScene
        SceneManager.LoadScene("Scenes/ObservationRoom", LoadSceneMode.Additive); // ObservationRoom
    }
}
