using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractionHandler : MonoBehaviour
{
    private List<CollectResource> inRangeResources;
    private List<PilotoLightSourceScript> inRangePilotoLightSources;

    // Reference to the parent GameObject containing both the image and text
    public GameObject resourceUIParent;

    // Reference to the TextMeshProUGUI component for the text
    public TextMeshProUGUI resourceText;

    private Vector3 targetScale;
    private float animationSpeed = 5f;  // Speed of the scaling animation

    void Start()
    {
        inRangeResources = new List<CollectResource>();
        inRangePilotoLightSources = new List<PilotoLightSourceScript>();

        // Initially hide the entire parent object that contains both the text and the image
        //resourceUIParent.SetActive(false);

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

            // Show the parent UI GameObject
            //resourceUIParent.SetActive(true);

            // Set the target scale to full size (pop in)
            targetScale = Vector3.one;
        }
        else
        {
            // Hide the parent UI GameObject if no resources are in range
            targetScale = Vector3.zero;
            //resourceUIParent.SetActive(false);
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
