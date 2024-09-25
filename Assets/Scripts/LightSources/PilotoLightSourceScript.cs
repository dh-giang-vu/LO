using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotoLightSourceScript : MonoBehaviour, ISanityProvider
{

    private Light light = null;
    private ParticleSystem smokes;

    public float intensity = 200f;

    public int maxLifespan = 10;
    public float lifespan = 0;
    public bool alive;

    float refuelTime = 0f;
    private Renderer renderer;

    [SerializeField] private float refuelWaitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
    renderer = GetComponent<Renderer>();
    this.light = GetComponentInChildren<Light>();
    Transform firstChild = transform.GetChild(0);
    this.smokes = firstChild.GetComponent<ParticleSystem>();
    Refuel();


    if (light == null)
    {
        Debug.Log("No Light component found in the children of this GameObject.");
    }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(lifespan);
        if (lifespan > 0) {
            lifespan = maxLifespan - (Time.time - refuelTime);
            renderer.material.SetFloat("_Dimming", 0.1f+0.7f * ( 1 - ((maxLifespan -lifespan)/maxLifespan)));
            light.intensity = intensity * ( 1 - ((maxLifespan - lifespan)/maxLifespan));
        }
        else if (lifespan <= 0 && alive) {
            Die();
        }
        
    }
    public IEnumerator ManualRefuel()
    {

        yield return new WaitForSeconds(refuelWaitTime);
        Refuel();
    }

    public void Refuel() {
        lifespan = maxLifespan;
        light.intensity = intensity;
        TurnOnSmoke();
        refuelTime = Time.time;
        alive = true;
    }

    public void Die() {
        TurnOffSmoke();
        lifespan = -1;
        light.intensity = 0;
        alive = false;
    }

    private void TurnOffSmoke() {
        smokes.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        Debug.Log("All particles cleared.");
    }

    private void TurnOnSmoke() {
        smokes.Play();
    }

    public bool isActive()
    {
        return alive;
    }

    public float getSanityEffect()
    {
        return 0.0f;
    }
}
