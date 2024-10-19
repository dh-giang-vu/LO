using UnityEngine;

public class BuildingSanity : MonoBehaviour, ISanityProvider
{
    [SerializeField] private float sanityEffect = 0.2f;
    public float getSanityEffect()
    {
        return sanityEffect;
    }

    public bool isActive()
    {
        return true;
    }
}
