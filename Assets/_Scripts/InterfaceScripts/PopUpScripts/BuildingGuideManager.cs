using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] PanelManager panelManager;


    void Update()
    {
        if (panelManager.GetNumberOfBuildings() != 0) 
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }
}
