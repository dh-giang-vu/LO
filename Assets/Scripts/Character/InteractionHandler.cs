using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{

    private List<CollectResource> inRangeResources;
    private List<PilotoLightSourceScript> inRangePilotoLightSources;

    void Start()
    {
        inRangeResources = new List<CollectResource>();
        inRangePilotoLightSources = new List<PilotoLightSourceScript>();
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
    }

}
