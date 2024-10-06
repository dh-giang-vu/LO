using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerGenerator : LightSource
{

    [SerializeField] private float refuelWaitTime = 1f;
    float refuelTime = 0f;
    private List<ElectricLight> activeLights = new List<ElectricLight>(); // Keep track of active light bulbs
    // Start is called before the first frame update
    void Start()
    {
        Refuel();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Power generator: " + lifespan);
        if (lifespan > 0)
        {
            lifespan = maxLifespan - (Time.time - refuelTime);
        }
        else if (lifespan <= 0 && alive)
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a LightBulb component
        ElectricLight lightBulb = other.GetComponent<ElectricLight>();
        if (lightBulb != null)
        {

            activeLights.Add(lightBulb); // Add to the list of active lights
            if (alive)
            {
                // Turn on the light if it's a LightBulb
                lightBulb.Refuel();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has a LightBulb component
        ElectricLight lightBulb = other.GetComponent<ElectricLight>();
        if (lightBulb != null)
        {
            // Turn off the light if it's a LightBulb
            lightBulb.Die();
            activeLights.Remove(lightBulb); // Remove from the list of active lights
        }
    }

    public override IEnumerator ManualRefuel()
    {
        yield return new WaitForSeconds(refuelWaitTime);
        Refuel();
    }

    public override void Refuel()
    {
        lifespan = maxLifespan;
        refuelTime = Time.time;
        alive = true;
        if (activeLights.Any())
        {
            foreach (ElectricLight light in activeLights)
            {
                light.Refuel();
            }
        }
    }

    public override void Die()
    {
        lifespan = -1;
        alive = false;
        foreach (ElectricLight light in activeLights)
        {
            light.Die();
        }
    }

    public override bool isActive()
    {
        return false;
    }

    public override float getSanityEffect()
    {
        return -1f;
    }
}
