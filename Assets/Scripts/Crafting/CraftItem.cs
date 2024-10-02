using UnityEngine;

public class CraftInterfaceToggle : MonoBehaviour
{

    public GameObject craftInterface;

    void Start()
    {
        if (craftInterface != null)
        {
            craftInterface.SetActive(false);
        }
        else
        {
            Debug.LogWarning("CraftInterface GameObject is not assigned.");
        }
    }

    void Update()
    {
        // Only check for keypress if the CraftInterface is assigned
        if (craftInterface != null && Input.GetKeyDown(KeyCode.C))
        {
            // Toggle the active state of the CraftInterface
            bool isActive = craftInterface.activeSelf;
            craftInterface.SetActive(!isActive);
            Debug.Log("CraftInterface is now " + (isActive ? "disabled" : "enabled"));
        }
    }
}
