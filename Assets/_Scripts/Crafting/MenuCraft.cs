using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCraft : MonoBehaviour
{
    // Serialized fields for material requirements, configurable per GameObject
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private int requiredWood = 0;
    [SerializeField] private int requiredCoal = 0;
    [SerializeField] private int requiredStone = 0;
    [SerializeField] private int requiredMetal = 0;
    [SerializeField] private int requiredFiber = 0;

    [SerializeField] public ItemClass itemToCraft; // This is now of type LightClass (ScriptableObject)

    [SerializeField] private Inventory inventory;  // Reference to the Inventory

    // Reference to the CraftUIMove script to control the UI movement
    [SerializeField] private CraftUIMove craftUIMove;

    // Reference to ItemPlacer
    [SerializeField] private PlaceItem itemPlacer;

    // Reference to CraftingController
    [SerializeField] private CraftingController craftingController;

    [SerializeField] private PopUpController popUpController;


    private void Start()
    {
        // Ensure the inventory instance is valid
        if (inventory == null)
        {
            Debug.LogError("Inventory reference is missing. Assign the script in the inspector.");
        }

        // Make sure itemToCraft is assigned
        if (itemToCraft == null)
        {
            Debug.LogError("No LightClass item assigned for crafting!");
        }

        // Ensure the CraftUIMove script is assigned
        if (craftUIMove == null)
        {
            Debug.LogError("CraftUIMove reference is missing. Assign the script in the inspector.");
        }

        // itemPlacer = FindObjectOfType<PlaceItem>();
        if (itemPlacer != null)
        {
            Debug.Log("PlaceItem script found.");
        }
        else
        {
            Debug.Log("PlaceItem script not found in the scene.");
        }
    }

    // Method to handle crafting the item and starting placement
    public void CraftAndPlaceItem()
    {
        // Check if enough materials exist for crafting
        if (HasRequiredMaterials())
        {
            UseRequiredMaterials(); // Consume the materials
            craftingController.StartCraftingItem(this);
            itemPlacer.StartPlacingItem(itemToCraft.model);      // Start placing the item

            // Call ToggleObjectPosition from CraftUIMove to move the UI element
            craftUIMove.ToggleObjectPosition();
        }
        else
        {
            DisplayNotEnough();
            Debug.LogWarning("Not enough materials to craft the item.");
        }
    }

    void DisplayNotEnough()
    {
        popUpController.ActivateAndFadeOut();
        
    }

    // Check if the inventory has the required materials
    private bool HasRequiredMaterials()
    {
        return inventory.woodAmount >= requiredWood &&
               inventory.stoneAmount >= requiredStone &&
               inventory.coalAmount >= requiredCoal &&
               inventory.metalAmount >= requiredMetal &&
               inventory.fiberAmount >= requiredFiber;
    }

    // Consume the required materials from the inventory
    private void UseRequiredMaterials()
    {
        // Find and reduce the quantities in the inventory
        CollectableClass wood = inventory.items.Find(item => item.itemName == "Wood");
        CollectableClass stone = inventory.items.Find(item => item.itemName == "Stone");
        CollectableClass coal = inventory.items.Find(item => item.itemName == "Coal");
        CollectableClass metal = inventory.items.Find(item => item.itemName == "Metal");
        CollectableClass fiber = inventory.items.Find(item => item.itemName == "Fiber");

        if (wood != null) wood.quantity -= requiredWood;
        if (stone != null) stone.quantity -= requiredStone;
        if (coal != null) coal.quantity -= requiredCoal;
        if (metal != null) metal.quantity -= requiredMetal;
        if (fiber != null) fiber.quantity -= requiredFiber;

        // Update material counts in the Inventory
        inventory.UpdateMaterialCounts();
    }

    public void RefundMaterials()
    {
        // Find and reduce the quantities in the inventory
        CollectableClass wood = inventory.items.Find(item => item.itemName == "Wood");
        CollectableClass stone = inventory.items.Find(item => item.itemName == "Stone");
        CollectableClass coal = inventory.items.Find(item => item.itemName == "Coal");
        CollectableClass metal = inventory.items.Find(item => item.itemName == "Metal");
        CollectableClass fiber = inventory.items.Find(item => item.itemName == "Fiber");

        if (wood != null) wood.quantity += requiredWood;
        if (stone != null) stone.quantity += requiredStone;
        if (coal != null) coal.quantity += requiredCoal;
        if (metal != null) metal.quantity += requiredMetal;
        if (fiber != null) fiber.quantity += requiredFiber;

        // Update material counts in the Inventory
        inventory.UpdateMaterialCounts();

    }

}
