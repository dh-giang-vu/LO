using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ScreenPostProcessing : MonoBehaviour
{
    private ColorAdjustments colorAdjustments;
    private Vignette vignette;
    private Color currentColor;
    private Volume globalVolume;
    private LensDistortion lensDistortion;
    [SerializeField] SanityManager sanityManager;
    public float distortionIntensity = 0f; // Current distortion intensity
    public float speed = 1f;              // Speed of the distortion change
    public float maxDistortion = 1f;    // Maximum distortion intensity
    public float minDistortion = -1f;   // Minimum distortion intensity

    private bool increasing = true;       // Whether the distortion is increasing or decreasing
    bool isFading = false;
    public float fadeDuration = 1f; // Duration of the fade

    // Start is called before the first frame update
    void Start()
    {
        globalVolume = GetComponent<Volume>();
        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments) &&
            globalVolume.profile.TryGet<Vignette>(out vignette) &&
            globalVolume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            // Get the current color from Color Adjustments' Color Filter
            currentColor = colorAdjustments.colorFilter.value;
            lensDistortion.intensity.overrideState = true;
        }
        else
        {
            Debug.LogError("Color Adjustments or Vignette not found in the Volume Profile.");
        }
    }
    void Update() {
        currentColor = colorAdjustments.colorFilter.value;
        Distort();
        
    }

    public void BuildingFilterOn() {
        colorAdjustments.colorFilter.value = new Color(0.9f, colorAdjustments.colorFilter.value.g, colorAdjustments.colorFilter.value.b);
    }
    public void BuildingFilterOff() {
        colorAdjustments.colorFilter.value = new Color(1, colorAdjustments.colorFilter.value.g, colorAdjustments.colorFilter.value.b);
    }

    public void LightSourceFilterOn() {
        colorAdjustments.colorFilter.value = new Color(colorAdjustments.colorFilter.value.r, colorAdjustments.colorFilter.value.g, 0.9f);
    }
    public void LightSourceFilterOff() {
        colorAdjustments.colorFilter.value = new Color(colorAdjustments.colorFilter.value.r, colorAdjustments.colorFilter.value.g, 1);
    }

        public void GhostFilterOn()
    {
        Color targetColor = new Color(0.3f, 0.3f, 0.3f); // Target ghost filter color
        if (!isFading) StartCoroutine(FadeColor(targetColor));
    }

    public void GhostFilterOff()
    {
        Color targetColor = new Color(1f, 1f, 1f); // Target default color
        if (!isFading) StartCoroutine(FadeColor(targetColor));
    }

        private IEnumerator FadeColor(Color newColor)
    {
        isFading = true;
        Color currentColor = colorAdjustments.colorFilter.value; // Current color
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // Lerp between the current and target color
            colorAdjustments.colorFilter.value = Color.Lerp(currentColor, newColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime; // Increase elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure final color is set at the end of the fade
        colorAdjustments.colorFilter.value = newColor;
        isFading = false;
    }

    public void AdjustFade(float intensity) {
        vignette.intensity.value = intensity;
    }

    private void UpdateVignette() {
        float vignetteAmount = sanityManager.GetSanityAmount();
        vignette.intensity.value = (1 - vignetteAmount);
    }

    private void Distort() {
        if (lensDistortion != null)
        {
            // Smoothly change the distortion intensity for a nausea effect
            if (increasing)
            {
                distortionIntensity += speed * Time.deltaTime;
                if (distortionIntensity >= maxDistortion)
                {
                    increasing = false;
                }
            }
            else
            {
                distortionIntensity -= speed * Time.deltaTime;
                if (distortionIntensity <= minDistortion)
                {
                    increasing = true;
                }
            }

            // Apply the changing intensity to the lens distortion effect
            lensDistortion.intensity.value = distortionIntensity;
        }
    }
}
