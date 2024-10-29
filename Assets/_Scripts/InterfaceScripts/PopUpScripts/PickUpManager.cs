using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] Inventory playerInventory;

    // Variables to store previous resource amounts
    private int previousWoodAmount;
    private int previousStoneAmount;
    private int previousCoalAmount;
    private int previousMetalAmount;
    private int previousFiberAmount;

    void Start()
    {
        // Initialize the previous amounts to the current inventory amounts
        previousWoodAmount = playerInventory.woodAmount;
        previousStoneAmount = playerInventory.stoneAmount;
        previousCoalAmount = playerInventory.coalAmount;
        previousMetalAmount = playerInventory.metalAmount;
        previousFiberAmount = playerInventory.fiberAmount;
    }

    void Update()
    {
        // Check if any resource amount has changed
        if (previousWoodAmount != playerInventory.woodAmount ||
            previousStoneAmount != playerInventory.stoneAmount ||
            previousCoalAmount != playerInventory.coalAmount ||
            previousMetalAmount != playerInventory.metalAmount ||
            previousFiberAmount != playerInventory.fiberAmount)
        {
            // Update the popup objects' active states
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }
}
