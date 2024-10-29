using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    private MenuCraft itemBeingCrafted = null;
    public bool lightCrafted = false;
    public bool buildCrafted = false;

    public void StartCraftingItem(MenuCraft menuCraft)
    {
        itemBeingCrafted = menuCraft;
    }

    public void FinishCraftingItem()
    {
        CheckGuideConditions();
        itemBeingCrafted = null;

    }

    public void CancelCraftingItem()
    {
        itemBeingCrafted.RefundMaterials();
        itemBeingCrafted = null;
    }

    private void CheckGuideConditions()
    {
        if (itemBeingCrafted.itemToCraft is LightClass)
        {
            lightCrafted = true;
            Debug.Log("light true");
            // lightCrafted = false;
            // Debug.Log("light false");
        }

        if (itemBeingCrafted.itemToCraft is BuildingClass)
        {
            buildCrafted = true;
            Debug.Log("building true");
            // buildCrafted = false;
            // Debug.Log("building false");
        }

    }

}
