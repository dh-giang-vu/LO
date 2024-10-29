using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] CraftingController craftingController;


    void Update()
    {
        if (craftingController.lightCrafted is true) 
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }

}
