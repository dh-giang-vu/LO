using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasLight : ElectricLight
{
    private Color[] colors = new Color[] {
    new Color(1, 0, 0, 0.7f),   // Red
    new Color(1, 0.5f, 0, 0.7f), // Orange
    new Color(1, 1, 0, 0.7f),   // Yellow
    new Color(0, 1, 0, 0.7f),   // Green
    new Color(0, 0, 1, 0.7f),   // Blue
    new Color(0.29f, 0, 0.51f, 0.7f), // Indigo
    new Color(0.56f, 0, 1, 0.7f)  // Violet
};
    private int currentColorIndex = 0;
    public float switchInterval = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
        StartCoroutine(SwitchColor());
        Die();
    }

    void Update()
    {
    }

        IEnumerator SwitchColor()
    {
        // Loop indefinitely
        while (true)
        {
            // Set the light color to the current color
            light.color = colors[currentColorIndex];

            // Move to the next color in the array (loop back to 0 if at the end)
            currentColorIndex = (currentColorIndex + 1) % colors.Length;

            // Wait for the interval before switching again
            yield return new WaitForSeconds(switchInterval);
        }
    }
}