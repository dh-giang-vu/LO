using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private bool isPlacingItem = false;
    [SerializeField] private GameObject itemToPlace = null;

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

        // Allow placing down the itemToPlace once
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("TerrainLayer")))
            {
                // Place the item at the hit point
                Vector3 placePosition = hit.point;
                Instantiate(itemToPlace, placePosition, Quaternion.identity);
                StopPlacingItem();
            }
        }

    }

    public void StartPlacingItem(GameObject gameObject)
    {
        isPlacingItem = true;
        itemToPlace = gameObject;
    }

    public void StopPlacingItem()
    {
        isPlacingItem = false;
        itemToPlace = null;
    }
}
