using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Logic: stop player from being able to sprint for a while if stamina is depleted to 0.
*/
public class StaminaManager : MonoBehaviour
{
    // Singleton instance
    public static StaminaManager Instance { get; private set; }

    [SerializeField] private float staminaCap = 10.0f;
    [SerializeField] private float staminaDeductionRate = 5.0f;
    [SerializeField] private float staminaRecoveryRate = 2.0f;
    [SerializeField] private float staminaDepletedTime = 5.0f;

    private float currentStamina;
    private bool staminaDepleted;

    void Awake()
    {
        // Check if instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this; // Set the singleton instance
            DontDestroyOnLoad(gameObject); // Optional: persists between scenes
        }
    }

    void Start()
    {
        currentStamina = staminaCap;
        staminaDepleted = false;
    }

    public float GetStaminaAmount => currentStamina; // Property to access stamina amount

    public bool CanSprint()
    {
        return !staminaDepleted;
    }

    public void RecoverStamina()
    {
        if (currentStamina < staminaCap)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
        }

        if (staminaDepleted)
        {
            Debug.Log("Stamina has been depleted. Cannot sprint temporarily. Recovering Stamina: " + currentStamina.ToString());
        }
    }

    public void ConsumeStamina()
    {
        if (staminaDepleted)
        {
            return;
        }

        currentStamina -= staminaDeductionRate * Time.deltaTime;

        if (currentStamina <= 0)
        {
            staminaDepleted = true;
            StartCoroutine(RecoverDepletedStamina());
        }

        Debug.Log("Consuming Stamina: " + currentStamina.ToString());
    }

    private IEnumerator RecoverDepletedStamina()
    {
        yield return new WaitForSeconds(staminaDepletedTime);
        staminaDepleted = false;
    }
}
