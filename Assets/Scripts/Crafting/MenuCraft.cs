using UnityEngine;

public class MenuCraft : MonoBehaviour
{
    
    // int woodAmount = 0;
    // Inventory inventory = Inventory.Instance;
    public string messageToPrint = "Default Message";

    public void PrintMessage()
    {
        //CheckWoodAmount();
        Debug.Log(messageToPrint);
        //Debug.Log("Amount of Wood: " + woodAmount);
    }

    // public void CheckWoodAmount()
    // {

    //     foreach (CollectableClass item in inventory.items)
    //     {
    //         if (item.itemName == "Wood")
    //         {
    //             woodAmount = item.quantity;
    //             break;
    //         }
    //     }

    // }
}