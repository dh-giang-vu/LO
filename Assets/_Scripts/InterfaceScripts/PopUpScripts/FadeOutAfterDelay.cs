using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class FadeOutAfterDelay : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color objectColor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectColor = objectRenderer.material.color;
        
        // Start the coroutine to handle fading
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Fade out over 2 seconds
        float fadeDuration = 2f;
        float startAlpha = objectColor.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            objectColor.a = alpha;
            objectRenderer.material.color = objectColor;

            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is set to 0
        objectColor.a = 0f;
        objectRenderer.material.color = objectColor;

        // Optionally, you can deactivate the object after fading out
        gameObject.SetActive(false);
    }
}

