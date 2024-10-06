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

    [SerializeField] private bool debugMode = false;

    void Start()
    {
        cam = Camera.main;
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
                instantiatedItem.layer = LayerMask.NameToLayer("NoCollision");   
            }
            else
            {
                instantiatedItem.transform.position = placePosition;
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
        instantiatedItem.layer = LayerMask.NameToLayer("Default");
        instantiatedItem = null;
    }
}
