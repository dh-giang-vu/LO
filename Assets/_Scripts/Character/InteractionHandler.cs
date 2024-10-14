using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractionHandler : MonoBehaviour
{
    private List<CollectResource> inRangeResources;
    private List<LightSource> inRangeLightSources;

    // Reference to the parent GameObject containing both the image and text
    public GameObject resourceUIParent;

    // Reference to the TextMeshProUGUI component for the text
    public TextMeshProUGUI resourceText;

    private Vector3 targetScale;
    private float animationSpeed = 5f;  // Speed of the scaling animation

    void Start()
    {
        inRangeResources = new List<CollectResource>();
        inRangeLightSources = new List<LightSource>();

        // Start with scale at zero (hidden)
        resourceUIParent.transform.localScale = Vector3.zero;

    }

    void Update()
    {
        // Check if there are any resources in range
        if (inRangeResources.Count > 0)
        {
            // Get the tag of the nearest resource in range
            string resourceTag = GetNearestInRangeResource().gameObject.tag;
            
            // Format and display the text
            resourceText.text = $"E - Collect {resourceTag}";

            // Set the target scale to full size (pop in)
            targetScale = Vector3.one;
        }
        else
        {
            // Hide the parent UI GameObject if no resources are in range
            targetScale = Vector3.zero;
        }
        resourceUIParent.transform.localScale = Vector3.Lerp(resourceUIParent.transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }

    public bool GatherResources()
    {
        CollectResource nearestResource = GetNearestInRangeResource();
        if (nearestResource == null)
        {
            return false;
        }
        string resourceTag = nearestResource.gameObject.tag;
        inRangeResources.Remove(nearestResource);
        nearestResource.Interact();
        
        return resourceTag == "Ore" || resourceTag == "Stone";
    }

    // Find and return nearest in range resource to player
    private CollectResource GetNearestInRangeResource()
    {
        CollectResource nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (CollectResource resource in inRangeResources)
        {
            float distance = Vector3.Distance(playerPosition, resource.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = resource;
            }
        }

        return nearest;
    }

    public void RefuelLightSources()
    {
        for (int i = 0; i < inRangeLightSources.Count; i++)
        {
            StartCoroutine(inRangeLightSources[i].ManualRefuel());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Add(resource);
        }
        if (other.TryGetComponent<LightSource>(out var lightSource))
        {
            inRangeLightSources.Add(lightSource);
            Debug.LogWarning(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Remove(resource);
        }
        if (other.TryGetComponent<LightSource>(out var lightSource))
        {
            inRangeLightSources.Remove(lightSource);
        }
    }

}
