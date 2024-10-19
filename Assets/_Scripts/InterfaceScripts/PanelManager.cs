using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingToggle;
    [SerializeField] private GameObject lightToggle;
    [SerializeField] private GameObject decorToggle;

    public void ActivateBuilding()
    {
        buildingToggle.SetActive(!buildingToggle.activeSelf);
    }

    public void ActivateLight()
    {
        lightToggle.SetActive(!lightToggle.activeSelf);
    }

    public void ActivateDecor()
    {
        decorToggle.SetActive(!decorToggle.activeSelf);
    }
}
