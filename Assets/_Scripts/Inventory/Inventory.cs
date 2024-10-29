using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<CollectableClass> items = new List<CollectableClass>();

    // Variables to store the amounts of materials
    public int woodAmount { get; private set; }
    public int stoneAmount { get; private set; }
    public int coalAmount { get; private set; }
    public int metalAmount { get; private set; }
    public int fiberAmount { get; private set; }

    public void Start()
    {
        // Print out the inventory items for debugging
        foreach (var item in items)
        {
            Debug.Log(item.ToString());
        }

        // Initialize material amounts by counting them in the current inventory
        UpdateMaterialCounts();
    }
      public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            AddItemsCheat();
        }
    }

    private void AddItemsCheat()
    {
        for (int i = 0; i < 20; i++)
        {
            CollectableClass wood = new CollectableClass();
            wood.itemName = "Wood";
            wood.quantity = 1;
            
            CollectableClass stone = new CollectableClass();
            stone.itemName = "Stone";
            stone.quantity = 1;
            
            CollectableClass coal = new CollectableClass();
            coal.itemName = "Coal";
            coal.quantity = 1;

            CollectableClass metal = new CollectableClass();
            metal.itemName = "Metal";
            metal.quantity = 1;

            CollectableClass fiber = new CollectableClass();
            fiber.itemName = "Fiber";
            fiber.quantity = 1;

            AddItem(wood);
            AddItem(stone);
            AddItem(coal);
            AddItem(metal);
            AddItem(fiber);
        }
    }

    public void AddItem(CollectableClass newItem) //Add item to list and update its quantity
    {
        CollectableClass existingItem = items.Find(item => item.itemName == newItem.itemName);
        if (existingItem != null)
        {
            // Add quantity to the existing item
            existingItem.quantity += 1;
        }
        else
        {
            // Add new item to the inventory
            newItem.quantity = 1;
            items.Add(newItem);
        }

        // Update material counts after adding an item
        UpdateMaterialCounts();
    }

    public void RemoveItem(CollectableClass item, int removeQuantity)  // Remove quantity when crafted
    {
        int index = items.IndexOf(item);
        if (index >= 0)
        {
            if (item.quantity >= removeQuantity)
            {
                // Decrease the stack size
                item.quantity -= removeQuantity;
            }
        }

        // Update material counts after removing an item
        UpdateMaterialCounts();
    }

    // Method to update material counts based on the current inventory
    public void UpdateMaterialCounts()
    {
        // Reset counts before calculating them
        woodAmount = 0;
        stoneAmount = 0;
        coalAmount = 0;
        metalAmount = 0;
        fiberAmount = 0;

        // Count the quantities of each resource
        foreach (var item in items)
        {
            switch (item.itemName)
            {
                case "Wood":
                    woodAmount = item.quantity;
                    break;
                case "Stone":
                    stoneAmount = item.quantity;
                    break;
                case "Coal":
                    coalAmount = item.quantity;
                    break;
                case "Metal":
                    metalAmount = item.quantity;
                    break;
                case "Fiber":
                    fiberAmount = item.quantity;
                    break;
            }
        }
    }

    void SpawnObjectInFrontOfCamera(ItemClass objectToSpawn)
    {
        if (objectToSpawn != null)
        {
            Camera mainCamera = Camera.main;
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 5f;
            Instantiate(objectToSpawn.model, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to spawn!");
        }
    }

    private void OnApplicationQuit()
    {
        // Reset stack sizes to 1 on application quit
        foreach (var item in items)
        {
            item.quantity = 1;
        }

        items.Clear(); // Clear the list of items
    }
}
