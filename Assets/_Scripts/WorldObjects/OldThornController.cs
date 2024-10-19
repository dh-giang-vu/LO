using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldThornController : MonoBehaviour
{

    private List<LightSource> inRangeLightSource = new List<LightSource>();

    // States: 
    //      0 - only base vine thorn
    //      1 - budding green thorns
    //      2 - flower bloom
    private int currentState = 0;
    [SerializeField] private float scaleAnimationTime = 2.0f;
    [SerializeField] private float scaleMultiplier = 1.0f;
    [SerializeField] private float checkLightInterval = 5.0f; // interval between checking for active light sources
    [SerializeField] private float destroyWaitTime = 5.0f;


    void Start()
    {
        // Deactivate flower groups
        DeactivateChildren(this.gameObject);

        // Deactivate each flower
        foreach (Transform flower_group in this.gameObject.transform)
        {
            foreach (Transform flower in flower_group.transform)
            {
                DeactivateChildObjectsByName(flower.gameObject, "pPipe28");
            }
        }

        // Check if is in range of any active light sources every `checkLightInterval`
        InvokeRepeating(nameof(CheckLightLevel), checkLightInterval, checkLightInterval);

    }

    // For deactivating flower groups
    private void DeactivateChildren(GameObject parentObject)
    {
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // For deactivating each flower
    private void DeactivateChildObjectsByName(GameObject parentObject, string childName)
    {
        foreach (Transform child in parentObject.transform)
        {
            // Check if the child's name matches the specified name
            if (child.name == childName)
            {
                // Disable the child object
                child.gameObject.SetActive(false);
            }
        }
    }

    // For activating flower groups (only green buds if flowe is disabled)
    private void ActivateChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            StartCoroutine(ScaleUp(child.gameObject, scaleAnimationTime, false));
        }
    }

    // For activating each flower
    private void ActivateChildObjectsByName(GameObject parentObject, string childName)
    {
        foreach (Transform child in parentObject.transform)
        {
            if (child.name == childName)
            {
                child.gameObject.SetActive(true);
                StartCoroutine(ScaleUp(child.gameObject, scaleAnimationTime, true));
            }
        }

    }

    // Scaling up animation
    private IEnumerator ScaleUp(GameObject child, float duration, bool useScale)
    {
        Vector3 originalScale = child.transform.localScale; // Store original scale
        child.transform.localScale = Vector3.zero; // Set to tiny (zero scale)

        float elapsedTime = 0f;
        
        // Use a user-set scale
        if (useScale)
        {
            originalScale *= scaleMultiplier;
        }

        while (elapsedTime < duration)
        {
            // Smoothly interpolate between zero scale and original scale over time
            child.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final scale is set exactly to the original scale
        child.transform.localScale = originalScale;
    }

    // Get active light sources from list of in range light sources
    private List<LightSource> GetActiveLightSources()
    {
        List<LightSource> activeLightSources = new();
        foreach (LightSource lightSource in inRangeLightSource)
        {
            if (lightSource.isActive())
            {
                activeLightSources.Add(lightSource);
            }
        }

        return activeLightSources;
    }

    // Check if is within range of any active light source and set state of thorn
    private void CheckLightLevel()
    {
        List<LightSource> activeLightSources = GetActiveLightSources();

        if (activeLightSources.Count > 0)
        {
            currentState += 1;
            SetThornState(currentState);
        }
    }

    // Show green buds (flower groups with each flower deactivated)
    private IEnumerator BudGreenThorn()
    {
        ActivateChildren(this.gameObject);
        yield return new WaitForSeconds(0.0f);
    }

    // Show flowers + destroy the thorn after some time
    private IEnumerator BloomFlower()
    {
        // Activate each flower
        foreach (Transform flower_group in this.gameObject.transform)
        {
            foreach (Transform flower in flower_group.transform)
            {
                ActivateChildObjectsByName(flower.gameObject, "pPipe28");
            }
        }
        yield return new WaitForSeconds(destroyWaitTime);
        Destroy(this.gameObject);
    }

    // Set state of thorn + execute corresponding action: bud green thorns + bloom flower
    private void SetThornState(int state)
    {
        if (state == 1)
        {
            StartCoroutine(BudGreenThorn());
        }
        else if (state == 2)
        {
            CancelInvoke(nameof(CheckLightLevel));
            StartCoroutine(BloomFlower());
        }
    }

    // Detect when a LightSource enters the Thorn's trigger area
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is a LightSource
        LightSource lightSource = other.GetComponent<LightSource>();
        if (lightSource != null)
        {
            if (!inRangeLightSource.Contains(lightSource))
            {
                inRangeLightSource.Add(lightSource); // Add the LightSource to the list
            }
        }
    }

    // Detect when a LightSource exits the Thorn's trigger area
    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger is a LightSource
        LightSource lightSource = other.GetComponent<LightSource>();
        if (lightSource != null)
        {
            inRangeLightSource.Remove(lightSource); // Remove the LightSource from the list
        }
    }

}
