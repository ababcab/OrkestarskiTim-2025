using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BeginLoadingafterdelay : MonoBehaviour
{
    public VideoClip video;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= video.length + 2f)
            SceneManager.LoadScene("Scenes/LevelLoader");
    }
}
