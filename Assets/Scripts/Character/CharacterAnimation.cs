using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Avatar idle;
    [SerializeField] private Avatar running;
    [SerializeField] private Avatar gathering;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.avatar = idle;
    }

    public void SetAnimation(bool isRunning)
    {

        if (isRunning)
        {
            animator.avatar = running;
        }
        else
        {
            animator.avatar = idle;
        }
        animator.SetBool("isRunning", isRunning);
    }

    public void PlayGatheringAnimation()
    {
        animator.avatar = gathering;
        animator.SetTrigger("startGathering");
    }

    public void OnGatheringEnd()
    {
        animator.avatar = idle;
        SendMessageUpwards("StopGathering");
    }
}
