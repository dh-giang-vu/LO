using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightSource : MonoBehaviour, ISanityProvider
{
    public int maxLifespan = 10;
    public float lifespan = 0;
    public bool alive;
    public float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract IEnumerator ManualRefuel();
    public abstract void Refuel();
    public abstract void Die();

    public abstract bool isActive();
    public abstract float getSanityEffect();
    public abstract bool Refuelable();
}