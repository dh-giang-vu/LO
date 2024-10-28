using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingObjectToggle : MonoBehaviour
{
    [SerializeField] private SanityManager sanityManager; // Reference to the SanityManager
    [SerializeField] private GameObject objectToToggle;   // The GameObject to enable/disable
    [SerializeField] UnityEvent onSanityGain;
    [SerializeField] UnityEvent onStopSanityGain;

    private bool inBuilding = false;

    void Update()
    {
        CheckBuildingStatus();

        // Enable or disable the GameObject based on the inBuilding status and sanity level
        float currentSanity = sanityManager.GetSanityAmount();
        objectToToggle.SetActive(inBuilding && currentSanity < 1.0f); // Disable if sanity is full
        
        if (inBuilding && currentSanity < 1.0f)
        {
            onSanityGain.Invoke();
        }
        else
        {
            onStopSanityGain.Invoke();
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
