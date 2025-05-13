using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BeginLoadingafterdelay : MonoBehaviour
{
    public VideoClip video;

    private void Start()
    {
        SceneManager.LoadScene("Scenes/LevelLoader");
    }

    /*void Update()
    {
        if (Time.time >= video.length)
            SceneManager.LoadScene("Scenes/LevelLoader");
    }*/
}
