using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Transform orangeBar; // Reference to the child bar (orange bar)

    void Update()
    {
        // Get the current stamina amount from the StaminaManager singleton (value between 0.0 and 1.0)
        float oldStaminaAmount = StaminaManager.Instance.GetStaminaAmount;
        float staminaAmount = oldStaminaAmount / 10.0f;


        // Update the X scale of the orange bar to reflect the stamina amount
        orangeBar.localScale = new Vector3(staminaAmount, orangeBar.localScale.y, orangeBar.localScale.z);
    }
}
