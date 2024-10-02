using UnityEngine;

public class MenuCraft : MonoBehaviour
{
    // Serialized fields for material requirements, configurable per GameObject
    [SerializeField] private int requiredWood = 0;
    [SerializeField] private int requiredCoal = 0;
    [SerializeField] private int requiredStone = 0;
    [SerializeField] private int requiredMetal = 0;
    [SerializeField] private int requiredFiber = 0;

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
    }

    // Method that runs if enough materials are available
    public void DoCraftItem()
    {
        if (HasRequiredMaterials())
        {
            Debug.Log(messageToPrint);
            Debug.Log("Enough resources available. DoCraftItem method executed.");

            // Optional: If you want to consume the resources after the method runs
            UseRequiredMaterials();
        }
        else
        {
            Debug.LogWarning("Not enough materials to run the DoCraftItem method.");
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
