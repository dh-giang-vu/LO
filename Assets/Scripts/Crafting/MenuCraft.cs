using UnityEngine;

public class MenuCraft : MonoBehaviour
{
    // Serialized fields for material requirements, configurable per GameObject
    [SerializeField] private int requiredWood = 0;
    [SerializeField] private int requiredCoal = 0;
    [SerializeField] private int requiredStone = 0;
    [SerializeField] private int requiredMetal = 0;
    [SerializeField] private int requiredFiber = 0;

    // SerializeField to specify the type of item to craft, drag-and-drop in Inspector
    [SerializeField] private ItemClass itemToCraft;

    private Inventory inventory;  // Reference to the Inventory singleton

    public string messageToPrint = "Default Message";

    private void Start()
    {
        // Get the singleton instance of the Inventory
        inventory = Inventory.Instance;

        // Ensure the inventory instance is valid
        if (inventory == null)
        {
            Debug.LogError("Inventory singleton instance is null. Ensure Inventory is instantiated.");
        }

        // Ensure that the item to craft is assigned
        if (itemToCraft == null)
        {
            Debug.LogError("No item type assigned for crafting! Please assign an item in the inspector.");
        }
    }

    // Method that runs if enough materials are available
    public ItemClass DoCraftItem()
    {
        if (HasRequiredMaterials())
        {
            Debug.Log(messageToPrint);
            Debug.Log("Enough resources available. DoCraftItem method executed.");

            // Consume resources
            UseRequiredMaterials();

            // Return the crafted item type
            return itemToCraft;
        }
        else
        {
            Debug.LogWarning("Not enough materials to craft the item.");
            return null; // Return null if crafting fails
        }
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

    // Consume the required materials from the inventory (optional)
    private void UseRequiredMaterials()
    {
        if (HasRequiredMaterials())
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
    }
}
