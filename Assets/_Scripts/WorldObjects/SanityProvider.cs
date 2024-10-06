using UnityEngine;

public interface ISanityProvider {
    // returns True if this sanity provider is effective (i.e. light source is not dead)
    bool isActive();
    // returns number of sanity it provides
    float getSanityEffect();
}
