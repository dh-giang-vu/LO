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
            if (provider.getSanityEffect() == 0f)
            {
                inLight = true;  // Player is near a light source
            }
            else
            {
                inBuilding = true;  // Player is inside a building
            }
        }
    }

    public void BuildingEffectOn()
    {
        Debug.Log("TURNING ON BUILDING FILTER");
        screenPostProcessing.BuildingFilterOn();
    }

    public void BuildingEffectOff()
    {
        Debug.Log("TURNING OFF BUILDING FILTER");
        screenPostProcessing.BuildingFilterOff();
    }

    public void LightSourceEffectOn()
    {
        Debug.Log("TURNING ON LIGHT FILTER");
        screenPostProcessing.LightSourceFilterOn();
    }

    public void LightSourceEffectOff()
    {
        Debug.Log("TURNING OFF LIGHT FILTER");
        screenPostProcessing.LightSourceFilterOff();
    }
}
