using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    [SerializeField] private float sanityCap = 100.0f;
    [SerializeField] private float deductionRate = 0.5f;
    private float sanity;
    private List<ISanityProvider> inRangeSanityProviders;

    void Start()
    {
        sanity = sanityCap;
        inRangeSanityProviders = new List<ISanityProvider>();
        InvokeRepeating(nameof(UpdateSanity), 0, (float)1.0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ISanityProvider>(out var sanityProvider))
        {
            inRangeSanityProviders.Add(sanityProvider);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ISanityProvider>(out var sanityProvider))
        {
            inRangeSanityProviders.Remove(sanityProvider);
        }
    }

    /*
     * Return a list of active SanityProvider in range with the player.
    */
    private List<ISanityProvider> GetActiveSanityProviders()
    {
        List<ISanityProvider> activeSanityProviders = new List<ISanityProvider>();
        foreach (ISanityProvider sanityProvider in inRangeSanityProviders)
        {
            if (sanityProvider.isActive())
            {
                activeSanityProviders.Add(sanityProvider);
            }
        }
        
        return activeSanityProviders;
    }

    /*
     * Get all active SanityProvider in range with player and update player's sanity.
     * If there are no active SanityProvider in range then deduct player's sanity.
    */
    private void UpdateSanity()
    {
        List<ISanityProvider> activeSanityProviders = GetActiveSanityProviders();

        if (activeSanityProviders.Count == 0)
        {
            sanity -= deductionRate;
        }
        else
        {
            foreach (ISanityProvider sanityProvider in activeSanityProviders)
            {
                float sanityEffect = sanityProvider.getSanityEffect();

                if (sanity + sanityEffect > sanityCap)
                {
                    sanity = sanityCap;
                }
                else
                {
                    sanity += sanityEffect;
                }
            }
        }

        Debug.Log("Player's sanity: " + sanity.ToString());

    }
}
