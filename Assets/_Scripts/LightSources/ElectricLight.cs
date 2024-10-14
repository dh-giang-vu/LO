using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLight : LightSource
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
    public override void Refuel()
    {
        if (ElecLight != null)
        {
            ElecLight.enabled = true;
            active = true;
        }
    }

    // Method to turn off the light
    public override void Die()
    {
        if (ElecLight != null)
        {
            ElecLight.enabled = false;
            active = false;
        }
    }

    public override bool isActive()
    {
        return active;
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
}
