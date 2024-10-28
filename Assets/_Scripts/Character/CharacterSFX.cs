using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip inGhostRangeAudio;
    [SerializeField] private AudioClip gainSanityAudio;
    [SerializeField, Range(0, 1)] private float sanityAudioVolume = 1.0f;
    [SerializeField, Range(0, 1)] private float ghostAudioVolume = 1.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = sanityAudioVolume;
        audioSource.loop = true;  // Enable looping for all audio clips
    }

    public void PlayGhostSound()
    {
        // Play the ghost sound - ghost sound override sanity gain sound
        if (!audioSource.isPlaying || audioSource.clip != inGhostRangeAudio)
        {
            audioSource.clip = inGhostRangeAudio;
            audioSource.volume = ghostAudioVolume;
            audioSource.Play();
        }
    }

    public void PlaySanityGain()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = gainSanityAudio;
            audioSource.volume = sanityAudioVolume;
            audioSource.Play();
        }
    }

    public void StopSanityGainAudio()
    {
        if (audioSource.isPlaying && audioSource.clip == gainSanityAudio)
        {
            ResetAudioSource();
        }
    }

    public void StopGhostAudio()
    {
        if (audioSource.isPlaying && audioSource.clip == inGhostRangeAudio)
        {
            ResetAudioSource();
        }
    }

    private void ResetAudioSource()
    {
        audioSource.Stop();
        audioSource.volume = sanityAudioVolume;
        audioSource.clip = null;
    }


}
