using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    [SerializeField] private float progress = 0.0f;
    [SerializeField] private float updateInterval = 2.0f;
    [SerializeField] private float raycastDistance = 10.0f;
    [SerializeField] private int gridSpacing = 5; 
    [SerializeField] private Terrain terrain;
    private float terrainWidth;
    private float terrainHeight;
    void Start()
    {
        terrainWidth = terrain.terrainData.size.x;
        terrainHeight = terrain.terrainData.size.z;
        InvokeRepeating(nameof(CheckGameProgress), 0, updateInterval);   
    }

    private void CheckGameProgress()
    {
        float numberOfHits = 0;
        int numberOfRays = 0;
        Vector3 terrainPos = terrain.GetPosition();
        
        for (int x = (int)terrainPos.x; x <= terrainWidth; x += gridSpacing)
        {
            for (int z = (int)terrainPos.z; z <= terrainHeight; z += gridSpacing)
            {
                Vector3 point = new(x, raycastDistance, z);
                numberOfRays += 1;

                Ray ray = new(point, Vector3.down);
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.yellow);

                if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
                {
                    LightSource lightSource = hit.collider.GetComponentInParent<LightSource>();
                    if (lightSource != null && lightSource.alive)
                    {
                        numberOfHits += 1;
                    }
                }
            }
        }

        progress = numberOfHits / numberOfRays;
    }
}
