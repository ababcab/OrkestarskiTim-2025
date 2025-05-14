using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BeginLoadingafterdelay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip video;

    // Update is called once per frame


    private bool videoEnded = false, videoStarted = false;
    private float videoEndedAt = -1f;
    void Update()
    { 
        if(videoPlayer.isPlaying)
        {
            videoStarted = true;
        }
        else if( videoStarted == true && videoEnded == false)
        {
            videoEndedAt = Time.time;
            videoEnded = true;
        }

        if (videoEnded && Time.time >= videoEndedAt + 2f) 
            SceneManager.LoadScene("Scenes/LevelLoader");
    }


}
