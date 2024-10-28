using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceManager : LightSource
{
    private Light light = null;
    private ParticleSystem smokes;
    private Inventory inventory;  // Dynamically found reference to the player's Inventory

    public float intensity = 200f;

    float refuelTime = 0f;
    private Renderer renderer;
    private int requiredCoal = 1; // Define the amount of coal required for refuel

    [SerializeField] private float refuelWaitTime = 1f;

    // SFX
    [SerializeField] private AudioClip fireCrackle;   
    [SerializeField] RefuelMarker refuelMarker; 
    private AudioSource audioSource;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        light = GetComponentInChildren<Light>();
        smokes = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        // Find Inventory instance in the scene if it's not assigned
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("Inventory not found in the scene. Ensure an Inventory exists.");
            }
        }

        Refuel();

        if (light == null)
        {
            Debug.Log("No Light component found in the children of this GameObject.");
        }
    }

    void Update()
    {
        if (lifespan > 0)
        {
            lifespan = maxLifespan - (Time.time - refuelTime);
            // renderer.material.SetFloat("_Dimming", 0.1f + 0.7f * (1 - ((maxLifespan - lifespan) / maxLifespan)));
            light.intensity = intensity * (1 - ((maxLifespan - lifespan) / maxLifespan));
        }
        else if (lifespan <= 0 && alive)
        {
            Die();
        }
        if (refuelMarker != null && Refuelable()) {
            refuelMarker.Activate();
        } else {
            refuelMarker.Deactivate();
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
            Debug.Log("Fire refueled, 1 coal consumed.");
        }
        else
        {
            Debug.LogWarning("Not enough coal to refuel.");
        }
    }

    public override void Refuel()
    {
        lifespan = maxLifespan;
        light.intensity = intensity;
        TurnOnSmoke();
        refuelTime = Time.time;
        alive = true;

        PlayRefuelSFX();
    }

    public override void Die()
    {
        TurnOffSmoke();
        lifespan = -1;
        light.intensity = 0;
        alive = false;

        StopRefuelSFX();
    }

    private void TurnOffSmoke()
    {
        smokes.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void TurnOnSmoke()
    {
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

    private void PlayRefuelSFX()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = fireCrackle;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    private void StopRefuelSFX()
    {
        // Stop the generator run loop audio
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    public override bool Refuelable()
    {
        if (lifespan < maxLifespan*0.25)
            return true;
        return false;
    }
}
