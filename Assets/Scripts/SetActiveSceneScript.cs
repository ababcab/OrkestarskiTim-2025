using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiveSceneScript : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/M1"));
    }
}
