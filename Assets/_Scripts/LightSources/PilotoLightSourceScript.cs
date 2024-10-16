using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PilotoLightSourceScript : LightSource
{

    private Light light = null;
    private ParticleSystem smokes;
    private Inventory inventory;  // Reference to the player's Inventory singleton


    public float intensity = 200f;

    float refuelTime = 0f;
    private Renderer renderer;
    private int requiredCoal = 1; // Define the amount of coal required for refuel


    [SerializeField] private float refuelWaitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        light = GetComponentInChildren<Light>();
        Transform firstChild = transform.GetChild(0);
        smokes = firstChild.GetComponent<ParticleSystem>();
        inventory = Inventory.Instance; // Get the singleton instance of the Inventory

        // Ensure the inventory instance is valid
        if (inventory == null)
        {
            Debug.LogError("Inventory singleton instance is null. Ensure Inventory is instantiated.");
        }
        Refuel();


        if (light == null)
        {
            Debug.Log("No Light component found in the children of this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        //Debug.Log(lifespan);
        if (lifespan > 0) {
            lifespan = maxLifespan - (Time.time - refuelTime);
            renderer.material.SetFloat("_Dimming", 0.1f+0.7f * ( 1 - ((maxLifespan -lifespan)/maxLifespan)));
            light.intensity = intensity * ( 1 - ((maxLifespan - lifespan)/maxLifespan));
        }
        else if (lifespan <= 0 && alive) {
            Die();
        }
        
    }
    public override IEnumerator ManualRefuel()
    {
         // Check if the player has enough coal
        CollectableClass coal = inventory.items.Find(item => item.itemName == "Coal");

        if (coal != null && coal.quantity >= requiredCoal)
        {
            // Deduct the required amount of coal
            coal.quantity -= requiredCoal;

            // Update the inventory to reflect the change in coal amount
            inventory.UpdateMaterialCounts();

      
         yield return new WaitForSeconds(refuelWaitTime);
         Refuel();

            Debug.Log("fire refueled, 1 coal consumed.");
        }
        else
        {
            Debug.LogWarning("Not enough coal to refuel the generator. 5 coal required.");
        }
  
        //  yield return new WaitForSeconds(refuelWaitTime);
        //  Refuel();
    }

    public override void Refuel() {
        
        lifespan = maxLifespan;
        light.intensity = intensity;
        TurnOnSmoke();
        refuelTime = Time.time;
        alive = true;
    }

    public override void Die() {
        TurnOffSmoke();
        lifespan = -1;
        light.intensity = 0;
        alive = false;
    }

    private void TurnOffSmoke() {
        smokes.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        Debug.Log("All particles cleared.");
    }

    private void TurnOnSmoke() {
        smokes.Play();
    }

    public override bool isActive()
    {
        return alive;
    }

    public override float getSanityEffect()
    {
        return 0.0f;
    }
}
