using UnityEngine;

public class SanityBar : MonoBehaviour
{
    [SerializeField] private Transform orangeBar; // Reference to the child bar (orange bar)
    [SerializeField] private SanityManager sanityManager; // Reference to the SanityManager

    void Update()
    {
        // Get the current sanity amount from the SanityManager instance (value between 0.0 and 1.0)
        float sanityAmount = sanityManager.GetSanityAmount();

        // Update the X scale of the orange bar to reflect the sanity amount
        orangeBar.localScale = new Vector3(sanityAmount, orangeBar.localScale.y, orangeBar.localScale.z);
    }
}
