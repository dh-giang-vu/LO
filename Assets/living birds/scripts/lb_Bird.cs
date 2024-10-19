using UnityEngine;
using System.Collections;

public class lb_Bird : MonoBehaviour {
    enum birdBehaviors {
        sing,
        preen,
        ruffle,
        peck
    }

    public AudioClip song1;
    public AudioClip song2;

    Animator anim;

    bool idle = true;
    bool perched = false;

    // hash variables for the animation states and properties
    int idleAnimationHash;
    int singTriggerHash;
    int peckBoolHash;
    int ruffleBoolHash;
    int preenBoolHash;

    float agitationLevel = .5f;

    void OnEnable () {
        anim = gameObject.GetComponent<Animator>();
        
        // Initialize the animation hashes
        idleAnimationHash = Animator.StringToHash("Base Layer.Idle");
        singTriggerHash = Animator.StringToHash("sing");
        peckBoolHash = Animator.StringToHash("peck");
        ruffleBoolHash = Animator.StringToHash("ruffle");
        preenBoolHash = Animator.StringToHash("preen");

        anim.SetFloat("IdleAgitated", agitationLevel);
    }

    void OnGroundBehaviors() {
        idle = anim.GetCurrentAnimatorStateInfo(0).nameHash == idleAnimationHash;
        if (idle) {
            // Randomly choose a behavior every 3 seconds
            if (Random.value < Time.deltaTime * 0.33f) {
                float rand = Random.value;
                if (rand < .3f) {
                    DisplayBehavior(birdBehaviors.sing);
                } else if (rand < .5f) {
                    DisplayBehavior(birdBehaviors.peck);
                } else if (rand < .6f) {
                    DisplayBehavior(birdBehaviors.preen);
                } else {
                    DisplayBehavior(birdBehaviors.ruffle);
                }

                // Adjust agitation level to mix up idle animations
                anim.SetFloat("IdleAgitated", Random.value);
            }
        }
    }

    void DisplayBehavior(birdBehaviors behavior) {
        idle = false;
        switch (behavior) {
            case birdBehaviors.sing:
                anim.SetTrigger(singTriggerHash);
                PlaySong();
                break;
            case birdBehaviors.ruffle:
                anim.SetTrigger(ruffleBoolHash);
                break;
            case birdBehaviors.preen:
                anim.SetTrigger(preenBoolHash);
                break;
            case birdBehaviors.peck:
                anim.SetTrigger(peckBoolHash);
                break;
        }
    }

    void PlaySong() {
        if (Random.value < 0.5f) {
            GetComponent<AudioSource>().PlayOneShot(song1, 0.1f);
        } else {
            GetComponent<AudioSource>().PlayOneShot(song2, 0.1f);
        }
    }

    void Update() {
        OnGroundBehaviors();  // Continuously check for idle behaviors
    }
}
