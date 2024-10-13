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
    [SerializeField] SanityManager sanityManager;

    // Start is called before the first frame update
    void Start()
    {
        Volume globalVolume = GetComponent<Volume>();
        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments) &&
            globalVolume.profile.TryGet<Vignette>(out vignette))
        {
            // Get the current color from Color Adjustments' Color Filter
            currentColor = colorAdjustments.colorFilter.value;
        }
        else
        {
            Debug.LogError("Color Adjustments or Vignette not found in the Volume Profile.");
        }
    }
    void Update() {
        currentColor = colorAdjustments.colorFilter.value;
        UpdateVignette();
        
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

    public void AdjustFade(float intensity) {
        vignette.intensity.value = intensity;
    }

    private void UpdateVignette() {
        float vignetteAmount = sanityManager.GetSanityAmount();
        vignette.intensity.value = (1 - vignetteAmount);
    }
}
