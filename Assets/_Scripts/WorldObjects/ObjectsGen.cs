using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGen : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Terrain terrain;
    public int numberOfInitObjects; // Number of objects when game starts
    public float autoSpawnInterval; // Auto spawn time interval
    // Start is called before the first frame update
    void Start()
    {
        // Spawn 'numberOfInitObjects' objects initially
        InitGenerate();
        // Start auto-spawning 1 item every 'autoSpawnInterval' seconds after initial spawn
        StartCoroutine(AutoSpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateObjectsOnTerrain()
    {
        // Get the terrain size
        TerrainData terrainData = terrain.terrainData;
        float terrainWidth = terrainData.size.x;
        float terrainLength = terrainData.size.z;

       
            // Generate random X and Z positions within the terrain bounds
            float randomX = Random.Range(0, terrainWidth);
            float randomZ = Random.Range(0, terrainLength);

            // Get the height of the terrain at the random X and Z positions
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

            // Add the terrain position to the object’s spawning point
            Vector3 spawnPosition = new Vector3(randomX, terrainHeight, randomZ);

            // Spawn the object at the calculated position
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        
    }

//Initial spawn 
    void InitGenerate()
    {
        for (int i = 0; i < numberOfInitObjects; i++)
        {
            GenerateObjectsOnTerrain();
        }
    }
    // Coroutine to auto-spawn 1 object every interval
    IEnumerator AutoSpawnObjects()
    {
        while (true) // Infinite loop to keep spawning indefinitely
        {
            yield return new WaitForSeconds(autoSpawnInterval); 
            GenerateObjectsOnTerrain();
        }
    }
}
