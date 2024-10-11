using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] private int maxNumGhost = 10;
    [SerializeField] private float minDistanceFromPlayer = 10.0f;
    [SerializeField] private float maxDistanceFromPlayer = 100.0f;
    [SerializeField] private int maxSpawnAttempts = 50;
    [SerializeField] private LayerMask layerMask;
    private List<GameObject> ghosts;

    void Start()
    {
        ghosts = new List<GameObject>();
    }

    // Spawn ghost near proximity of the player && outside of LightSource range
    // if there are less than maxNumGhost on the map
    public void SpawnGhost()
    {
        if (ghosts.Count >= maxNumGhost)
        {
            return;
        }

        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position;

        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;

        // Attempt to find valid spawn point outside of light source range + near player
        for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
        {
            // Random position in a spherical shell between min and max distances
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            float randomDistance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            spawnPosition = playerPosition + randomDirection * randomDistance;

            // Check if the position is inside any light source's range
            if (IsValidPosition(spawnPosition))
            {
                validPosition = true;
                break;
            }
        }

        // If find a valid position, spawn the ghost
        if (validPosition)
        {
            GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity, gameObject.transform);
            ghosts.Add(ghost);
        }
        else
        {
            // Debug.LogWarning("Couldn't find a valid position to spawn a ghost.");
        }
    }

    // Valid if point is on Terrain and does not overlap with LightSource
    private bool IsValidPosition(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, ghostPrefab.GetComponent<CapsuleCollider>().radius, layerMask);
        if (colliders.Length == 0)
        {
            return false;
        }

        bool hasTerrainCollider = false;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<LightSource>())
            {
                return false;
            }
            if (collider is TerrainCollider)
            {
                hasTerrainCollider = true;
            }
        }
        return hasTerrainCollider;
    }

    public void RemoveGhost(GameObject ghost)
    {
        ghosts.Remove(ghost);
    }
}

