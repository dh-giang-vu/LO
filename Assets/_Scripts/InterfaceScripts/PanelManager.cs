using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingToggle;
    [SerializeField] private GameObject lightToggle;
    [SerializeField] private GameObject decorToggle;

    public int numberOfLights = 0;
    public int numberOfBuildings = 0;

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

    public void AddNumberOfLights()
    {
        numberOfLights++;
    }

    public void AddNumberOfBuildings()
    {
        numberOfBuildings++;
    }

    public int GetNumberOfLights()
    {
        return numberOfLights;
    }

    public int GetNumberOfBuildings()
    {
        return numberOfBuildings;
    }

}
