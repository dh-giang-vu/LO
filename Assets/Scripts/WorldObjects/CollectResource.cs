using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private float destroyWaitTime = 1f;

    public void Interact()
    {
        StartCoroutine(DelayedRemoveSelf());
        // UpdateInventory();
    }

    IEnumerator DelayedRemoveSelf()
    {
        yield return new WaitForSeconds(destroyWaitTime);
        Destroy(gameObject);
    }
}