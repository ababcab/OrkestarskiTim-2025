using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public SceneManager SceneManager;
    // Funkcionalno ali nema dobar lighting
    void Start()
    {
        SceneManager.LoadScene("Scenes/Katalog Tutorijal", LoadSceneMode.Additive); // Main
        SceneManager.LoadScene("Scenes/M1", LoadSceneMode.Additive); // Main
        SceneManager.LoadScene("Scenes/SS1", LoadSceneMode.Additive); // SampleScene
        SceneManager.LoadScene("Scenes/OBS1", LoadSceneMode.Additive); // ObservationRoom
        SceneManager.UnloadSceneAsync("Scenes/LevelLoader",UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
    private bool m1SetActive = false;
    private void Update()
    {
        if(m1SetActive == false)
        try
        {

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/M1"));
            m1SetActive = true;
            gameObject.SetActive(false);
        }
        catch
        {

        }
    }

}
