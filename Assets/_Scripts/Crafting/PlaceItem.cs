using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    // Base properties for Item Placement
    [SerializeField] private GameObject itemToPlace = null;
    [SerializeField] private GameObject instantiatedItem = null;
    [SerializeField] private bool isPlacingItem = false;

    private Camera cam;

    // Debug mode
    [SerializeField] private bool debugMode = false;

    // For preventing collision during placing item
    private LayerMask instantiatedItemLayerMask;

    // Object rotate to look at player
    private GameObject player;

    // Cloud particle system
    [SerializeField] private ParticleSystem cloudParticleSystem;
    [SerializeField] private Vector3 defaultCloudSize = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField] private float cloudScaling = 0.25f;


    // Material handling variables
    [SerializeField] private Material craftingMaterial; // The material to apply while crafting
    private List<Material> originalMaterials = new List<Material>(); // Store original materials for all meshes
    private List<int> originalLayers = new List<int>(); // Store original layers for all meshes
    private bool overlaps = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        cam = Camera.main;
        instantiatedItemLayerMask = LayerMask.NameToLayer("Default");
    }

    void Update()
{
    // Not placing anything -> skip
    if (!isPlacingItem || itemToPlace == null)
    {
        return;
    }

    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("TerrainLayer")))
    {
        // Place the item at the hit point
        Vector3 placePosition = hit.point;
        
        if (instantiatedItem == null)
        {
            // Instantiate the item with its prefab's original rotation
            instantiatedItem = Instantiate(itemToPlace, placePosition, itemToPlace.transform.rotation);
            instantiatedItemLayerMask = instantiatedItem.layer;
            instantiatedItem.layer = LayerMask.NameToLayer("NoCollision");

            // Apply blueprint material to all RendererMeshes + move to NoCollision layer
            ModifyAllMeshes(instantiatedItem);
        }
        else
        {
            // Update the item's position
            instantiatedItem.transform.position = placePosition;

            // Retain the prefab's original rotation
            Vector3 originalRotation = itemToPlace.transform.eulerAngles;

            // Calculate the direction to look at the player
            Vector3 lookDirection = player.transform.position - instantiatedItem.transform.position;
            lookDirection.y = 0;  // Ignore any height differences

            // Calculate the Y-axis angle to face the player
            float targetYRotation = Quaternion.LookRotation(lookDirection).eulerAngles.y;

            // Apply only the Y-axis rotation to face the player, while keeping the original X and Z rotation from the prefab
            instantiatedItem.transform.eulerAngles = new Vector3(originalRotation.x, targetYRotation, originalRotation.z);
            List<Collider> colliders = GetNonTriggerColliders();
            if (IsOverlappingWithAny(instantiatedItem.GetComponent<BoxCollider>(), colliders)) {
                SetUnplaceable();
                Debug.Log("OVERLAPPING");
            } else {
                SetPlaceable();
                Debug.Log("NOT OVERLAPPING");
            }

             
        }
    }
    if (isPlacingItem && Input.GetKeyDown(KeyCode.C))
    {
        Destroy(instantiatedItem);
        instantiatedItem = null;
        isPlacingItem = false;
    }
    if (Input.GetKeyDown(KeyCode.Mouse0) && instantiatedItem != null && !overlaps)
    {
        StopPlacingItem();
    }
}

    private void SetPlaceable()
    {
        // Get the Renderer components from the GameObject and all its children
        Renderer[] renderers = instantiatedItem.GetComponentsInChildren<Renderer>();

        // Loop through each Renderer and set the colors
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Set the colors on each material
                material.SetColor("_Color1", new Color(0.0f, 0.0f, 0.55f));
                material.SetColor("_Color2", new Color(0.68f, 0.85f, 0.9f));
            }

        }

        overlaps = false;  // Some additional logic, if needed
    }

    private void SetUnplaceable()
    {
        // Get the Renderer components from the GameObject and all its children
        Renderer[] renderers = instantiatedItem.GetComponentsInChildren<Renderer>();

        // Loop through each Renderer and set the colors
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Set the colors on each material
                material.SetColor("_Color1", new Color(0.55f, 0.0f, 0.0f));  // Dark Red
                material.SetColor("_Color2", new Color(1.0f, 0.6f, 0.6f));   // Light Red
            }

        }

        overlaps = true;  // Some additional logic, if needed
    }

    private bool IsOverlappingWithAny(Collider itemCollider, List<Collider> colliders)
    {
        foreach (Collider collider in colliders)
        {
            // Use Bounds.Intersects to check if their bounds overlap
            if (collider.bounds.Intersects(itemCollider.bounds))
            {
                return true;  // Overlap found
            }
        }
        return false;  // No overlaps
    }
    private List<Collider> GetNonTriggerColliders()
    {
        // Get all colliders in the scene
        Collider[] allColliders = FindObjectsOfType<Collider>();

        // Create a list to store the non-trigger colliders
        List<Collider> nonTriggerColliders = new List<Collider>();

        // Iterate through all colliders and filter out those with isTrigger = true
        foreach (Collider collider in allColliders)
        {
            if (!collider.isTrigger && (collider.gameObject.layer == 0 || collider.gameObject.layer == 13))
            {
                nonTriggerColliders.Add(collider);
            }
        }

        return nonTriggerColliders;
    }



    public void StartPlacingItem(GameObject gameObject)
    {
        isPlacingItem = true;
        itemToPlace = gameObject;
    }

    public void StopPlacingItem()
    {
        if (!debugMode)
        {
            isPlacingItem = false;
            itemToPlace = null;
        }

        // Change materials and layer to original configuration
        RevertToOriginal(instantiatedItem);

        // Placing item effect
        PlayCloudParticleSystem();

        instantiatedItem.layer = instantiatedItemLayerMask;
        instantiatedItem = null;
    }

    private void ModifyAllMeshes(GameObject instantiatedItem)
    {
        originalMaterials.Clear();
        originalLayers.Clear();

        // Get all MeshRenderer components in the GameObject and its children
        MeshRenderer[] renderers = instantiatedItem.GetComponentsInChildren<MeshRenderer>();

        // Loop through each MeshRenderer
        foreach (MeshRenderer renderer in renderers)
        {
            // Save original material and layer for each renderer
            originalMaterials.AddRange(renderer.materials);
            originalLayers.Add(renderer.gameObject.layer);

            // Apply crafting material to all sub-meshes
            Material[] craftingMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                craftingMaterials[i] = craftingMaterial;
            }
            renderer.materials = craftingMaterials;

            // Change the layer to NoCollision for each renderer
            renderer.gameObject.layer = LayerMask.NameToLayer("NoCollision");
        }
    }

    private void RevertToOriginal(GameObject instantiatedItem)
    {
        // Get all MeshRenderer components in the GameObject and its children
        MeshRenderer[] renderers = instantiatedItem.GetComponentsInChildren<MeshRenderer>();
        int materialIndex = 0;

        // Loop through each MeshRenderer
        for (int i = 0; i < renderers.Length; i++)
        {
            MeshRenderer renderer = renderers[i];

            // Set the materials back to their original
            Material[] materials = new Material[renderer.materials.Length];
            for (int j = 0; j < renderer.materials.Length; j++)
            {
                if (materialIndex < originalMaterials.Count)
                {
                    materials[j] = originalMaterials[materialIndex];
                    materialIndex++;
                }
            }
            renderer.materials = materials;

            // Revert back to the original layer
            if (i < originalLayers.Count)
            {
                renderer.gameObject.layer = originalLayers[i];
            }
        }
    }

    // Play cloud particle system, size adjusted to item being placed
    private void PlayCloudParticleSystem()
    {
        if (instantiatedItem.TryGetComponent<Renderer>(out var instantiatedItemRenderer))
        {
            Vector3 size = instantiatedItemRenderer.bounds.size;
            cloudParticleSystem.transform.localScale = size * cloudScaling;
        }
        else
        {
            cloudParticleSystem.transform.localScale = defaultCloudSize;
        }
        cloudParticleSystem.transform.position = instantiatedItem.transform.position;
        cloudParticleSystem.Play();
    }
}
