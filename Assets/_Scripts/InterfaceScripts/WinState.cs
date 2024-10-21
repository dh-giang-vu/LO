using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // GameObject to activate when sanity is zero
    [SerializeField] private ProgressTracker progressTracker; // Reference to the SanityManager

    void Update()
    {
        // Get the current sanity amount from the SanityManager instance (value between 0.0 and 1.0)
        float progressAmount = progressTracker.GetProgress();

        // Check if sanity amount is less than or equal to zero
        if (progressAmount >= 0.90)
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

