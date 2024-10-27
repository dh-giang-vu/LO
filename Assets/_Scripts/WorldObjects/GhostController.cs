using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour, ISanityProvider
{
    [SerializeField] private float timeToLive = 30.0f; // Time before the ghost is destroyed
    [SerializeField] private float fadeDuration = 1.0f; // Duration of fade in seconds
    [SerializeField] private Renderer ghostRenderer; // Expose the Renderer in the Inspector

    private GhostSpawner ghostSpawner; // Reference to the GhostSpawner
    private GameObject player; // Reference to the player GameObject

    // Store the original values for restoration later
    private float originalTransparency;
    private Color originalAuraColor;
    bool active = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ghostSpawner = GetComponentInParent<GhostSpawner>();

        // Check if the ghostRenderer is not null before proceeding
        if (ghostRenderer != null)
        {
            // Store the original values
            originalTransparency = ghostRenderer.material.GetFloat("_Transparency");
            originalAuraColor = ghostRenderer.material.GetColor("_AuraColor");

            // Set transparency and aura color to fully transparent
            ghostRenderer.material.SetFloat("_Transparency", 0); // Fully transparent
            originalAuraColor.a = 0; // Set alpha of AuraColor to 0
            ghostRenderer.material.SetColor("_AuraColor", originalAuraColor); // Apply modified color

            StartCoroutine(FadeIn()); // Start the fade-in coroutine
        }
        else
        {
            Debug.LogError("No Renderer assigned to the Ghost Controller. Please assign a Renderer in the Inspector.");
        }

        StartCoroutine(DestroyAfterTime()); // Start the coroutine to destroy the ghost after timeToLive seconds
    }

    void Update()
    {
        // Make the ghost face the player
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    // Coroutine to handle the fade-in effect
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the new transparency and aura color values
            float t = elapsedTime / fadeDuration;

            // Restore transparency and aura color over time
            float newTransparency = Mathf.Lerp(0, originalTransparency, t); // From 0 to original transparency
            originalAuraColor.a = Mathf.Lerp(0, originalAuraColor.a, t); // From 0 to original aura alpha

            // Apply the new values to the material
            ghostRenderer.material.SetFloat("_Transparency", newTransparency);
            ghostRenderer.material.SetColor("_AuraColor", originalAuraColor);

            yield return null; // Wait until the next frame
        }

        // Ensure the final transparency and aura color are set to their original values
        ghostRenderer.material.SetFloat("_Transparency", originalTransparency);
        originalAuraColor.a = 1; // Ensure aura color is fully opaque
        ghostRenderer.material.SetColor("_AuraColor", originalAuraColor);
    }

    // Coroutine to destroy the ghost after timeToLive seconds
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToLive);
        ghostSpawner.RemoveGhost(gameObject);
        active = false;
        Destroy(gameObject);
    }
    public float getSanityEffect() {
        return -0.05f;
    }
    public bool isActive() {
        return active;
    }
}
