using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenEffectManager : MonoBehaviour
{
    [SerializeField] ScreenPostProcessing screenPostProcessing;
    [SerializeField] ParticleSystem lightSourceParticles;
    [SerializeField] ParticleSystem buildingParticles;
    [SerializeField] SanityManager sanityManager;
    bool inBuilding = false;
    bool inLight = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WithinAreaEffect();
        if (inBuilding) {
            BuildingEffectOn();
        } else {
            BuildingEffectOff();
        }
        if (inLight) {
            LightSourceEffectOn();
        } else {
            LightSourceEffectOff();
        }
    }

    public void WithinAreaEffect() {
        inBuilding = false;
        inLight = false;
        List<ISanityProvider> sanityProviders = sanityManager.GetActiveSanityProviders();
        foreach (ISanityProvider provider in sanityProviders) {
            if (provider.getSanityEffect() == 0f) {
                inLight = true;
            } else {
                inBuilding = true;
            }
        }
    }

    public void BuildingEffectOn() {
        Debug.Log("TURNING ON BUILDING FILTER");
        screenPostProcessing.BuildingFilterOn();
        buildingParticles.Play();
    }

    public void BuildingEffectOff() {
        Debug.Log("TURNING OFF BUILDING FILTER");
        screenPostProcessing.BuildingFilterOff();
        buildingParticles.Stop();
    }
    
    public void LightSourceEffectOn() {
        Debug.Log("TURNING ON LIGHT FILTER");
        screenPostProcessing.LightSourceFilterOn();
        lightSourceParticles.Play();
    }

    public void LightSourceEffectOff() {
        Debug.Log("TURNING OFF LIGHT FILTER");
        screenPostProcessing.LightSourceFilterOff();
        lightSourceParticles.Stop();
    }
}
