using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGen : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject thorns;
    public Terrain terrain;
    [SerializeField] private Transform character;
    private float noSpawnDistance = 40f;
    public int numberOfInitObjects; // Number of objects when game starts
    public float autoSpawnInterval; // Auto spawn time interval
    // Start is called before the first frame update
    public GameObject initialThornSpawnPoints;

    void Start()
    {
        // Spawn 'numberOfInitObjects' objects initially
        InitGenerate();
        // Spawn thorns at the predefined spawn points
        SpawnCastleThorns();
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
        GameObject instantiatedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // 20% chance to spawn thorns
        if (Random.Range(0, 100) > 80 && thorns != null)
        {
            GameObject thorn = Instantiate(thorns, instantiatedObject.transform.position, thorns.transform.rotation, instantiatedObject.transform);
            thorn.transform.localScale = new Vector3(250.0f, 250.0f, 354.160f);
        }

    }

    void GenerateObjectsOutsideCharacterView()
    {
        // Get the terrain size
        TerrainData terrainData = terrain.terrainData;
        float terrainWidth = terrainData.size.x;
        float terrainLength = terrainData.size.z;

        Vector3 characterPosition = character.position;

        // Variables to store random X and Z positions
        float randomX = 0;
        float randomZ = 0;

        bool isTooClose = true;
        while (isTooClose)
        {
            // Generate random X and Z positions within terrain bounds
            randomX = Random.Range(0, terrainWidth);
            randomZ = Random.Range(0, terrainLength);

            // Calculate the distance from the character to the generated position
            float distanceFromCharacter = Vector3.Distance(new Vector3(randomX, 0, randomZ), new Vector3(characterPosition.x, 0, characterPosition.z));

            // Check if the generated position is far enough from the character
            if (distanceFromCharacter > noSpawnDistance)
            {
                isTooClose = false; // Exit the loop if the position is far enough
            }
        }

        // Get the height of the terrain at the random X and Z positions
        float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

        // Spawn the object at the calculated position
        Vector3 spawnPosition = new Vector3(randomX, terrainHeight, randomZ);
        GameObject instantiatedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // 20% chance to spawn thorns
        if (Random.Range(0, 100) > 80 && thorns != null)
        {
            GameObject thorn = Instantiate(thorns, instantiatedObject.transform.position, thorns.transform.rotation, instantiatedObject.transform);
            thorn.transform.localScale = new Vector3(250.0f, 250.0f, 354.160f);
        }
    }

    //Initial spawn 
    void InitGenerate()
    {
        for (int i = 0; i < numberOfInitObjects; i++)
        {
            // GenerateObjectsOnTerrain();
            GenerateObjectsOutsideCharacterView();
        }
    }
    // Coroutine to auto-spawn 1 object every interval
    IEnumerator AutoSpawnObjects()
    {
        while (true) // Infinite loop to keep spawning indefinitely
        {
            yield return new WaitForSeconds(autoSpawnInterval);
            // GenerateObjectsOnTerrain();
            GenerateObjectsOutsideCharacterView();
        }
    }

    // Spawns thorns at random sizes and rotations at each child of the 'initialThornSpawnPoints' object
    private void SpawnCastleThorns()
    {
        if (initialThornSpawnPoints == null || thorns == null) return;

        // Get all child transforms of 'initialThornSpawnPoints'
        foreach (Transform spawnPoint in initialThornSpawnPoints.transform)
        {
            // Get the height of the terrain at the random X and Z positions
            float terrainHeight = terrain.SampleHeight(spawnPoint.position);
            // Add the terrain position to the object’s spawning point
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, terrainHeight, spawnPoint.position.z);

            // Spawn the object at the calculated position
            GameObject instantiatedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            GameObject thorn = Instantiate(thorns, instantiatedObject.transform.position, thorns.transform.rotation, instantiatedObject.transform);
            thorn.transform.localScale = new Vector3(250.0f, 250.0f, 354.160f);
        }
    }
}
