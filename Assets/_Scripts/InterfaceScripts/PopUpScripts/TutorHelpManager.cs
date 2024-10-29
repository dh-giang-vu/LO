using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorHelpManager : MonoBehaviour
{
    [SerializeField] GameObject electricObjectToEnable;
    [SerializeField] GameObject tutorObjectToEnable;

    void Start()
    {
        // Start the coroutine to enable the object after a delay
        
        StartCoroutine(EnableElectricHelpAfterDelay(300f));
    }

    

    private IEnumerator EnableElectricHelpAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);
        
        // Enable the object
        electricObjectToEnable.SetActive(true);

        StartCoroutine(EnableTutorHelpAfterDelay(180f));
    }

    private IEnumerator EnableTutorHelpAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);
        
        
        // Enable the object
        tutorObjectToEnable.SetActive(true);
    }
}
