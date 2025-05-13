using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip ambience;

    public float masterSoundVolume = 1;
    [Range(0, 1f)]
    public float volModifier1 = 1f;


    private void Start()
    {
        startButton.onClick.AddListener(TaskOnClicked);
        PlaySound(volModifier1, ambience);
    }

    void TaskOnClicked()
    {
        SceneManager.LoadScene("Scenes/Cutscene");
    }

    private void PlaySound(float volModifier, AudioClip myClip)
    {
        soundSource.clip = myClip;
        soundSource.volume = masterSoundVolume * volModifier;
        soundSource.Play();
    }
}
