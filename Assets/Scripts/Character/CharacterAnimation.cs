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

    public void SetMovementAnimation(bool isMoving, bool isSprinting)
    {

        if (isMoving)
        {
            animator.avatar = running;
        }
        else
        {
            animator.avatar = idle;
        }
        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isSprinting", isSprinting);
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
        Debug.Log("OnGatheringEnd");
    }
}
