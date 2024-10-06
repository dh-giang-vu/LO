using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{

    [SerializeField] private GameObject objectToActivate;
    void Update()
    {
        // Get the current sanity amount from the SanityManager singleton (value between 0.0 and 1.0)
        float sanityAmount = SanityManager.Instance.GetSanityAmount();

        if (sanityAmount <= 0)
        {
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
