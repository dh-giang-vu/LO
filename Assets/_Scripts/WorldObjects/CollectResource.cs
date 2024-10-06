using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private float destroyWaitTime = 1f;
    [SerializeField] private CollectableClass[] collectables;

    public void Interact()
    {
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
        var inventory = Inventory.Instance;
        for (int i = 0; i < collectables.Length; i++) {
            Debug.Log(collectables[i]);
            inventory.AddItem(collectables[i]);
        }
    }
}