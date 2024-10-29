using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] CraftingController craftingController;


    void Update()
    {
        if (craftingController.buildCrafted is true) 
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }
}
