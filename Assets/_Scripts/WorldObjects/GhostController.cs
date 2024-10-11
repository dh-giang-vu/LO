using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float timeToLive = 30.0f;
    private GhostSpawner ghostSpawner;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ghostSpawner = GetComponentInParent<GhostSpawner>();
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        // Make the ghost face the player
        Vector3 directionToPlayer = player.transform.position - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    // Coroutine to destroy the ghost after timeToLive seconds
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToLive);
        ghostSpawner.RemoveGhost(this.gameObject);
        Destroy(gameObject);
    }
}
