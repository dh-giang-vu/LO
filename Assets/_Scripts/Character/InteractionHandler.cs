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
            // Get the tag of the first resource in range
            string resourceTag = inRangeResources[0].gameObject.tag;
            
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
