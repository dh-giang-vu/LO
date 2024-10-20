using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public GameObject targetObject;  // The object to fade out

    public float fadeDuration = 2f;  // Duration of fade

    private TextMeshProUGUI[] textMeshes;  // TextMeshPro components
    private Image[] images;                // Image components

    void Start()
    {
        // Collect TextMeshPro and Image components from the target object
        textMeshes = targetObject.GetComponentsInChildren<TextMeshProUGUI>();
        images = targetObject.GetComponentsInChildren<Image>();
        
        // Disable the object initially if needed, but DO NOT set alpha to 0 here
        targetObject.SetActive(false);  
    }

    public void ActivateAndFadeOut()
    {
        // Ensure the object is fully visible (set alpha to 1)
        SetAlpha(1f);

        // Activate the target object and start the fade-out coroutine
        targetObject.SetActive(true);
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float elapsedTime = 0f;

        // Fade out the object over the specified duration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Gradually fade alpha from 1 to 0

            SetAlpha(alpha);
            yield return null;  // Wait for the next frame
        }

        // Ensure the alpha is fully 0 at the end of the fade
        SetAlpha(0f);

        // Deactivate the object once it's fully faded out
        targetObject.SetActive(false);
    }

    // Helper method to set the alpha of all TextMeshProUGUI and Image components
    private void SetAlpha(float alpha)
    {
        // Set alpha for TextMeshProUGUI components
        foreach (TextMeshProUGUI textMesh in textMeshes)
        {
            Color color = textMesh.color;
            color.a = alpha;
            textMesh.color = color;
        }

        // Set alpha for Image components
        foreach (Image image in images)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
