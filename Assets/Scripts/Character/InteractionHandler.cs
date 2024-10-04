using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{

    private List<CollectResource> inRangeResources;
    private List<PilotoLightSourceScript> inRangePilotoLightSources;
     private List<PowerGenerator> inRangePowerGenerators;

    public String resourceInRange;

    void Start()
    {
        inRangeResources = new List<CollectResource>();
        inRangePilotoLightSources = new List<PilotoLightSourceScript>();
        inRangePowerGenerators = new List<PowerGenerator>();
    }

    void Update()
    {
        for (int i = 0; i < inRangeResources.Count; i++)
        {
            Debug.Log("resource is: " + inRangeResources[i]);
        }
        
    }

    public void GatherResources()
    {
        for (int i = 0; i < inRangeResources.Count; i++)
        {
            var curr = inRangeResources[i];
            inRangeResources.Remove(curr);
            curr.Interact();
        }
    }

   

    public void RefuelLightSources()
    {
        for (int i = 0; i < inRangePilotoLightSources.Count; i++)
        {
            StartCoroutine(inRangePilotoLightSources[i].ManualRefuel());
        }
    }

//power generator
    public void RefuelPowerGenerator()
    {
        for (int i = 0; i < inRangePowerGenerators.Count; i++)
        {
            StartCoroutine(inRangePowerGenerators[i].ManualRefuel());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Add(resource);
        }

        if (other.TryGetComponent<PilotoLightSourceScript>(out var pilotoLight))
        {
            inRangePilotoLightSources.Add(pilotoLight);
        }
//power generator
        if (other.TryGetComponent<PowerGenerator>(out var powerGenerator))
        {
            inRangePowerGenerators.Add(powerGenerator);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Remove(resource);
        }
        if (other.TryGetComponent<PilotoLightSourceScript>(out var pilotoLight))
        {
            inRangePilotoLightSources.Remove(pilotoLight);
        }
        //power generator
        if (other.TryGetComponent<PowerGenerator>(out var powerGenerator))
        {
            inRangePowerGenerators.Remove(powerGenerator);
        }
    }

}
