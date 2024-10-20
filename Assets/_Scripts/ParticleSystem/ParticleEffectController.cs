using System.Collections.Generic;
using UnityEngine;

public class BuildingObjectToggle : MonoBehaviour
{
    [SerializeField] private SanityManager sanityManager; // Reference to the SanityManager
    [SerializeField] private GameObject objectToToggle;   // The GameObject to enable/disable
    AudioSource audioSource;
    [SerializeField] private AudioClip gainSanityAudio;   // The GameObject to enable/disable
    [SerializeField, Range(0, 1)] private float audioVolume = 1.0f;

    private bool inBuilding = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckBuildingStatus();

        // Enable or disable the GameObject based on the inBuilding status and sanity level
        float currentSanity = sanityManager.GetSanityAmount();
        objectToToggle.SetActive(inBuilding && currentSanity < 1.0f); // Disable if sanity is full
        
        if (inBuilding && currentSanity < 1.0f)
        {
            // If audio is not already playing, play the sanity gain sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(gainSanityAudio, audioVolume);
            }
        }
        else
        {
            // Stop the audio if the player is not in the building or sanity is full
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void CheckBuildingStatus()
    {
        inBuilding = false; // Reset the status

        // Get the active sanity providers to check the building status
        List<ISanityProvider> sanityProviders = sanityManager.GetActiveSanityProviders();
        foreach (ISanityProvider provider in sanityProviders)
        {
            if (provider is BuildingSanity)
            {
                inBuilding = true; // Player is inside a building
                break; // No need to check further, we found that the player is in a building
            }
        }
    }
}
