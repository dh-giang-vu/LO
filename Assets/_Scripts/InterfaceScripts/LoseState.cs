using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // GameObject to activate when sanity is zero
    [SerializeField] private SanityManager sanityManager; // Reference to the SanityManager

    void Update()
    {
        // Get the current sanity amount from the SanityManager instance (value between 0.0 and 1.0)
        float sanityAmount = sanityManager.GetSanityAmount();

        // Check if sanity amount is less than or equal to zero
        if (sanityAmount <= 0)
        {
            // Activate the specified GameObject
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No GameObject assigned to activate.");
            }
        }
    }
}
