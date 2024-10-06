using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternLightSource : LightSource
{
    private Light light = null;
    public float intensity = 200f;
    float refuelTime = 0f;

    private float refuelWaitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
        Refuel();
        if (light == null)
        {
            Debug.Log("No Light component found in the children of this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifespan > 0) {
            lifespan = maxLifespan - (Time.time - refuelTime);
            light.intensity = intensity * ( 1 - ((maxLifespan - lifespan)/maxLifespan));
        }
        else if (lifespan <= 0 && alive) {
            Die();
        }
    }

    public override IEnumerator ManualRefuel() {
        yield return new WaitForSeconds(refuelWaitTime);
        Refuel();
    }

    public override void Refuel() {
        lifespan = maxLifespan;
        light.intensity = intensity;
        refuelTime = Time.time;
        alive = true;
    }

    public override void Die() {
        lifespan = -1;
        light.intensity = 0;
        alive = false;
    }

    public override bool isActive()
    {
        return alive;
    }

    public override float getSanityEffect()
    {
        return 0.0f;
    }
}
