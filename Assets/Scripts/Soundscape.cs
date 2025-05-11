using UnityEngine;

public class Soundscape : MonoBehaviour
{

    public AudioSource soundSource;
    public AudioClip ambience;
    public float masterSoundVolume = 1;
    [Range(0, 1f)]
    public float volModifier = 1f;

    void Start()
    {
        PlaySound(volModifier, ambience);
    }

    private void PlaySound(float volModifier, AudioClip myClip)
    {
        soundSource.clip = myClip;
        soundSource.volume = masterSoundVolume * volModifier;
        soundSource.Play();
    }
}
