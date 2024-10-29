using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] PanelManager panelManager;


    void Update()
    {
        if (panelManager.GetNumberOfLights() != 0) 
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }

}
