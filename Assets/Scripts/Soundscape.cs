using UnityEngine;

public class Soundscape : MonoBehaviour
{

    public AudioSource soundSource;
    public AudioClip ambience;
    public AudioClip music;
    public float masterSoundVolume = 1;
    [Range(0, 1f)]
    public float volModifier1 = 1f;
    [Range(0, 1f)]
    public float volModifier2 = 0.5f;

    void Start()
    {
        PlaySound(volModifier1, ambience);
        PlaySound(volModifier2, music);
    }

    private void PlaySound(float volModifier, AudioClip myClip)
    {
        soundSource.clip = myClip;
        soundSource.volume = masterSoundVolume * volModifier;
        soundSource.PlayOneShot(myClip);
    }
}
