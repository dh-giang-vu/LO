using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    // Singleton instance
    public static SanityManager Instance { get; private set; }

    [SerializeField, Range(0, 100)] private float deductionRate = 5.0f; // Deduction rate percentage per second (5 means 5% per second)
    private float sanityAmount; // Sanity value between 0.0 and 1.0
    private List<ISanityProvider> inRangeSanityProviders;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep this manager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        sanityAmount = 1.0f; // Start with full sanity (1.0)
        inRangeSanityProviders = new List<ISanityProvider>();
        InvokeRepeating(nameof(UpdateSanity), 0, 1.0f); // Call UpdateSanity every 1 second
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
    public List<ISanityProvider> GetActiveSanityProviders()
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

    private void UpdateSanity()
    {
        List<ISanityProvider> activeSanityProviders = GetActiveSanityProviders();

        if (activeSanityProviders.Count == 0)
        {
            // Deduct a flat percentage of the total sanity (e.g., 5% of 1.0 = 0.05 per second if deductionRate = 5)
            float sanityReduction = deductionRate / 10.0f; // Converts 5% to 0.05
            sanityAmount = Mathf.Clamp(sanityAmount - sanityReduction, 0.0f, 1.0f); // Ensure sanity doesn't go below 0
        }
        else
        {
            foreach (ISanityProvider sanityProvider in activeSanityProviders)
            {
                float sanityEffect = sanityProvider.getSanityEffect();
                // Clamp sanity amount between 0.0 and 1.0
                sanityAmount = Mathf.Clamp(sanityAmount + sanityEffect, 0.0f, 1.0f);
                
            }
        }

        Debug.Log("Player's sanity: " + sanityAmount.ToString("F3")); // Format to 3 decimal places for clarity
    }

    // Public property to get the current sanity amount
    public float GetSanityAmount()
    {
        return sanityAmount;
    }
}
