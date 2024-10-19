using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasLight : ElectricLight
{
    private float hueValue = 0f;  // Start at the beginning of the color wheel (red)
    public float hueSpeed = 0.1f; // Speed at which the hue changes

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        // Increment hueValue over time, wrapping it back to 0 after it reaches 1
        hueValue += hueSpeed * Time.deltaTime%1;
        // if (hueValue >= 1f)
        // {
        //     hueValue -= 1f;  // Wrap the hue value back to 0 to keep cycling through the color wheel
        //     Debug.Log("Hue DEAD "+ hueValue);
        // }
        Debug.Log("Hue Value "+ hueValue);
        // Convert the HSV value to RGB and assign it to the light's color
        light.color = Color.HSVToRGB(hueValue, 1f, 1f);  // Saturation and Value are set to 1 for full color
        Debug.Log("Current Light Color: " + light.color);
    }
}
