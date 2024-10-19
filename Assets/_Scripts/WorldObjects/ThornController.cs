using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornController : MonoBehaviour
{

    [SerializeField]private List<LightSource> inRangeLightSource = new List<LightSource>();

    // States: 
    //      0 - only base vine thorn
    //      1 - budding green thorns
    //      2 - flower bloom
    [SerializeField]private int currentState = 0;
    [SerializeField] private float scaleAnimationTime = 2.0f;
    [SerializeField] private float scaleMultiplier = 1.0f;


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

        InvokeRepeating(nameof(CheckLightLevel), 0.0f, 5.0f);

    }

    private void DeactivateChildren(GameObject parentObject)
    {
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

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


    private void ActivateChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            StartCoroutine(ScaleUp(child.gameObject, scaleAnimationTime, false));
        }
    }

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

    private IEnumerator ScaleUp(GameObject child, float duration, bool useScale)
    {
        Vector3 originalScale = child.transform.localScale; // Store original scale
        child.transform.localScale = Vector3.zero; // Set to tiny (zero scale)

        float elapsedTime = 0f;

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

    private void CheckLightLevel()
    {
        List<LightSource> activeLightSources = GetActiveLightSources();

        if (activeLightSources.Count > 0)
        {
            currentState += 1;
            SetThornState(currentState);
        }
    }

    private IEnumerator BudGreenThorn()
    {
        Debug.LogWarning("Bud Green Thorn");
        ActivateChildren(this.gameObject);
        yield return new WaitForSeconds(0.0f);
    }

    private IEnumerator BloomFlower()
    {
        Debug.LogWarning("Bloom Flower");
        // Activate each flower
        foreach (Transform flower_group in this.gameObject.transform)
        {
            foreach (Transform flower in flower_group.transform)
            {
                ActivateChildObjectsByName(flower.gameObject, "pPipe28");
            }
        }
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

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
