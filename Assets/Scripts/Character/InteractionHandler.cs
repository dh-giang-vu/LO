using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{

    private List<CollectResource> inRangeResources;

    void Start()
    {
        inRangeResources = new List<CollectResource>();
    }

    void Update()
    {
        if (inRangeResources.Count == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < inRangeResources.Count; i++)
            {
                var curr = inRangeResources[i];
                inRangeResources.Remove(curr);
                curr.Interact();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Add(resource);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CollectResource>(out var resource))
        {
            inRangeResources.Remove(resource);
        }
    }

}
