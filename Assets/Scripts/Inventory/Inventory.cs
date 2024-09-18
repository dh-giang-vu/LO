using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // [SerializeField] private ItemClass itemToAdd;  //Collect items: set woods
    // [SerializeField] private CollectableClass itemToCraft; //items used for craft
    // [SerializeField] private ItemClass craftedItem; // crafted item: set campfire

    public int sanity = 100;

    public static Inventory Instance { get; private set; }

    public List<CollectableClass> items = new List<CollectableClass>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {

        

        foreach (var item in items)
        {
            Debug.Log(item.ToString());
        }

    }

    void Update()
    {
        // Press "A" to add 1 wood, Gather function here
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     AddItem(itemToAdd);
        //     foreach (var item in items)
        //     {
        //         Debug.Log(item.ToString());
        //     }
        // }

        // Press "C" to craft 1 campfire, Crafting here
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     Craft("Wood", 3, craftedItem);
        //     foreach (var item in items)
        //     {
        //         Debug.Log(item.ToString());
        //     }
        // }
    }
    public void AddItem(CollectableClass newItem) //Add item to list and update its quatity // Update only collectable items
    {
        CollectableClass existingItem = items.Find(item => item.itemName == newItem.itemName);
        if (existingItem != null)
        {
            // add quatity to the them
            existingItem.quantity += 1;

        }

        else items.Add(newItem);
    }

    public void RemoveItem(CollectableClass item, int removeQuatity)  //remove quality when crafted

    {

        int index = items.IndexOf(item);
        if (index >= 0)
        {
            if (item.quantity >= removeQuatity)
            {
                // Decrease the stack size
                item.quantity -= removeQuatity;
            }
        }
    }
    // Revamp crafting system
    // public void Craft(string requiredItemName, int requiredQuantity, ItemClass craftItem)
    // {
    //     // Find the item with the specified name in the inventory
    //     ItemClass itemToCraft = items.Find(item => item.itemName == requiredItemName);

    //     // Check if the item was found and if the player has enough quantity
    //     if (itemToCraft != null && itemToCraft.quantity >= requiredQuantity)
    //     {
    //         // Reduce the quantity of the required item
    //         itemToCraft.quantity -= requiredQuantity;

    //         // Remove the item if its quantity is zero
    //         if (itemToCraft.quantity <= 0)
    //         {
    //             items.Remove(itemToCraft);
    //         }

    //         // Add the crafted item to the inventory
    //         AddItem(craftItem);

    //         // Spawn the crafted item (campfire) in front of the player
    //         SpawnObjectInFrontOfCamera(craftItem);
    //     }
    //     else
    //     {
    //         // Not enough resources
    //         Debug.Log("Not enough resources");
    //     }
    // }
    //Test spawn, need to fix position
    void SpawnObjectInFrontOfCamera(ItemClass objectToSpawn)
    {
        // Check if the object to spawn is assigned
        if (objectToSpawn != null)
        {
            // Get the camera
            Camera mainCamera = Camera.main;

            // Calculate the spawn position in front of the camera
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 5f;

            // Spawn the object at the calculated position with no rotation
            Instantiate(objectToSpawn.model, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to spawn!");
        }
    }
    private void OnApplicationQuit()
    {
        // reset stack sizes
        foreach (var item in items)
        {
            item.quantity = 1; // Reset stack sizes to 1
        }


        items.Clear(); // Clear the list of items
    }
}
