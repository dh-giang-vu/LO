using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornController : MonoBehaviour
{
    private List<LightSource> inRangeLightSource = new List<LightSource>();

    // States: 
    //      0 - only base vine thorn
    //      1 - budding green thorns
    //      2 - flower bloom
    private int currentState = 0;

    [SerializeField] private GameObject fragmentPrefab;  // Prefab for fragments
    [SerializeField] private Material explosionMaterial;  // Explosion shader material

    [SerializeField] private float flowerScaleAnimationTime = 2.0f;
    [SerializeField] private float flowerScaleMultiplier = 1.5f;
    [SerializeField] private float greenBudScaleAnimationTime = 2.0f;
    [SerializeField] private float greenBudScaleMultiplier = 1.5f;
    [SerializeField] private float checkLightInterval = 5.0f; // interval between checking for active light sources
    [SerializeField] private float destroyWaitTime = 5.0f;
    [SerializeField] private int fragmentCount = 10; // Number of fragments to create

    void Start()
    {
        DeactivateChildren(this.gameObject);
        foreach (Transform flower in this.gameObject.transform)
        {
            DeactivateChildObjectsByName(flower.gameObject, "pPipe28");
        }

        // Check if is in range of any active light sources every `checkLightInterval`
        InvokeRepeating(nameof(CheckLightLevel), checkLightInterval, checkLightInterval);
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
            if (child.name == childName)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void ActivateChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            StartCoroutine(ScaleUp(child.gameObject, greenBudScaleAnimationTime, "greenBud"));
        }
    }

    private void ActivateChildObjectsByName(GameObject parentObject, string childName)
    {
        foreach (Transform child in parentObject.transform)
        {
            if (child.name == childName)
            {
                child.gameObject.SetActive(true);
                StartCoroutine(ScaleUp(child.gameObject, flowerScaleAnimationTime, "flower"));
            }
        }
    }

    private IEnumerator ScaleUp(GameObject child, float duration, string useScale)
    {
        Vector3 originalScale = child.transform.localScale; // Store original scale
        child.transform.localScale = Vector3.zero; // Set to tiny (zero scale)

        float elapsedTime = 0f;

        if (useScale == "flower")
        {
            originalScale *= flowerScaleMultiplier;
        }
        else if (useScale == "greenBud")
        {
            originalScale *= greenBudScaleMultiplier;
        }

        while (elapsedTime < duration)
        {
            child.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

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
        foreach (Transform flower in this.gameObject.transform)
        {
            ActivateChildObjectsByName(flower.gameObject, "pPipe28");
        }

        // Wait before exploding
        yield return new WaitForSeconds(destroyWaitTime);

        // Trigger explosion effect on the thorn itself
        StartCoroutine(ApplyExplosionShader());

        // Animate fragmentation on the thorn
        StartCoroutine(FragmentThorn());

        // Wait a bit for the effect to complete
        yield return new WaitForSeconds(1.5f);

        // Destroy the thorn object after explosion
        Destroy(this.gameObject);
    }

    private IEnumerator FragmentThorn()
    {
        float fragmentationDuration = 1.0f; // Duration for fragmentation effect
        float elapsedTime = 0f;

        // Get the current scale of the thorn
        Vector3 originalScale = transform.localScale;
        
        while (elapsedTime < fragmentationDuration)
        {
            float progress = elapsedTime / fragmentationDuration;
            
            // Scale down the thorn to simulate fragmentation
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);

            // Apply random offsets to vertices to simulate jagged fragmentation
            float randomOffset = Mathf.Sin(progress * Mathf.PI) * 0.03f; // Use sin for gradual increase
            foreach (Transform child in transform)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-randomOffset, randomOffset),
                    Random.Range(-randomOffset, randomOffset),
                    Random.Range(-randomOffset, randomOffset)
                );

                child.localPosition += offset * progress; // Apply offset based on progress
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure thorn is fully "fragmented" at the end
        transform.localScale = Vector3.zero; // Final scale to zero
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

    private void OnTriggerEnter(Collider other)
    {
        LightSource lightSource = other.GetComponent<LightSource>();
        if (lightSource != null && !inRangeLightSource.Contains(lightSource))
        {
            inRangeLightSource.Add(lightSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LightSource lightSource = other.GetComponent<LightSource>();
        if (lightSource != null)
        {
            inRangeLightSource.Remove(lightSource);
        }
    }

    private IEnumerator ApplyExplosionShader()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.material = explosionMaterial;
        }

        float explosionDuration = 1.0f;
        float elapsedTime = 0f;

        while (elapsedTime < explosionDuration)
        {
            float progress = elapsedTime / explosionDuration;
            foreach (Renderer rend in renderers)
            {
                rend.material.SetFloat("_ExplosionProgress", progress);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Renderer rend in renderers)
        {
            rend.material.SetFloat("_ExplosionProgress", 1.0f);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private void CreateFragments()
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Random.rotation);
            fragment.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f); // Randomize scale for fragments
            Rigidbody rb = fragment.AddComponent<Rigidbody>();
            rb.AddExplosionForce(500f, transform.position, 5f); // Explode fragments
            // Optional: Add a fragment shader controller if needed
            // fragment.AddComponent<FragmentShaderController>().Initialize(this);
        }
    }
}
