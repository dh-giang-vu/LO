using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLight : LightSource
{
    protected Light light;

    // Start is called before the first frame update
    void Start()
    {
        
        // Get the Light component and turn it off initially
        light = GetComponentInChildren<Light>();
        Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     // Method to turn on the light
    public override void Refuel()
    {
        if (light != null)
        {
            light.enabled = true;
            alive = true;
        }
    }

    // Method to turn off the light
    public override void Die()
    {
        if (light != null)
        {
            light.enabled = false;
            alive = false;
        }
    }

    public override bool isActive()
    {
        return alive;
    }

    public override float getSanityEffect()
    {
        return 0.0f;
    }

    public override IEnumerator ManualRefuel()
    {
        // throw new System.NotImplementedException();
        return null;
    }
    public override bool Refuelable()
    {
        return false;
    }
}
