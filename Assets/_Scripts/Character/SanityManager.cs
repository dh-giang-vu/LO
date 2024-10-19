using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SanityManager : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float deductionRate = 5.0f; // Deduction rate percentage per second (5 means 5% per second)
    [SerializeField, Range(0, 1)] private float lowSanityThreshold = 0.5f;
    [SerializeField] private UnityEvent onLowSanity;
    
    private float sanityAmount; // Sanity value between 0.0 and 1.0
    private List<ISanityProvider> inRangeSanityProviders;

    void Start()
    {
        sanityAmount = 1.0f; // Start with full sanity (1.0)
        inRangeSanityProviders = new List<ISanityProvider>();
        InvokeRepeating(nameof(UpdateSanity), 0, 1.0f); // Call UpdateSanity every 1 second
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("found somthin" + other.name);
        if (other.TryGetComponent<ISanityProvider>(out var sanityProvider))
        {
            inRangeSanityProviders.Add(sanityProvider);
            Debug.Log("found Sanity Provider");
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
        bool inLight = false;
        List<ISanityProvider> activeSanityProviders = GetActiveSanityProviders();

        foreach (ISanityProvider sanityProvider in activeSanityProviders)
        {
            if (sanityProvider is LightSource) {
                inLight = true;
            }
            float sanityEffect = sanityProvider.getSanityEffect();
            // Clamp sanity amount between 0.0 and 1.0
            sanityAmount = Mathf.Clamp(sanityAmount + sanityEffect / 5.0f, 0.0f, 1.0f);
        }
        if (!inLight) {
            float sanityReduction = deductionRate / 10.0f; // Converts 5% to 0.05
            sanityAmount = Mathf.Clamp(sanityAmount - sanityReduction, 0.0f, 1.0f); // Ensure sanity doesn't go below 0
        }


        if (sanityAmount < lowSanityThreshold)
        {
            this.onLowSanity.Invoke();
        }
    }

    // Public property to get the current sanity amount
    public float GetSanityAmount()
    {
        return sanityAmount;
    }
}
