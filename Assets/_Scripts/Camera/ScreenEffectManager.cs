using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectManager : MonoBehaviour
{
    [SerializeField] ScreenPostProcessing screenPostProcessing;
    [SerializeField] SanityManager sanityManager;

    bool inBuilding = false;
    bool inLight = false;

    void Update()
    {
        WithinAreaEffect();

        if (inBuilding)
        {
            BuildingEffectOn();
        }
        else
        {
            BuildingEffectOff();
        }

        if (inLight)
        {
            LightSourceEffectOn();
        }
        else
        {
            LightSourceEffectOff();
        }
    }

    public void WithinAreaEffect()
    {
        inBuilding = false;
        inLight = false;

        List<ISanityProvider> sanityProviders = sanityManager.GetActiveSanityProviders();
        foreach (ISanityProvider provider in sanityProviders)
        {
            if (provider is LightSource)
            {
                inLight = true;  // Player is near a light source
            }
            else if (provider is BuildingSanity)
            {
                inBuilding = true;  // Player is inside a building
                Debug.Log("INBUILDING");
            }
        }
    }

    public void BuildingEffectOn()
    {
        screenPostProcessing.BuildingFilterOn();
    }

    public void BuildingEffectOff()
    {
        screenPostProcessing.BuildingFilterOff();
    }

    public void LightSourceEffectOn()
    {
        screenPostProcessing.LightSourceFilterOn();
    }

    public void LightSourceEffectOff()
    {
        screenPostProcessing.LightSourceFilterOff();
    }
}
