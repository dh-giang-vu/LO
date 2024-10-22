using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionHandler : MonoBehaviour
{
    private List<CollectResource> inRangeResources;
    private List<LightSource> inRangeLightSources;
    private List<GameObject> inRangeCrows;  // List to store nearby crows

    // Reference to the parent GameObject containing both the image and text
    public GameObject resourceUIParent;

    // Reference to the TextMeshProUGUI component for the text
    public TextMeshProUGUI resourceText;

    private Vector3 targetScale;

    public InterfaceManager interfaceManager;


    private float animationSpeed = 5f;  // Speed of the scaling animation

    void Start()
    {
        inRangeResources = new List<CollectResource>();
        inRangeLightSources = new List<LightSource>();
        inRangeCrows = new List<GameObject>();  // Initialize the crow list

        // Start with scale at zero (hidden)
        resourceUIParent.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        // Check if there are any crows in range
        if (inRangeCrows.Count > 0)
        {
            // Show "E - Speak" when near a crow
            resourceText.text = "E - Speak";

            // Set the target scale to full size (pop in)
            targetScale = Vector3.one;

            interfaceManager.OnCrowEnter();
        }
        // Check if there are any resources in range if no crows are nearby
        else if (inRangeResources.Count > 0)
        {
            // Get the tag of the nearest resource in range
            string resourceTag = GetNearestInRangeResource().gameObject.tag;

            // Format and display the text for resource interaction
            resourceText.text = $"E - Collect {resourceTag}";

            // Set the target scale to full size (pop in)
            targetScale = Vector3.one;
        }
        else
        {
            // Hide the parent UI GameObject if no resources or crows are in range
            targetScale = Vector3.zero;
        }

        // Smoothly transition the UI scaling
        resourceUIParent.transform.localScale = Vector3.Lerp(resourceUIParent.transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }

    public string GatherResources()
    {
        CollectResource nearestResource = GetNearestInRangeResource();
        if (nearestResource == null)
        {
            return "NOTHING";
        }
        string resourceTag = nearestResource.gameObject.tag;
        inRangeResources.Remove(nearestResource);
        nearestResource.Interact();

        if (resourceTag == "Ore" || resourceTag == "Stone")
        {
            return "mining";
        }
        else if (resourceTag == "Tree")
        {
            return "chopping";
        }
        return "";
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

    public bool RefuelLightSources()
    {
        for (int i = 0; i < inRangeLightSources.Count; i++)
        {
            StartCoroutine(inRangeLightSources[i].ManualRefuel());
        }
        if (inRangeLightSources.Count == 0) {
            return false;
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Add(resource);
        }
        if (other.TryGetComponent<LightSource>(out var lightSource) && other is BoxCollider)
        {
            inRangeLightSources.Add(lightSource);
        }

        // Detect GameObjects tagged as "crow" and add them to the inRangeCrows list
        if (other.CompareTag("crow"))
        {
            inRangeCrows.Add(other.gameObject);
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

        // Remove crows from the list when they exit the trigger area
        if (other.CompareTag("crow"))
        {
            inRangeCrows.Remove(other.gameObject);
            interfaceManager.OnCrowExit();
        }
    }
}
