using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLight : MonoBehaviour, ISanityProvider
{
    private Light ElecLight;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Light component and turn it off initially
         ElecLight = GetComponentInChildren<Light>();
        if (ElecLight != null)
        {
            ElecLight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     // Method to turn on the light
    public void TurnOn()
    {
        if (ElecLight != null)
        {
            ElecLight.enabled = true;
            active = true;
        }
    }

    // Method to turn off the light
    public void TurnOff()
    {
        if (ElecLight != null)
        {
            ElecLight.enabled = false;
            active = false;
        }
    }

    public bool isActive()
    {
        return active;
    }

    public float getSanityEffect()
    {
        return 0.0f;
    }
}
