using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOutUI : MonoBehaviour
{
    public float delayBeforeFade = 5f;
    public float fadeOutDuration = 2f;
    
    private Image[] uiImages;
    private TextMeshProUGUI[] uiTexts;
    private float fadeTimer;

    private void Start()
    {
        // Get the Image components and TMP components from the children of this GameObject
        uiImages = GetComponentsInChildren<Image>();
        uiTexts = GetComponentsInChildren<TextMeshProUGUI>();

        // Start the fade-out coroutine
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Wait for the specified delay before starting the fade-out
        yield return new WaitForSeconds(delayBeforeFade);

        // Start fading out over the duration
        fadeTimer = 0f;

        // Store the initial colors of images and texts
        Color[] initialImageColors = new Color[uiImages.Length];
        for (int i = 0; i < uiImages.Length; i++)
        {
            initialImageColors[i] = uiImages[i].color;
        }
        
        Color[] initialTextColors = new Color[uiTexts.Length];
        for (int i = 0; i < uiTexts.Length; i++)
        {
            initialTextColors[i] = uiTexts[i].color;
        }

        while (fadeTimer < fadeOutDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeOutDuration);

            // Set the alpha for each Image component
            for (int i = 0; i < uiImages.Length; i++)
            {
                if (uiImages[i])
                {
                    uiImages[i].color = new Color(
                        initialImageColors[i].r, 
                        initialImageColors[i].g, 
                        initialImageColors[i].b, 
                        alpha
                    );
                }
            }

            // Set the alpha for each TMP component
            for (int i = 0; i < uiTexts.Length; i++)
            {
                if (uiTexts[i])
                {
                    uiTexts[i].color = new Color(
                        initialTextColors[i].r, 
                        initialTextColors[i].g, 
                        initialTextColors[i].b, 
                        alpha
                    );
                }
            }

            yield return null;
        }

        // Ensure all images and texts are fully transparent at the end of the fade
        for (int i = 0; i < uiImages.Length; i++)
        {
            if (uiImages[i])
            {
                uiImages[i].color = new Color(
                    initialImageColors[i].r, 
                    initialImageColors[i].g, 
                    initialImageColors[i].b, 
                    0f
                );
            }
        }

        for (int i = 0; i < uiTexts.Length; i++)
        {
            if (uiTexts[i])
            {
                uiTexts[i].color = new Color(
                    initialTextColors[i].r, 
                    initialTextColors[i].g, 
                    initialTextColors[i].b, 
                    0f
                );
            }
        }
    }
}
