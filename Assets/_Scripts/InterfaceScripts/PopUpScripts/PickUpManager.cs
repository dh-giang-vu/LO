using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] Inventory playerInventory;


    void Update()
    {
        if (playerInventory.woodAmount != 0 ||
            playerInventory.stoneAmount != 0 ||
            playerInventory.coalAmount != 0 ||
            playerInventory.metalAmount != 0 ||
            playerInventory.fiberAmount != 0)
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }

}
