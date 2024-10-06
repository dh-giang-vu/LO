using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public TextMeshProUGUI resourceText;

    private Dictionary<string, int> resourceAmounts;

    private string part1 = "<sprite name=\"";
    private string part2 = "\">";

    private void Start()
    {
        // Initialize resourceAmounts dictionary
        resourceAmounts = new Dictionary<string, int>()
        {
            { "Wood", 0 },
            { "Stone", 0 },
            { "Coal", 0 },
            { "Metal", 0 },
            { "Fiber", 0 }
        };

        // Update resource amounts and display
        UpdateResourceAmounts();
        DisplayResources();
        DisplayStats();
    }

    private void Update()
    {
        // Optionally update the resources in real-time
        UpdateResourceAmounts();
        DisplayResources();
        DisplayStats();
    }

    private void UpdateResourceAmounts()
    {
        // Retrieve the inventory instance
        Inventory inventory = Inventory.Instance;

        // Update the quantities of each resource from the inventory
        foreach (CollectableClass item in inventory.items)
        {
            if (resourceAmounts.ContainsKey(item.itemName))
            {
                resourceAmounts[item.itemName] = item.quantity;
            }
        }
    }

    private void DisplayResources()
    {
        // Clear the resourceText before displaying resources
        resourceText.text = "";

        // Loop through each resource and add it to the resourceText
        foreach (var resource in resourceAmounts)
        {
            resourceText.text += resource.Value + "x " + part1 + resource.Key + part2 + "   ";
        }
    }

    private void DisplayStats()
    {
        Inventory inventory = Inventory.Instance;
        // Clear the resourceText before displaying resources
    }

}
