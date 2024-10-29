using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] ProgressTracker progressTracker;

    void Update()
    {
        if (progressTracker.GetProgress() >= 0.15)
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }       
    }
}
