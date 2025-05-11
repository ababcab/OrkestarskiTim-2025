using UnityEngine;

public class Soundscape : MonoBehaviour
{

    public AudioSource soundSource;
    public AudioClip ambience;
    public float masterSoundVolume = 1;
    [Range(0, 1f)]
    public float volModifier = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaySound(volModifier, ambience);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlaySound(float volModifier, AudioClip myClip)
    {
        soundSource.clip = myClip;
        soundSource.volume = masterSoundVolume * volModifier;
        soundSource.Play();
    }
}
