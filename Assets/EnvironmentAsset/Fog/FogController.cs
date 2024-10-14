using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    public GameObject player; // Reference to the player
    [SerializeField] private ParticleSystem fogPrefab; // Prefab of the fog particle system
    [SerializeField] private float spawnRadius = 30f; // Radius in which to spawn fog
    [SerializeField] private float removeDistance = 40f; // Distance beyond which fog is removed
    [SerializeField] private float spawnInterval = 10f; // Interval between fog spawns

    private List<GameObject> activeFogSystems = new List<GameObject>(); // List to track active fog systems

    void Start()
    {
        // Start by spawning the initial fog systems around the player
        SpawnInitialFog();
    }

    void Update()
    {
        // Update fog systems based on player's position
        ManageFogSystems();
    }

    private void SpawnInitialFog()
    {
        // Create an initial grid or circular pattern of fog systems around the player
        for (float x = -spawnRadius; x <= spawnRadius; x += spawnInterval)
        {
            for (float z = -spawnRadius; z <= spawnRadius; z += spawnInterval)
            {
                Vector3 spawnPosition = new Vector3(player.transform.position.x + x, player.transform.position.y, player.transform.position.z + z);
                SpawnFog(spawnPosition);
            }
        }
    }

    private void ManageFogSystems()
    {
        // Remove fog systems that are too far from the player
        for (int i = activeFogSystems.Count - 1; i >= 0; i--)
        {
            GameObject fogSystem = activeFogSystems[i];
            float distance = Vector3.Distance(fogSystem.transform.position, player.transform.position);

            if (distance > removeDistance)
            {
                // Remove fog system if it's too far
                Destroy(fogSystem);
                activeFogSystems.RemoveAt(i);
            }
        }

        // Check if new fog systems need to be spawned around the player
        SpawnNewFogAroundPlayer();
    }

    private void SpawnNewFogAroundPlayer()
    {
        // Spawn fog systems in new areas around the player
        for (float x = -spawnRadius; x <= spawnRadius; x += spawnInterval)
        {
            for (float z = -spawnRadius; z <= spawnRadius; z += spawnInterval)
            {
                Vector3 spawnPosition = new Vector3(player.transform.position.x + x, player.transform.position.y, player.transform.position.z + z);

                // Check if there is already a fog system close to the spawn position
                bool fogExists = false;
                foreach (GameObject fogSystem in activeFogSystems)
                {
                    if (Vector3.Distance(fogSystem.transform.position, spawnPosition) < spawnInterval)
                    {
                        fogExists = true;
                        break;
                    }
                }

                // If no fog system exists, spawn a new one
                if (!fogExists)
                {
                    SpawnFog(spawnPosition);
                }
            }
        }
    }

    private void SpawnFog(Vector3 position)
    {
        GameObject newFog = Instantiate(fogPrefab.gameObject, position, Quaternion.identity);
        activeFogSystems.Add(newFog);
    }
}
