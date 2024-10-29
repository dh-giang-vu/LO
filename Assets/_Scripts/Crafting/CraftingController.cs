using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    private MenuCraft itemBeingCrafted = null;
    public bool lightCrafted;
    public bool buildCrafted;

    public void StartCraftingItem(MenuCraft menuCraft)
    {
        itemBeingCrafted = menuCraft;
    }

    public void FinishCraftingItem()
    {
        itemBeingCrafted = null;

        if (itemBeingCrafted.itemToCraft is LightClass)
        {
            lightCrafted = true;
            lightCrafted = false;
        }

        if (itemBeingCrafted.itemToCraft is BuildingClass)
        {
            buildCrafted = true;
            buildCrafted = false;
        }
    }

    public void CancelCraftingItem()
    {
        itemBeingCrafted.RefundMaterials();
        itemBeingCrafted = null;
    }


}
