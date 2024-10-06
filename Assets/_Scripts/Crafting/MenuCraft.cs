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

    [SerializeField] private LightClass itemToCraft; // This is now of type LightClass (ScriptableObject)
    private float itemPlaceDistance = 30.0f;

    private Inventory inventory;  // Reference to the Inventory singleton
    private bool isPlacingItem = false;
    private GameObject instantiatedItem = null;

    // Variables to handle the movement of the objectToDisable
    private bool isMovingObject = false; // Track if the object is moving
    private Vector3 targetPosition; // Target position for the movement
    private float movementDuration = 1f; // Duration of movement
    private float movementProgress = 0f; // Track how far we are in the movement

    // Track if crafting is in progress
    private bool isCraftingInProgress = false;

    private void Start()
    {
        // Get the singleton instance of the Inventory
        inventory = Inventory.Instance;

        // Ensure the inventory instance is valid
        if (inventory == null)
        {
            Debug.LogError("Inventory singleton instance is null. Ensure Inventory is instantiated.");
        }

        // Make sure itemToCraft is assigned
        if (itemToCraft == null)
        {
            Debug.LogError("No LightClass item assigned for crafting!");
        }
    }

    // Method to handle crafting the item and starting placement
    public void CraftAndPlaceItem()
    {
        // Prevent crafting if already in progress
        if (isCraftingInProgress || isMovingObject)
        {
            Debug.LogWarning("Crafting or movement is already in progress.");
            return;
        }

        // Check if enough materials exist for crafting
        if (HasRequiredMaterials())
        {
            isCraftingInProgress = true; // Set the crafting flag

            UseRequiredMaterials(); // Consume the materials
            StartPlacingItem();      // Start placing the item

            // Set target position and start moving the object down
            targetPosition = objectToMove.transform.position + new Vector3(0, -500, 0);
            isMovingObject = true; // Start the movement
            movementProgress = 0f; // Reset progress
        }
        else
        {
            Debug.LogWarning("Not enough materials to craft the item.");
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

    // Method to start placing the crafted item
    private void StartPlacingItem()
    {
        // Ensure the item to craft has a model assigned
        if (itemToCraft != null && itemToCraft.model != null)
        {
            isPlacingItem = true;
        }
        else
        {
            Debug.LogError("LightClass item or its model is not assigned!");
        }
    }

    private void Update()
    {
        // Check if player is placing an item
        if (isPlacingItem && itemToCraft != null && itemToCraft.model != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Use the main camera
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out RaycastHit hit, itemPlaceDistance, LayerMask.GetMask("TerrainLayer")))
            {
                Vector3 placePosition = hit.point;

                // If the item hasn't been instantiated yet, instantiate it at the hit point
                if (instantiatedItem == null)
                {
                    instantiatedItem = Instantiate(itemToCraft.model, placePosition, Quaternion.identity);
                    instantiatedItem.layer = LayerMask.NameToLayer("NoCollision");                    
                }
                else
                {
                    instantiatedItem.transform.position = placePosition;  // Update its position as the player moves the cursor
                }
            }

            // If the player clicks the mouse button, finalize placement
            if (Input.GetKeyDown(KeyCode.Mouse0) && instantiatedItem != null)
            {
                StopPlacingItem();
            }
        }

        // Handle movement of objectToDisable
        if (isMovingObject)
        {
            movementProgress += Time.deltaTime / movementDuration; // Increment progress based on time
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, movementProgress);

            // Stop moving if we reached the target position
            if (movementProgress >= 1f)
            {
                isMovingObject = false; // Stop the movement
                movementProgress = 0f; // Reset progress
                isCraftingInProgress = false; // Reset crafting state
            }
        }
    }

    // Method to stop placing the item
    private void StopPlacingItem()
    {
        isPlacingItem = false;
        instantiatedItem.layer = LayerMask.NameToLayer("Default");
        instantiatedItem = null;  // Clear the reference so no further updates happen
    }
}