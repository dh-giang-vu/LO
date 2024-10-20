using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    private MenuCraft itemBeingCrafted = null;

    public void StartCraftingItem(MenuCraft menuCraft)
    {
        itemBeingCrafted = menuCraft;
    }

    public void FinishCraftingItem()
    {
        itemBeingCrafted = null;
    }

    public void CancelCraftingItem()
    {
        itemBeingCrafted.RefundMaterials();
        itemBeingCrafted = null;
    }
}
