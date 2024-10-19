using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private bool isPlacingItem = false;
    [SerializeField] private GameObject itemToPlace = null;
    [SerializeField] private GameObject instantiatedItem = null;
    [SerializeField] private ParticleSystem cloudParticleSystem;
    [SerializeField] private Vector3 defaultCloudSize = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField] private float cloudScaling = 0.25f;

    [SerializeField] private bool debugMode = false;
    private LayerMask instantiatedItemLayerMask;

    // Material handling variables
    [SerializeField] private Material craftingMaterial; // The material to apply while crafting
    private Material originalMaterial; // Store the original material
    private GameObject player;

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
                instantiatedItem = Instantiate(itemToPlace, placePosition, Quaternion.identity);
                instantiatedItemLayerMask = instantiatedItem.layer;
                instantiatedItem.layer = LayerMask.NameToLayer("NoCollision");

                // Assign blueprint material
                if (instantiatedItem.TryGetComponent<Renderer>(out var instantiatedItemRenderer))
                {
                    originalMaterial = instantiatedItemRenderer.material;
                    instantiatedItemRenderer.material = craftingMaterial;
                }
            }
            else
            {
                instantiatedItem.transform.position = placePosition;
                instantiatedItem.transform.LookAt(player.transform);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && instantiatedItem != null)
        {
            StopPlacingItem();
        }

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
        
        // Play cloud particle system, size adjusted to item being placed
        if (instantiatedItem.TryGetComponent<Renderer>(out var instantiatedItemRenderer))
        {
            Vector3 size = instantiatedItemRenderer.bounds.size;
            cloudParticleSystem.transform.localScale = size * cloudScaling;

            // Revert back to the original material
            instantiatedItemRenderer.material = originalMaterial;
        }
        else
        {
            cloudParticleSystem.transform.localScale = defaultCloudSize;
        }
        cloudParticleSystem.transform.position = instantiatedItem.transform.position;
        cloudParticleSystem.Play();


        instantiatedItem.layer = instantiatedItemLayerMask;
        instantiatedItem = null;
    }
}
