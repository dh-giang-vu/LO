using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private float destroyWaitTime = 1f;
    [SerializeField] private CollectableClass[] collectables;
    [SerializeField] private AudioClip resourceCollectionSound;
    [SerializeField, Range(0,1)] private float audioVolume = 1.0f;

    private Inventory inventory;  // Dynamically found reference to the player's Inventory

    void Start()
    {
        // Find Inventory instance in the scene if it's not assigned
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("Inventory not found in the scene. Ensure an Inventory exists.");
            }
        }
    }

    public void Interact()
    {
        AudioSource.PlayClipAtPoint(resourceCollectionSound, transform.position, audioVolume);
        StartCoroutine(DelayedRemoveSelf());
    }

    IEnumerator DelayedRemoveSelf()
    {
        yield return new WaitForSeconds(destroyWaitTime);
        UpdateInventory();
        Destroy(gameObject);
    }

    void UpdateInventory()
    {
        for (int i = 0; i < collectables.Length; i++)
        {
            Debug.Log(collectables[i]);
            inventory.AddItem(collectables[i]);
        }
    }
}
