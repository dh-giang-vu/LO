using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : ElectricLight
{
    private Color[] colors = new Color[] { new Color(1, 0, 0, 0.7f), new Color(1, 1, 0, 0.7f), new Color(0, 1, 0, 0.7f) };
    private int currentColorIndex = 0;
    public float switchInterval = 1f;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
        Refuel();
        StartCoroutine(SwitchColor());
    }

    // Update is called once per frame
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
