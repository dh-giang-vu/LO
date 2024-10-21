using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Transform orangeBar; // Reference to the child bar (orange bar)
    [SerializeField] private StaminaManager staminaManager; // Reference to the StaminaManager

    void Update()
    {
        // Get the current stamina amount from the StaminaManager instance (value between 0.0 and 1.0)
        float oldStaminaAmount = staminaManager.GetStaminaAmount; // No parentheses for properties
        float staminaAmount = oldStaminaAmount / 10.0f; // Adjust the value as needed

        // Update the X scale of the orange bar to reflect the stamina amount
        orangeBar.localScale = new Vector3(staminaAmount, orangeBar.localScale.y, orangeBar.localScale.z);
    }
}
