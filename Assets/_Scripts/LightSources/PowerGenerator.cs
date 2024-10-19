using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : LightSource
{
    [SerializeField] private float refuelWaitTime = 1f;
    float refuelTime = 0f;
    private List<ElectricLight> activeLights = new List<ElectricLight>();

    private Inventory inventory;  // Dynamically found reference to the player's Inventory
    private int requiredCoal = 1;

    void Start()
    {
        // Find Inventory instance in the scene if it's not assigned
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("Inventory not found in the scene. Ensure an Inventory exists.");
            }
        }
    }

    void Update()
    {
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
        ElectricLight lightBulb = other.GetComponent<ElectricLight>();
        if (lightBulb != null)
        {
            activeLights.Add(lightBulb);
            if (alive)
            {
                lightBulb.Refuel();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ElectricLight lightBulb = other.GetComponent<ElectricLight>();
        if (lightBulb != null)
        {
            lightBulb.Die();
            activeLights.Remove(lightBulb);
        }
    }

    public override IEnumerator ManualRefuel()
    {
        CollectableClass coal = inventory.items.Find(item => item.itemName == "Coal");

        if (coal != null && coal.quantity >= requiredCoal)
        {
            coal.quantity -= requiredCoal;
            inventory.UpdateMaterialCounts();
            yield return new WaitForSeconds(refuelWaitTime);
            Refuel();
            Debug.Log("Generator refueled, 1 coal consumed.");
        }
        else
        {
            Debug.LogWarning("Not enough coal to refuel the generator.");
        }
    }

    public override void Refuel()
    {
        lifespan = maxLifespan;
        refuelTime = Time.time;
        alive = true;

        foreach (ElectricLight light in activeLights)
        {
            light.Refuel();
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
        return alive;
    }

    public override float getSanityEffect()
    {
        return -1f;
    }
}
