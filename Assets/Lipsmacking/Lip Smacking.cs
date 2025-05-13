using System.Collections.Generic;
using UnityEngine;

public class LipSmacking : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private AudioSource source;
    [Header("Samples")]
    [SerializeField]
    private List<AudioClip> samples;
    [SerializeField]
    private float minimumGapBetweenStart;
    [SerializeField]
    private float maximumGapBetweenStart;
    // Update is called once per frame

    private float nextPlayTime = 0f;
    void Update()
    {
        if(Time.time >= nextPlayTime)
        {
            source.PlayOneShot(samples[Random.Range(0,samples.Count)]);
            nextPlayTime = Time.time + Random.Range(minimumGapBetweenStart, maximumGapBetweenStart);
        }
    }
}
